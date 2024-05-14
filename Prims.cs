using System;
using System.Collections.Generic;
using System.Linq;

namespace Plagiarism_Validation
{
    public class Prims
    {
        private int[] _key;
        private bool[] _visited;
        private int[] _parent;
        public List<Tuple<long, Edge>> Result;
        public Dictionary<string, int> NodeIdMap;

        public Prims()
        {
            NodeIdMap = new Dictionary<string, int>();
        }

        public void Initialize(int size)
        {
            _key = new int[size + 1];
            _visited = new bool[size + 1];
            _parent = new int[size + 1];
            Result = new List<Tuple<long, Edge>>();

            for (int i = 0; i <= size; ++i)
            {
                _key[i] = int.MinValue;
                _visited[i] = false;
                _parent[i] = -1;
            }
        }

        public void ConstructMSTForComponent(List<Edge> componentEdges)
        {
            int componentSize = componentEdges.Count;
            Initialize(componentSize + 5);
            

            // Prim's algorithm for maximum spanning tree
            for (int i = 0; i <= componentSize; i++)
            {
                int u = MaxKey();
                _visited[u] = true;

                foreach (var edge in componentEdges)
                {
                    int v = edge.Destination.id;
                    int weight = Math.Max(edge.firstSimilarity, edge.secondSimilarity); // Use Math.Max for weight

                    if (!_visited[v] && weight > _key[v])
                    {
                        _parent[v] = u;
                        _key[v] = weight;
                    }
                }
            }

            // Add edges to result
            for (int i = 1; i <= componentSize; i++)
            {
                if (_parent[i] != -1)
                {
                   int u = _parent[i];
                    Edge edge = componentEdges[i];
                    Result.Add(new Tuple<long, Edge>(Math.Max(edge.firstSimilarity, edge.secondSimilarity), edge));
                }
            }

            // Print max cost for the component
            long maxCost = Result.Sum(tuple => tuple.Item1);
            Console.WriteLine($"Max Cost for Component: {maxCost}");
        }


        private int MaxKey()
        {
            int max = int.MinValue;
            int maxIndex = 0;

            for (int v = 1; v < _key.Length; ++v)
            {
                if (!_visited[v] && _key[v] > max)
                {
                    max = _key[v];
                    maxIndex = v;
                }
            }

            return maxIndex;
        }

        public int GetNodeId(string nodeName)
        {
            if (!NodeIdMap.ContainsKey(nodeName))
            {
                NodeIdMap[nodeName] = NodeIdMap.Count;
            }
            return NodeIdMap[nodeName];
        }
    }
}
