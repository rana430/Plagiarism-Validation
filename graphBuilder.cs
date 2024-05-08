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
            bool rootSet = false; // Keep track of whether the root node has been set
            foreach (var pair in pairs)
            {
                int sourceId = pair.Source.id;
                int destinationId = pair.Destination.id;

                // Set the root node if it hasn't been set yet
                if (!rootSet)
                {
                    root_node = sourceId;
                    rootSet = true;
                }

                // Initialize color for source and destination nodes
                if (!color.ContainsKey(sourceId))
                    color[sourceId] = 'w';
                if (!color.ContainsKey(destinationId))
                    color[destinationId] = 'w';

                // Calculate percentage similarity
                float percentage = (pair.firstSimilarity + pair.secondSimilarity) / 2f;

                // Add edge to edgesDFS dictionary
                AddEdge(sourceId, destinationId, percentage, 1);
                AddEdge(destinationId, sourceId, percentage, 1);

                // Add destination node to source node's adjacency list
                if (!adj_list.ContainsKey(sourceId))
                    adj_list[sourceId] = new List<int>();
                adj_list[sourceId].Add(destinationId);

                // Add source node to destination node's adjacency list
                if (!adj_list.ContainsKey(destinationId))
                    adj_list[destinationId] = new List<int>();
                adj_list[destinationId].Add(sourceId);
            }
        }







        private void AddEdge(int source, int destination, float weight , short dfs)
        {
            KeyValuePair<int, int> key = new KeyValuePair<int, int>(source, destination);
            edgesDFS[key] = weight;
        }
        
    }
}