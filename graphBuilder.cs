﻿using System;
using System.Collections.Generic;

namespace Plagiarism_Validation
{
    public class GraphBuilder
    {
        public int root_node;
        public Dictionary<KeyValuePair<int, int>, float> edgesDFS;
        public Dictionary<int, char> color;
        public Dictionary<int, List<int>> adj_list;
        private bool root = false;
        public GraphBuilder()
        {
            edgesDFS = new Dictionary<KeyValuePair<int, int>, float>();
            color = new Dictionary<int , char>();
            adj_list = new Dictionary<int, List<int>>();
        }

        public void BuildGraph(List<Edge> pairs)
        {
            
            foreach (var pair in pairs)
            {
                int sourceId = pair.Source.id;
                int destinationId = pair.Destination.id;
                if (root == false)
                {
                    root_node = sourceId;
                    root = true;
                }
                color[sourceId] = 'w';
                color[destinationId] = 'w';
                float percentage = ((pair.firstSimilarity + pair.secondSimilarity) / 2f);
                AddEdge(sourceId, destinationId, percentage,1);
                AddEdge(destinationId, sourceId, percentage,1);
                try
                {
                    adj_list[sourceId].Add(destinationId);
                }
                catch
                {
                    adj_list[sourceId] = new List<int>();
                    adj_list[sourceId].Add(destinationId);
                }
                try
                {
                    adj_list[destinationId].Add(sourceId);
                }
                catch
                {
                    adj_list[destinationId] = new List<int>();
                    adj_list[destinationId].Add(sourceId);
                }
            }
            
        }


        
     

    
        private void AddEdge(int source, int destination, float weight , short dfs)
        {
            KeyValuePair<int, int> key = new KeyValuePair<int, int>(source, destination);
            edgesDFS[key] = weight;
        }
        
    }
}