﻿using System;
using System.Collections.Generic;

namespace Plagiarism_Validation
{
    public class GraphBuilder
    {
        public HashSet<(string source, string destination, int weight)> edges;

        public GraphBuilder()
        {
            edges = new HashSet<(string, string, int)>();
        }

        public void BuildGraph(List<(string file1, int percentage1, string file2, int percentage2,int index)> pairs)
        {
            foreach (var (file1, percentage1, file2, percentage2, index) in pairs)
            {
                AddEdge(file1, file2, percentage1);
                AddEdge(file2, file1, percentage2);  
            }
        }

        private void AddEdge(string source, string destination, int weight)
        {
            edges.Add((source, destination, weight));
        }

        public void PrintGraph()
        {
            foreach (var edge in edges)
            {
                Console.WriteLine($"From {edge.source} to {edge.destination} with weight {edge.weight}");
            }
        }
    }
}