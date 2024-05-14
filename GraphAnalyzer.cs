using System;
using System.Collections.Generic;

namespace Plagiarism_Validation
{
    public class GraphAnalyzer //change its name
    {
        private Dictionary<int, HashSet<Edge>> graph;//adj matrix for graph
        private HashSet<int> visited;//list contains only visited nodes
        private List<Tuple<float, Edge>> componentEdges; //list contains the edges of the created component --each component has its own--

        public List<Component> components;//list of components constructed
        public int maxId;//max number generated for the input file --not used--

        public GraphAnalyzer()
        {
            maxId = StringIdAssigner.currentId;
            graph = new Dictionary<int, HashSet<Edge>>();
            visited = new HashSet<int>();
        }

        public void buildGraph(List<Edge> pairs) // build unDirected Graph in O(E)
        {
            foreach (var edge in pairs)
            {
                int sourceId = edge.Source.id;
                int destinationId = edge.Destination.id;

                if (!graph.ContainsKey(sourceId))
                    graph[sourceId] = new HashSet<Edge>();

                if (!graph.ContainsKey(destinationId))
                    graph[destinationId] = new HashSet<Edge>();

                graph[sourceId].Add(edge);
                graph[destinationId].Add(edge);
            }
        }

        public List<Component> ConstructComponent()//O(V+E)
        {
            components = new List<Component>();

            foreach (var vertex in graph.Keys)
            {
                if (!visited.Contains(vertex))//O(V)
                {
                    var componentVertices = new SortedSet<string>();
                    float sum = 0;
                    int edgeCount = 0;
                    componentEdges = new List<Tuple<float, Edge>>();//init new one for each component --we can remove float--

                    DFS(vertex, ref componentVertices, ref sum, ref edgeCount);//O(E)

                    //componentsWithSumAndEdgeCount.Add(new GroupStatComponent(componentVertices, sum, edgeCount));
                    components.Add(new Component((float)(sum / edgeCount), componentEdges,componentVertices,edgeCount));
                }
            }

            return components;
        }

        private void DFS(int v, ref SortedSet<string> componentVertices, ref float componentSum, ref int edgeCount)
        {
            visited.Add(v);
            componentVertices.Add(StringIdAssigner.GetString(v));

            foreach (var edge in graph[v])
            {
                int neighborVertex = (edge.Source.id == v) ? edge.Destination.id : edge.Source.id;//this line is bec we pass edge not destination for the graph
                float weight = edge.edgeAvgSim;

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
