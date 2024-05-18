﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiarism_Validation
{
    public class Component
    {
        public long weight=0;
        public int lineMatches;
        public List<Tuple<long, Edge>> MstEdges;//edges of mst
        public float avgSim = 0.0f;// average sum of edges of each component
        public List<Tuple<float, Edge>> edges;// edges of group stat
        public List<Edge> hashEdges = new List<Edge>();
        public SortedSet<long> ids;// set contains the ids of each component 
        public int edgeCount = 0;//number of edges
        public List<string> stringIds;

        public Component(int weight, List<Tuple<long, Edge>> tuples)
        {
            this.weight = weight;
            MstEdges = new List<Tuple<long, Edge>>();
            MstEdges = tuples;


        }
        public void SortIds()
        {
            foreach(var id in stringIds)
            {
                long ll;
                if (long.TryParse(id, out ll))
                {
                    ids.Add(ll);
                   
                }
            }
        }
        public Component(float avg, List<Tuple<float, Edge>> edge, List<string> id, int edgesCount)
        {
            avgSim = avg;
            edges = new List<Tuple<float, Edge>>();
            edges = edge;
            ids = new SortedSet<long>();
            stringIds = id;
            edgeCount = edgesCount;
        }
        public void AddTuple(Tuple<long, Edge> tuple)
        {
            MstEdges.Add(tuple);
        }
        public void AddTuple(Tuple<float, Edge> tuple)
        {
            edges.Add(tuple);
        }
        public void printStatComponent()// for printing group stat in o(e)
        {
            foreach (var node in edges)
            {

                Console.WriteLine($"    Node 1: {node.Item2.Source.idString}, Node 2: {node.Item2.Destination.idString}, Weight: {node.Item2.lineMatches}");
            }
            Console.WriteLine("****************************");
        }

        public void PrintComponent()
        {

            foreach (var node in MstEdges)
            {

                Console.WriteLine($"    file 1: {node.Item2.Source.path}, file 2: {node.Item2.Destination.path}, Line Matches: {node.Item2.lineMatches}");
            }
            Console.WriteLine("****************************");
        }

        /*public void printComponnentExcel()
        {
            foreach(var i in nodes)
            {
                hashEdges.Add(i.Item2);
            }
            ExcelWriter excelWriter = new ExcelWriter();
            string filePath = @"F:\FCIS\Level 3\Second term\Algorithms\Project\[3] Plagiarism Validation\Test Cases\Complete\Easy\MST.xlsx";
            excelWriter.WriteToExcel(filePath, hashEdges);

        }*/
        public void SortEdgesByLineMatches()
        {
            MstEdges.Sort((x, y) =>
            {
                // First, compare line matches
                int lineMatchesComparison = y.Item2.lineMatches.CompareTo(x.Item2.lineMatches);

                // If line matches are equal, compare max similarity
                if (lineMatchesComparison == 0)
                {
                    // If max similarity is also equal, compare by other criteria (if available)
                    if (x.Item2.maxSimilarity == y.Item2.maxSimilarity)
                    {
                       
                        return 0;
                    }
                    // Compare by max similarity
                    return y.Item2.maxSimilarity.CompareTo(x.Item2.maxSimilarity);
                }
                // Compare by line matches
                return lineMatchesComparison;
            });
        }
        /*public string getStringIds()
        {
            string result = "";
            foreach(var id in ids)
            {

            }
        }
*/
        public void sortComponentEdges()//sort the component edges according to the maxSim & line Matches
        {

        }


        public void calcWeight()
        {
            foreach(var edge in MstEdges)//O(E)
            {
                weight += edge.Item1;
            }
        }

    }
}