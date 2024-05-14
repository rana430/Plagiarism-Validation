using System;
using System.Collections.Generic;

namespace Plagiarism_Validation
{
    public class GraphAnalyzer
    {
        private Dictionary<int, HashSet<Edge>> graph;
        private HashSet<int> visited;
        private List<Tuple<float, Edge>> componentEdges;

        public List<Component> components;
        public int maxId;

        public GraphAnalyzer()
        {
            maxId = StringIdAssigner.currentId;
            graph = new Dictionary<int, HashSet<Edge>>();
            visited = new HashSet<int>();
        }

        public void buildGraph(List<Edge> pairs)
        {
            foreach (var edge in pairs)
            {
                int sourceId = edge.Source.id;
                int destinationId = edge.Destination.id;
                float weight = (edge.firstSimilarity + edge.secondSimilarity) / 2.0f;

                if (!graph.ContainsKey(sourceId))
                    graph[sourceId] = new HashSet<Edge>();

                if (!graph.ContainsKey(destinationId))
                    graph[destinationId] = new HashSet<Edge>();

                graph[sourceId].Add(edge);
                graph[destinationId].Add(edge);
            }
        }

        public List<Component> ConstructComponent()
        {

            components = new List<Component>();

            foreach (var vertex in graph.Keys)
            {
                if (!visited.Contains(vertex))
                {
                    var componentVertices = new SortedSet<int>();
                    float sum = 0;
                    int edgeCount = 0;
                    componentEdges = new List<Tuple<float, Edge>>();

                    DFS(vertex, ref componentVertices, ref sum, ref edgeCount);

                    components.Add(new Component((float)(sum / edgeCount), componentEdges,componentVertices,edgeCount));
                }
            }

            return components;
        }

        private void DFS(int v, ref SortedSet<int> componentVertices, ref float componentSum, ref int edgeCount)
        {
            visited.Add(v);
            componentVertices.Add(v);

            foreach (var edge in graph[v])
            {
                int neighborVertex = (edge.Source.id == v) ? edge.Destination.id : edge.Source.id;
                float weight = (edge.Source.id == v) ? edge.secondSimilarity : edge.firstSimilarity;

                componentSum += weight;
                edgeCount++;
                componentEdges.Add(new Tuple<float, Edge>(weight, edge));

                if (!visited.Contains(neighborVertex))
                {
                    DFS(neighborVertex, ref componentVertices, ref componentSum, ref edgeCount);
                }
            }
        }
    }
}
