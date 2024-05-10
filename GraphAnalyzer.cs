using System;
using System.Collections.Generic;

namespace Plagiarism_Validation
{
    public class GraphAnalyzer
    {
        private Dictionary<int, List<Tuple<int, float,Edge>>> graph;
        public List<Tuple<float, Edge>> componentEdges;
        public Component component;
        public List<Component> groupComponents;
        public GraphAnalyzer()
        {
            graph = new Dictionary<int, List<Tuple<int, float,Edge>>>();
            
        }
        public void buildGraph(List<Edge> pairs)
        {
            foreach (var i in pairs)
            {
                int sourceId = i.Source.id;
                int destinationId = i.Destination.id;
                float weight = (i.firstSimilarity + i.secondSimilarity) / 2.0f;
                //Console.WriteLine($"First similarity: {i.firstSimilarity}, Second Similarity: {i.secondSimilarity}, Weight: {weight}");
                if (!graph.ContainsKey(sourceId))
                    graph[sourceId] = new List<Tuple<int, float,Edge>>();
                if (!graph.ContainsKey(destinationId))
                    graph[destinationId] = new List<Tuple<int, float,Edge>>();

                graph[sourceId].Add(new Tuple<int, float, Edge>(destinationId, weight,i));
                graph[destinationId].Add(new Tuple<int, float, Edge>(sourceId, weight,i));
            }
        }

        public List<GroupStatComponent> ConnectedComponentsWithSumAndEdgeCount()
        {
            var visited = new Dictionary<int, bool>();
            var componentsWithSumAndEdgeCount = new List<GroupStatComponent>();
            groupComponents = new List<Component>();

            // Initialize all vertices as not visited
            foreach (var vertex in graph.Keys)
            {
                visited[vertex] = false;
            }

            foreach (var vertex in graph.Keys)
            {
                if (!visited[vertex])
                {
                    var componentVertices = new List<int>();
                    float sum = 0;
                    int edgeCount = 0;
                    componentEdges = new List<Tuple<float, Edge>>();
                    DFS(vertex, visited, ref componentVertices, ref sum, ref edgeCount);
                    componentsWithSumAndEdgeCount.Add(new GroupStatComponent(componentVertices, sum, edgeCount));
                    component = new Component((float)(sum / edgeCount), componentEdges);
                    groupComponents.Add(component);
                }
            }

            return componentsWithSumAndEdgeCount;
        }

        private void DFS(int v, Dictionary<int, bool> visited, ref List<int> componentVertices, ref float componentSum, ref int edgeCount)
        {
            visited[v] = true;
            componentVertices.Add(v); // Add the current vertex to the component
                                      // Traverse through all adjacent vertices
            foreach (var neighbor in graph[v])
            {
                
                int neighborVertex = neighbor.Item1;
                float weight = neighbor.Item2;
                componentSum += weight; // Add weight of the current edge
                edgeCount++; // Increment edge count
                componentEdges.Add(new Tuple<float, Edge>(weight, neighbor.Item3));
                // If neighbor is not visited, recurse on it
                if (!visited[neighborVertex])
                {
                    DFS(neighborVertex, visited, ref componentVertices, ref componentSum, ref edgeCount);
                }
            }
        }




    }
}
