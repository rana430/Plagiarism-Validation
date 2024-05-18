using System;
using System.Collections.Generic;

namespace Plagiarism_Validation
{
    public class MSTGraph
    {
        public List<Edge> edges;

        public MSTGraph()
        {
            edges = new List<Edge>();
        }

        public void BuildMSTGraph(List<Edge> pairs)
        {
            foreach (var edge in pairs)//O(e)
            {
                AddEdge(edge);
                AddEdge(new Edge(edge.lineMatches, edge.secondSimilarity, edge.firstSimilarity, edge.Destination, edge.Source, edge.rowNumber));
            }
        }

        private void AddEdge(Edge edge)
        {
            edges.Add(edge);
        }

        public void PrintGraph()
        {
            foreach (var edge in edges)
            {
                Console.WriteLine($"From {edge.Source.path} to {edge.Destination.path} with weight {edge.secondSimilarity} (Index: {edge.rowNumber})");
            }
        }
    }
}
