using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiarism_Validation
{
    class Program
    {
        static void Main(string[] args)
        {
            /*// Path to your Excel file
            string filePath = @"D:\uni\Algo\file.xlsx";
            Excel excel = new Excel();
            // Read the Excel file using the Excel class
            Excel.Read(filePath);*/

            string filePath = @"F:\FCIS\Level 3\Second term\Algorithms\Project\[3] Plagiarism Validation\Test Cases\Sample\2-Input.xlsx";
            //Console.WriteLine("hello");
            // Read the file pairs and percentages from the Excel file
            List<(string file1, int percentage1, string file2, int percentage2)> pairs = Excel.ReadFilePairs(filePath);
 
            /*foreach (var pair in pairs)
            {
                //Console.WriteLine("hello");
                Console.WriteLine($"File1: {pair.file1}, Percentage1: {pair.percentage1}%, File2: {pair.file2}, Percentage2: {pair.percentage2}%");
            }*/
            GraphBuilder graphBuilder = new GraphBuilder();
            graphBuilder.BuildMSTGraph(pairs);

            // Print the graph (optional)
            graphBuilder.PrintGraph();
            MST test = new MST();
            long cost = test.ConstructingMST(graphBuilder.edges);
            Console.WriteLine(cost);
            foreach(var i in test.result)
            {
                Console.WriteLine("{0} ({1}, {2})", i.Item1, test.nodeIdMap.FirstOrDefault(x => x.Value == i.Item2.Item1).Key, test.nodeIdMap.FirstOrDefault(x => x.Value == i.Item2.Item2).Key);
            }
            //Console.ReadLine();
        }
    }
}
