using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            // Start the stopwatch for total time
            Stopwatch totalStopwatch = new Stopwatch();
            totalStopwatch.Start();

            // Start the stopwatch for GroupAnalyzer time
            Stopwatch groupAnalyzerStopwatch = new Stopwatch();
            Stopwatch mstStopwatch = new Stopwatch();
            
            

            // Read pairs from file
            string filePath = @"F:\FCIS\Level 3\Second term\Algorithms\Project\[3] Plagiarism Validation\Test Cases\Complete\Hard\1-Input.xlsx";
            List<Edge> pairs = Excel.ReadFilePairs(filePath);
            mstStopwatch.Stop();
            // Analyze groups
            GraphAnalyzer graph = new GraphAnalyzer();
            graph.buildGraph(pairs);
            // Start the stopwatch for GroupAnalyzer time
            groupAnalyzerStopwatch.Start();
            List<Component> sums = graph.ConstructComponent();
            //sort components
            sums.Sort((x, y) => (y.avgSim).CompareTo(x.avgSim));
            StatExcelWriter statexcelWriter = new StatExcelWriter();
            string statOutFilePath = @"F:\FCIS\Level 3\Second term\Algorithms\Project\[3] Plagiarism Validation\Test Cases\Sample\stat.xlsx";
            statexcelWriter.WriteToExcel(statOutFilePath, sums);

            // Stop the stopwatch for GroupAnalyzer time
            groupAnalyzerStopwatch.Stop();
            
            // Build MST
            /*MST_Graph mst_graph = new MST_Graph();
            mst_graph.BuildMSTGraph(pairs);*/

            // Start the stopwatch for MST time
            mstStopwatch.Start();
            MST test = new MST();
            long cost = test.ConstructingMST(graph.components, pairs);
            List<Component> components = test.GetComponents();

            foreach (var i in components)
            {
                i.SortEdgesByLineMatches();
            }

            MSTExcelWriter excelWriter = new MSTExcelWriter();
            string outFilePath = @"F:\FCIS\Level 3\Second term\Algorithms\Project\[3] Plagiarism Validation\Test Cases\Sample\MST.xlsx";
            excelWriter.WriteToExcel(outFilePath, components);

            // Stop the stopwatch for MST time
            mstStopwatch.Stop();

            // Stop the stopwatch for total time
            totalStopwatch.Stop();

            // Get the elapsed time for each component
            long groupAnalyzerElapsedTime = groupAnalyzerStopwatch.ElapsedMilliseconds;
            long mstElapsedTime = mstStopwatch.ElapsedMilliseconds;
            long totalElapsedTime = totalStopwatch.ElapsedMilliseconds;

            // Print the elapsed time for each component
            Console.WriteLine($"GroupAnalyzer Time: {groupAnalyzerElapsedTime} milliseconds");
            Console.WriteLine($"MST Time: {mstElapsedTime} milliseconds");
            Console.WriteLine($"Total Time: {totalElapsedTime} milliseconds");

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
