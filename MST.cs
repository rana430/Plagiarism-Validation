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
        public List<Component> MstComponents = new List<Component>();

      

        public void Initialize(int size)//O(E)
        {
            id = new int[size];
            for (int i = 0; i < size ; ++i)
            {
                id[i] = i;
            }
        }

        public int Root(int x)//O(log V)
        {
            while (id[x] != x)
            {
                id[x] = id[id[x]];
                x = id[x];
            }
            return x;
        }

        public void Union(int x, int y)//O(log V)
        {
            int p = Root(x);
            int q = Root(y);
            id[p] = id[q];
        }

        public long Kruskal(Tuple<long, Edge>[] p, Component component)//O(E log V)
        {
            int x, y;
            long cost, maxCost = 0;
            component.MstEdges = new List<Tuple<long, Edge>>();
            
            for (int i = 1; i < p.Length; ++i)
            {
                x = p[i].Item2.Source.id;
                y = p[i].Item2.Destination.id;
                cost = p[i].Item1;
                if (Root(x) != Root(y))//Log(V)
                {
                    maxCost += cost;
                    Union(x, y);//Log (V)
                    component.MstEdges.Add(new Tuple<long, Edge>(cost, p[i].Item2));
                }
            }
            return maxCost;
        }

        public long ConstructingMST(List<Component> components,List<Edge>edges) //sumation of C (O(E log E + E logV ))
        {
            
            MstComponents = components;
            long maxCost=0;
            Initialize(edges.Count*2);//O(e)
            foreach (var component in components)//O(C)
            {
                List<Tuple<long, Edge>> edgeList = new List<Tuple<long, Edge>>();//change it to int 
                List<Tuple<float, Edge>> componentEdge = new List<Tuple<float, Edge>>();
                componentEdge= component.edges;
                foreach (var item in componentEdge)// O(e)
                {
                    int weight = item.Item2.maxSimilarity;
                    edgeList.Add(new Tuple<long, Edge>(weight, item.Item2));
                }

                edgeList.Sort((x, y) =>
                {
                    // First, compare by weight
                    int weightComparison = y.Item1.CompareTo(x.Item1);

                    // If weights are equal, compare by line matches
                    if (weightComparison == 0)
                    {
                        // Compare by line matches
                        return y.Item2.lineMatches.CompareTo(x.Item2.lineMatches);
                    }

                    // Return the result of the weight comparison
                    return weightComparison;
                });//o(e log e)

                p = new Tuple<long, Edge>[edgeList.Count];//we convert it to array as we dont take the first edge also sorting list is faster than sorting array
                p = edgeList.ToArray();//O(e)

                maxCost += Kruskal(p,component);//O(e log V)

            }
            return maxCost;
        }

     
        public List<Component> GetComponents() //O(C log c)
        {
          
            // Convert the dictionary values to a list of components
            MstComponents.Sort((c1, c2) => c2.avgSim.CompareTo(c1.avgSim));//O(C log c)

            return MstComponents;
        }



    }
}
