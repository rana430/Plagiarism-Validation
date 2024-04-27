using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiarism_Validation
{
    class MST
    {
        static int[] id;
        public Tuple<long, Tuple<int, int, int>>[] p;
        public List<Tuple<long, Tuple<int, int>>> result;
        public List<int> idx;


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
            // In the constructor or wherever you initialize 'result'
            result = new List<Tuple<long, Tuple<int, int>>>();
            idx = new List<int>();
            for (int i = 0; i < p.Length; ++i)
            {
                // Selecting edges one by one in increasing order from the beginning
                x = p[i].Item2.Item1;
                y = p[i].Item2.Item2;
                cost = p[i].Item1;
                index = p[i].Item2.Item3;
                // Check if the selected edge is creating a cycle or not
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
        public long ConstructingMST(HashSet<(string source, string destination, int weight, int index)> edges)
        {
            // Initialize a list to store edges in the format (weight, (source, destination, index))
            List<Tuple<long, Tuple<int, int, int>>> edgeList = new List<Tuple<long, Tuple<int, int, int>>>();

            // Convert edge format from (source, destination, weight) to (weight, (source, destination, index))
            foreach (var edge in edges)
            {
                int source = GetNodeId(edge.source);
                int destination = GetNodeId(edge.destination);
                int weight = edge.weight;
                int index = edge.index;
                edgeList.Add(new Tuple<long, Tuple<int, int, int>>(weight, new Tuple<int, int, int>(source, destination, index)));
            }


            // Sort edges by weight in descending order
            edgeList.Sort((x, y) => y.Item1.CompareTo(x.Item1));

            // Convert list of tuples to array
            p = edgeList.ToArray();
            /*foreach (var i in p)
            {
                // Using string format
                Console.WriteLine("{0} ({1}, {2})", i.Item1, i.Item2.Item1, i.Item2.Item2);
                // Or concatenating into a single string
                // Console.WriteLine(i.Item1 + " (" + i.Item2.Item1 + ", " + i.Item2.Item2 + ")");
            }*/


            // Initialize the id array
            Initialize(p.Length);

            // Apply Kruskal's algorithm to find the maximum spanning tree cost
            long maxCost = Kruskal(p);

            return maxCost;
        }

        // Helper method to get the unique node id for each node (source or destination)
        public Dictionary<string, int> nodeIdMap = new Dictionary<string, int>();
        public int GetNodeId(string nodeName)
        {
            if (!nodeIdMap.ContainsKey(nodeName))
            {
                nodeIdMap[nodeName] = nodeIdMap.Count;
            }
            return nodeIdMap[nodeName];
        }
        public int getResultSize()
        {
            return idx.ToArray().Length;
        }

   






    }
}
