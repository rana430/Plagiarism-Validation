using System;
using System.Collections.Generic;
using System.Linq;

namespace Plagiarism_Validation
{
    public class MST
    {
        public int[] id;
        public Tuple<long, Edge>[] p;
        public List<Tuple<long, Edge>> result;
        public List<int> idx;
        public Dictionary<string, int> nodeIdMap;
        public List<Component> MstComponents = new List<Component>();

        public MST()
        {
            nodeIdMap = new Dictionary<string, int>();
        }

        public void Initialize(int size)//O()
        {
            id = new int[size];
            for (int i = 0; i < size ; ++i)
            {
                id[i] = i;
            }
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

        public long Kruskal(Tuple<long, Edge>[] p, Component component)//O(E log N)
        {
            int x, y;
            long cost, maxCost = 0;
            component.nodes = new List<Tuple<long, Edge>>();
            
            for (int i = 1; i < p.Length; ++i)
            {
                x = p[i].Item2.Source.id;
                y = p[i].Item2.Destination.id;
                cost = p[i].Item1;
                if (Root(x) != Root(y))
                {
                    maxCost += cost;
                    Union(x, y);
                    component.nodes.Add(new Tuple<long, Edge>(cost, p[i].Item2));
                }
            }
            return maxCost;
        }

        public long ConstructingMST(List<Component> components,List<Edge>edges) //sumation of C (O(e log e))
        {
            
            MstComponents = components;
            long maxCost=0;
            Initialize(edges.Count);//O(e)
            foreach (var component in components)//O(C)
            {
                List<Tuple<long, Edge>> edgeList = new List<Tuple<long, Edge>>();
                List<Tuple<float, Edge>> componentEdge = new List<Tuple<float, Edge>>();
                componentEdge= component.edges;
                foreach (var item in componentEdge)// O(e)
                {
                    int weight = Math.Max(item.Item2.firstSimilarity, item.Item2.secondSimilarity);
                    edgeList.Add(new Tuple<long, Edge>(weight, item.Item2));
                }

                edgeList.Sort((x, y) => y.Item1.CompareTo(x.Item1)); //o(e log e)

                p = new Tuple<long, Edge>[edgeList.Count];
                p = edgeList.ToArray();//O(e)
                /* foreach( var i in p)
                 {
                     Console.WriteLine($"    Node 1: {i.Item2.Source.id}, Node 2: {i.Item2.Destination.id}, Weight: {i.Item2.lineMatches}, cost: {i.Item1}");
                 }
     */
                Console.WriteLine(p.Length);

                
                maxCost += Kruskal(p,component);//O(e log N)

            }
            return maxCost;
        }

        /*public int GetNodeId(string nodeName)
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
        }*/
        public List<Component> GetComponents() //O(C)
        {
          
            // Convert the dictionary values to a list of components
            MstComponents.Sort((c1, c2) => c2.avgSim.CompareTo(c1.avgSim));//O(C)

            return MstComponents;
        }



    }
}
