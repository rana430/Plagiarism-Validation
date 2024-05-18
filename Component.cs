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
        public List<Edge> edges;// edges of group stat
        public SortedSet<long> ids;// set contains the ids of each component 
        public int edgeCount = 0;//number of edges
        public List<string> stringIds;

        public Component(int weight, List<Tuple<long, Edge>> tuples)
        {
            this.weight = weight;
            MstEdges = new List<Tuple<long, Edge>>();
            MstEdges = tuples;


        }
        public void convertIds()
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
        public Component(float avg, List<Edge> edge, List<string> id, int edgesCount)
        {
            avgSim = avg;
            edges = new List<Edge>();
            edges = edge;
            ids = new SortedSet<long>();
            stringIds = id;
            edgeCount = edgesCount;
        }
        public void AddTuple(Tuple<long, Edge> tuple)
        {
            MstEdges.Add(tuple);
        }
        public void AddTuple(Edge edge)
        {
            edges.Add(edge);
        }
        
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
      

    }
}