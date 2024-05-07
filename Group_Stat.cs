using System;
using System.Collections.Generic;

namespace Plagiarism_Validation
{
    internal class Group_Stat
    {
        private GraphBuilder Graph;

        public Group_Stat(List<Edge> pairs)
        {
            Graph = new GraphBuilder();
            Graph.BuildGraph(pairs);
        }

        // Perform DFS to find connected components and compute the sum of weights for each component
        public List<KeyValuePair<List<int>, float>> CalculateStats()
        {
            List<KeyValuePair<List<int>, float>> connectedComponents = new List<KeyValuePair<List<int>, float>>();
            Dictionary<int, bool> visited = new Dictionary<int, bool>();

            foreach (var node in Graph.adj_list.Keys)
            {
                if (!visited.ContainsKey(node))
                {
                    List<int> componentNodes = new List<int>();
                    float weightSum = DepthFirstSearch(node, visited, componentNodes);
                    connectedComponents.Add(new KeyValuePair<List<int>, float>(componentNodes, weightSum));
                }
            }

            return connectedComponents;
        }

        // Depth-first search to find connected components and compute the sum of weights
        private float DepthFirstSearch(int node, Dictionary<int, bool> visited, List<int> componentNodes)
        {
            visited[node] = true;
            componentNodes.Add(node);
            float weightSum = 0;

            foreach (var neighbor in Graph.adj_list[node])
            {
                if (!visited.ContainsKey(neighbor))
                {
                    weightSum += (float)Graph.edgesDFS[new KeyValuePair<int, int>(node, neighbor)];
                    weightSum += DepthFirstSearch(neighbor, visited, componentNodes);
                }
            }

            return weightSum;
        }

        // Print connected components and their sum of weights
        public void PrintConnectedComponents()
        {
            List<KeyValuePair<List<int>, float>> connectedComponents = CalculateStats();

            Console.WriteLine("Connected Components:");
            foreach (var component in connectedComponents)
            {
                Console.WriteLine("Nodes: " + string.Join(", ", component.Key));
                Console.WriteLine("Sum of Weights: " + component.Value);
                Console.WriteLine();
            }
        }
    }
}
