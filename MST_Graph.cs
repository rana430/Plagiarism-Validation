using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiarism_Validation
{
    public class MST_Graph
    {
        public HashSet<(string source, string destination, int weight, int index)> edges;

        public MST_Graph()
        {
            edges = new HashSet<(string, string, int, int)>();
        }

        public void BuildMSTGraph(List<(string file1, int percentage1, string file2, int percentage2,int idx)> pairs)
        {
            int cnt = 1;
            foreach (var (file1, percentage1, file2, percentage2,idx) in pairs)
            {
                int max_percentage = Math.Max(percentage1, percentage2);
                AddEdge(file1, file2, max_percentage, idx);
                AddEdge(file2, file1, max_percentage, idx);
                
            }
        }

        private void AddEdge(string source, string destination, int weight, int index)
        {
            edges.Add((source, destination, weight, index));
        }

        public void PrintGraph()
        {
            foreach (var edge in edges)
            {
                Console.WriteLine($"From {edge.source} to {edge.destination} with weight {edge.weight} (Index: {edge.index})");
            }
        }
    }

}
