using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiarism_Validation
{
    class Graph
    {

        public List<Node> nodes;
        public List<Edge> edges;

        public Graph()
        {
            nodes = new List<Node>();
            edges = new List<Edge>();

        public string root_node;
        public Dictionary<KeyValuePair<string, string>, int> edgesDFS;
        public Dictionary<string, char> color;
        private HashSet<(string source, string destination, int weight)> edges;
        public Dictionary<string, List<string>> adj_list;
        private bool root = false;
        public GraphBuilder()
        {
            edgesDFS = new Dictionary<KeyValuePair<string, string>, int>();
            edges = new HashSet<(string, string, int)>();
            color = new Dictionary<string , char>();
            adj_list = new Dictionary<string, List<string>>();
        }

        public void BuildGraph(List<(string file1, int percentage1, string file2, int percentage2)> pairs)
        {
            
            foreach (var (file1, percentage1, file2, percentage2) in pairs)
            {
                if (root == false)
                {
                    root_node = file1;
                    root = true;
                }
                color[file1] = 'w';
                color[file2] = 'w';
                int percentage = (percentage1 + percentage2) / 2;
                AddEdge(file1, file2, percentage,1);
                AddEdge(file2, file1, percentage,1);
                try
                {
                    adj_list[file1].Add(file2);
                }
                catch
                {
                    adj_list[file1] = new List<string>();
                    adj_list[file1].Add(file2);
                }
                try
                {
                    adj_list[file2].Add(file1);
                }
                catch
                {
                    adj_list[file2] = new List<string>();
                    adj_list[file2].Add(file1);
                }
            }
            
        }


        
        public void BuildMSTGraph(List<(string file1, int percentage1, string file2, int percentage2)> pairs)
        {
            foreach (var (file1, percentage1, file2, percentage2) in pairs)
            {
                int max_percentage = Math.Max(percentage1, percentage2);
                AddEdge(file1, file2, max_percentage);
                AddEdge(file2, file1, max_percentage);
            }
        }

        private void AddEdge(string source, string destination, int weight)
        {
            edges.Add((source, destination, weight));
        }
        private void AddEdge(string source, string destination, int weight , short dfs)
        {
            KeyValuePair<string, string> key = new KeyValuePair<string, string>(source, destination);
            edgesDFS[key] = weight;
        }
        public void PrintGraph()
        {
            foreach (var edge in edges)
            {
                Console.WriteLine($"From {edge.source} to {edge.destination} with weight {edge.weight}");
            }

        }
        
     
        
    }
}
