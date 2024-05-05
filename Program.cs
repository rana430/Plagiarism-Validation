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

            string filePath = @"F:\FCIS\Level 3\Second term\Algorithms\Project\[3] Plagiarism Validation\Test Cases\Complete\Easy\1-Input.xlsx";
            //Console.WriteLine("hello");
            // Read the file pairs and percentages from the Excel file
            List<Edge> pairs = Excel.ReadFilePairs(filePath);

            //foreach (var pair in pairs)
            //{
            //    //Console.WriteLine("hello");
            //    Console.WriteLine($"File1: {pair.Source}, Percentage1: {pair.firstSimilarity}%, File2: {pair.Source}, Percentage2: {pair.secondSimilarity}%");
            //}
            MST_Graph mst_graph = new MST_Graph();
            mst_graph.BuildMSTGraph(pairs);
            mst_graph.PrintGraph();

            // Print the graph (optional)
            //graphBuilder.PrintGraph();
            MST test = new MST();
            long cost = test.ConstructingMST(mst_graph.edges);
            Console.WriteLine(cost);
            foreach (var i in test.result)
            {
                Console.WriteLine("{0} ({1}, {2})", i.Item1, test.nodeIdMap.FirstOrDefault(x => x.Value == i.Item2.Item1).Key, test.nodeIdMap.FirstOrDefault(x => x.Value == i.Item2.Item2).Key);
            }
            //foreach (var i in test.idx)
            //{
            //    Console.WriteLine(i);
            //}
            //ExcelPackage packageExcel = Excel.GetPackage(filePath);
            //ExcelWorksheet worksheet = packageExcel.Workbook.Worksheets[0];

            //if (worksheet != null)
            //{
            //    // Access the rows of the worksheet
            //    int rowCount = worksheet.Dimension.Rows;
            //    int columnCount = worksheet.Dimension.Columns;

            //    foreach (var row in test.idx)
            //    {
            //        for (int col = 1; col <= columnCount; col++)
            //        {
            //            // Access cell value
            //            string cellValue = worksheet.Cells[row, col].Value?.ToString();
            //            Console.Write(cellValue + "\t");
            //        }
            //        Console.WriteLine(); // Move to the next row
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Failed to open the worksheet.");
            //}
            //Console.ReadLine();
        }
    }
}
