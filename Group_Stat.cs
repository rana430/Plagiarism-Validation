using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plagiarism_Validation;



namespace Plagiarism_Validation
{
    internal class Group_Stat
    {
        private GraphBuilder Graph;
        private List<KeyValuePair<List<string>, int>> cycles;
        private static int cycleIndex = 0;
        Group_Stat(List<(string file1, int percentage1, string file2, int percentage2)> pairs)
        {
            Graph = new GraphBuilder();
            Graph.BuildGraph(pairs);

        }
        //Performs DFS to finds Cycles and gets average similarity in one cycle 
        void Averge_similarity(string node)
        {
            Dictionary<string, string> p = new Dictionary<string, string>();
            bool found = false;
            while (true)
            {
                found = false;
                dfs(node, null, p);
                foreach (var v in Graph.color)
                {
                    if (v.Value == 'w')
                    {
                        node = v.Key;
                        found = true;
                        break;
                    }
                }
                if (!found)
                    break;
            }
        }
        public List<KeyValuePair<List<string>, int>> calculate_stats()
        {
            Averge_similarity(Graph.root_node);
            return cycles;
        }
        private void dfs(string node, string parent, Dictionary<string, string> parents)
        {
            //visted the node
            if (Graph.color[node] == 'b')
            {
                return;
            }
            //partially visited
            if (Graph.color[node] == 'g')
            {
                List<string> cycle_comp = new List<string>();
                KeyValuePair<string, string> edge = new KeyValuePair<string, string>(node, parent);
                string curr = parent;
                int edge_cost = 0;
                edge_cost += Graph.edgesDFS[edge];
                cycle_comp.Add(curr);
                while (curr != node)
                {
                    curr = parents[curr];
                    edge = new KeyValuePair<string, string>(curr, parents[curr]);
                    edge_cost += Graph.edgesDFS[edge];
                    cycle_comp.Add(curr);

                }
                KeyValuePair<List<string>, int> key = new KeyValuePair<List<string>, int>(cycle_comp, edge_cost);
                cycles.Add(key);
                cycleIndex++;
                return;
            }
            parents[node] = parent;
            Graph.color[node] = 'g';
            foreach (var v in Graph.adj_list[node])
            {
                if (node == parent)
                    continue;
                dfs(v, node, parents);

            }
            Graph.color[node] = 'b';
        }



        /* static List<List<string>> GetGroups()
         {
             HashSet<string> visited = new HashSet<string>();
             List<List<string>> groups = new List<List<string>>();

             foreach (var node in graph.Keys)
             {
                 if (!visited.Contains(node))
                 {
                     List<string> group = new List<string>();
                     dfs(node, visited, group);
                     groups.Add(group);
                 }
             }
 */

        /*private void dfs(string node,List<int> cost , HashSet<string> visited)
        {
        visited.Remove(node);
        if (Graph.ContainsKey(node))

            foreach (var neighbor in Graph.adj_list[node])
            {
                if (visited.Contains(neighbor))
                {   
                    dfs(neighbor,cost, visited);
                }
            }
        }*/

        /* private int dfs(string start_node , string node ,ref int cost,int zero_cost , bool back_track)
         {
             //base case 
             if (node == start_node && Graph.color[node] == 'b')
             {
                 back_track = false;
                 return zero_cost;
             }
             Graph.color[node] = 'b';

             foreach(var neighbor in Graph.adj_list[node])
             {
                if(Graph.color[neighbor] == 'w' && neighbor != start_node)
                 {
                     int cost_curr;
                     KeyValuePair<string, string> key = new KeyValuePair<string, string>(node, neighbor);
                     cost_curr = Graph.edgesDFS[key];
                     cost += dfs(start_node, neighbor,ref cost, cost_curr,back_track);
                     if(back_track == false)
                     {
                         break;
                     }
                 }
             }
             return 0;
         }
     }
     */


    }
}