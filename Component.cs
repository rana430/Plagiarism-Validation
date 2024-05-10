using System;
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
        public List<Tuple<long, Edge>> nodes;//edges of mst
        public float avgSim = 0.0f;
        public List<Tuple<float, Edge>> edges;// edges of group stat


        public Component(int weight, List<Tuple<long, Edge>> tuples)
        {
            this.weight = weight;
 
            nodes = new List<Tuple<long, Edge>>();
            nodes = tuples;

        }
        public Component(float avg, List<Tuple<float, Edge>> edge)
        {
            avgSim = avg;
            edges = new List<Tuple<float, Edge>>();
            edges = edge;
        }
        public void AddTuple(Tuple<long, Edge> tuple)
        {
            nodes.Add(tuple);
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

            foreach (var node in nodes)
            {

                Console.WriteLine($"    Node 1: {node.Item2.Source.idString}, Node 2: {node.Item2.Destination.idString}, Weight: {node.Item2.lineMatches}");
            }
            Console.WriteLine("****************************");
        }
        public void SortEdgesByLineMatches()//O(E)
        {
            nodes.Sort((x, y) => y.Item2.lineMatches.CompareTo(x.Item2.lineMatches));
        }
        
        public void calcWeight()
        {
            foreach(var edge in nodes)//O(E)
            {
                weight += edge.Item1;
            }
        }

    }
}
