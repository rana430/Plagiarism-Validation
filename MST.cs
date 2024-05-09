using System;
using System.Collections.Generic;
using System.Linq;

namespace Plagiarism_Validation
{
    public class MST
    {
        private int[] id;
        public Tuple<long, Edge>[] p;
        public List<Tuple<long, Edge>> result;
        public List<int> idx;
        public Dictionary<string, int> nodeIdMap;

        public MST()
        {
            nodeIdMap = new Dictionary<string, int>();
        }

        public void Initialize(int size)
        {
            id = new int[size];
            for (int i = 0; i < size; ++i)
                id[i] = i;
        }

        public int Root(int x)
        {
            while (id[x] != x)
            {
                id[x] = id[id[x]];
                x = id[x];
            }
            return x;
        }

        public void Union(int x, int y)
        {
            int p = Root(x);
            int q = Root(y);
            id[p] = id[q];
        }

        public long Kruskal(Tuple<long, Edge>[] p)
        {
            int x, y;
            long cost, maxCost = 0;
            int index = 0;

            result = new List<Tuple<long,Edge>>();
            idx = new List<int>();

            for (int i = 1; i < p.Length; ++i)
            {
                x = p[i].Item2.Source.id;
                y = p[i].Item2.Destination.id;
                cost = p[i].Item1;
                index = p[i].Item2.lineMatches;

                if (Root(x) != Root(y))
                {
                    maxCost += cost;
                    Union(x, y);
                    result.Add(new Tuple<long, Edge>(cost, p[i].Item2));
                    idx.Add(index);
                }
            }
            return maxCost;
        }

        public long ConstructingMST(List<Edge> edges) //O(e log e)
        {
            List<Tuple<long, Edge>> edgeList = new List<Tuple<long, Edge>>();

            foreach (var edge in edges)// O(e)
            {
                int source = edge.Source.id;
                int destination = edge.Destination.id;
                int weight = Math.Max(edge.firstSimilarity, edge.secondSimilarity);
                int index = edge.lineMatches;
                edgeList.Add(new Tuple<long, Edge>(weight, edge));
            }

            edgeList.Sort((x, y) => y.Item1.CompareTo(x.Item1)); //o(e log e)


            p = edgeList.ToArray();//O(e)
           /* foreach( var i in p)
            {
                Console.WriteLine($"    Node 1: {i.Item2.Source.id}, Node 2: {i.Item2.Destination.id}, Weight: {i.Item2.lineMatches}, cost: {i.Item1}");
            }
*/
            Initialize(p.Length);//O(e)
            long maxCost = Kruskal(p);//O(e log N)

            return maxCost;
        }

        public int GetNodeId(string nodeName)
        {
            if (!nodeIdMap.ContainsKey(nodeName))
            {
                nodeIdMap[nodeName] = nodeIdMap.Count;
            }
            return nodeIdMap[nodeName];
        }
        public void sortResult()
        {
            result.Sort();

        }

        public int GetResultSize()
        {
            return idx.Count;
        }
        public void printResult()
        {
            foreach (var tuple in result)
            {
                Console.WriteLine($"{tuple.Item1}, {tuple.Item2}");
            }
        }
        public List<Component> GetComponents()
        {
            // Create a dictionary to store components indexed by their root
            Dictionary<int, Component> rootToComponent = new Dictionary<int, Component>();

            foreach (var tuple in result)//O(
            {
                int rootX = Root(tuple.Item2.Source.id);
                int rootY = Root(tuple.Item2.Destination.id);

                // If both nodes belong to the same root, add the tuple to the component
                if (rootX == rootY)
                {
                    int root = rootX;
                    if (!rootToComponent.ContainsKey(root))//O(1)
                    {
                        rootToComponent[root] = new Component(0, new List<Tuple<long, Edge>>());
                    }
                    rootToComponent[root].AddTuple(tuple);//O(1)
                }
            }

            // Convert the dictionary values to a list of components
            List<Component> components = rootToComponent.Values.ToList();//O(C)
            components.Sort((c1, c2) => c1.weight.CompareTo(c2.weight));//O(C)

            return components;
        }



    }
}
