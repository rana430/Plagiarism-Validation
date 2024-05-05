using System;
using System.Collections.Generic;
using System.Linq;

namespace Plagiarism_Validation
{
    public class MST
    {
        private int[] id;
        public Tuple<long, Tuple<int, int, int>>[] p;
        public List<Tuple<long, Tuple<int, int>>> result;
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

        public long Kruskal(Tuple<long, Tuple<int, int, int>>[] p)
        {
            int x, y;
            long cost, maxCost = 0;
            int index = 0;

            result = new List<Tuple<long, Tuple<int, int>>>();
            idx = new List<int>();

            for (int i = 0; i < p.Length; ++i)
            {
                x = p[i].Item2.Item1;
                y = p[i].Item2.Item2;
                cost = p[i].Item1;
                index = p[i].Item2.Item3;

                if (Root(x) != Root(y))
                {
                    maxCost += cost;
                    Union(x, y);
                    result.Add(new Tuple<long, Tuple<int, int>>(cost, new Tuple<int, int>(x, y)));
                    idx.Add(index);
                }
            }
            return maxCost;
        }

        public long ConstructingMST(List<Edge> edges)
        {
            List<Tuple<long, Tuple<int, int, int>>> edgeList = new List<Tuple<long, Tuple<int, int, int>>>();

            foreach (var edge in edges)
            {
                int source = GetNodeId(edge.Source.path);
                int destination = GetNodeId(edge.Destination.path);
                int weight = Math.Max(edge.firstSimilarity, edge.secondSimilarity);
                int index = edge.lineMatches;
                edgeList.Add(new Tuple<long, Tuple<int, int, int>>(weight, new Tuple<int, int, int>(source, destination, index)));
            }

            edgeList.Sort((x, y) =>
            {
                int weightComparison = y.Item1.CompareTo(x.Item1);
                if (weightComparison == 0)
                {
                    int lineMatchesComparison = y.Item2.Item3.CompareTo(x.Item2.Item3);
                    if (lineMatchesComparison == 0)
                    {
                        return y.Item1.CompareTo(x.Item1);
                    }
                    return lineMatchesComparison;
                }
                return weightComparison;
            });

            p = edgeList.ToArray();

            Initialize(p.Length);
            long maxCost = Kruskal(p);

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
    }
}
