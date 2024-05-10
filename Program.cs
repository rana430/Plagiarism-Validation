using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

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

            string filePath = @"F:\FCIS\Level 3\Second term\Algorithms\Project\[3] Plagiarism Validation\Test Cases\Complete\Hard\2-Input.xlsx";
            //Console.WriteLine("hello");
            // Read the file pairs and percentages from the Excel file
            List<Edge> pairs = Excel.ReadFilePairs(filePath);

            //foreach (var pair in pairs)
            //{
            //    //Console.WriteLine("hello");
            //    Console.WriteLine($"File1: {pair.Source}, Percentage1: {pair.firstSimilarity}%, File2: {pair.Source}, Percentage2: {pair.secondSimilarity}%");
            //}
            
            //test group stat
            GraphAnalyzer graph = new GraphAnalyzer();
            graph.buildGraph(pairs);
            List<GroupStatComponent> sums = graph.ConnectedComponentsWithSumAndEdgeCount();
            sums.Sort((x, y) => (y.avgSimSum/(y.edgeCount)).CompareTo(x.avgSimSum / (x.edgeCount)));
            foreach (var i in sums)
            {
                /*Console.Write("Component: ");
                foreach (var e in i.edges)
                {
                    Console.Write($"{e} ");
                }*/
                Console.WriteLine();
                Console.WriteLine($"Sum of weights: {i.avgSimSum / (i.edgeCount)}");
                Console.WriteLine($"Number of edges: {i.edges.Count}");
            }
            List<Component> groupStat = new List<Component>();
            groupStat = graph.groupComponents;



            //Mst test
            MST_Graph mst_graph = new MST_Graph();
            mst_graph.BuildMSTGraph(pairs);
            // mst_graph.PrintGraph();

            // Print the graph (optional)
            //graphBuilder.PrintGraph();
            MST test = new MST();
            long cost = test.ConstructingMST(groupStat, mst_graph.edges);
            Console.WriteLine(cost);
            //foreach (var i in test.result)
            //{
            //    Console.WriteLine("{0} ({1}, {2})", i.Item1, test.nodeIdMap.FirstOrDefault(x => x.Value == i.Item2.Item1).Key, test.nodeIdMap.FirstOrDefault(x => x.Value == i.Item2.Item2).Key);
            //}
            List<Component> components = test.GetComponents();

            foreach (var i in components)
            {
                i.SortEdgesByLineMatches();
                i.PrintComponent();
            }

            //prims test 

            /*Prims prims = new Prims();
            prims.ConstructMSTForComponent(pairs);
            foreach(var i in prims.Result)
            {
                Console.WriteLine(i.Item1);
                Console.WriteLine(i.Item2.Source.id);
                Console.WriteLine(i.Item2.Destination.id);
                Console.WriteLine("**********");

            }*/


        }
    }
}
