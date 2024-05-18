using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using OfficeOpenXml;

namespace Plagiarism_Validation
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, string> testTypes = new Dictionary<int, string>
            {
                { 1, @"F:\FCIS\Level 3\Second term\Algorithms\Project\[3] Plagiarism Validation\Test Cases\Sample" },
                { 2, @"F:\FCIS\Level 3\Second term\Algorithms\Project\[3] Plagiarism Validation\Test Cases\Complete" }
            };

            Dictionary<int, string> testLevels = new Dictionary<int, string>
            {
                { 1, "Easy" },
                { 2, "Medium" },
                { 3, "Hard" }
            };

            bool continueProgram = true; // Assign an initial value to continueProgram
            do
            {
                Console.Clear();
                Console.WriteLine("Test Cases: Select Test case type => 1- Sample   2- Complete");
                if (!int.TryParse(Console.ReadLine(), out int choice) || (choice != 1 && choice != 2))
                {
                    Console.WriteLine("Invalid input. Enter 1 or 2!!");
                    continue; // Skip to the next iteration of the loop
                }

                string folderPath = testTypes[choice];
                int testLevelChoice = 1;
                if (choice == 2)
                {
                    Console.WriteLine("Choose Test Level:\n1- Easy\n2- Medium\n3- Hard");
                    if (!int.TryParse(Console.ReadLine(), out testLevelChoice) || (testLevelChoice < 1 || testLevelChoice > 3))
                    {
                        Console.WriteLine("Invalid input. Enter 1, 2 or 3!!");
                        continue; // Skip to the next iteration of the loop
                    }
                    folderPath = Path.Combine(folderPath, testLevels[testLevelChoice]);
                }

                ProcessFilesInFolder(folderPath, testTypes[choice], testLevels[testLevelChoice]);

                Console.WriteLine("Do you want to process another folder? (y/n)");
                continueProgram = Console.ReadLine().ToLower() == "y";
            } while (continueProgram);
        }
        
        static void ProcessFilesInFolder(string folderPath, string testType, string testLevel)
        {
            // Get all Excel files in the folder
            string[] excelFiles = Directory.GetFiles(folderPath, "*input.xlsx");
            int idx = 1;//file number 
            foreach (var filePath in excelFiles)
            {
                ProcessFile(filePath, testType, testLevel, idx++);
            }
        }

        static void ProcessFile(string filePath, string testType, string testLevel, int idx)
        {
            TestFile testFile = new TestFile(testType, testLevel, filePath);
            testFile.totalTimeStopwatch.Start();

            List<Edge> pairs = Excel.ReadFilePairs(filePath);
            testFile.GroupingTimeStopwatch.Start();

            // Analyze groups
            GraphAnalyzer graph = new GraphAnalyzer();
            graph.buildGraph(pairs);
            // Start the stopwatch for GroupAnalyzer time
            testFile.GroupingTimeStopwatch.Start();
            List<Component> components = graph.ConstructComponent();
            //sort components
            testFile.MSTTimeStopwatch.Start();
            components.Sort((x, y) => ((y.avgSim / y.edgeCount).CompareTo(x.avgSim / x.edgeCount)));//O(C log C)
            testFile.MSTTimeStopwatch.Stop();
            // Stop the stopwatch for GroupAnalyzer time
            testFile.GroupingTimeStopwatch.Stop();

            // Start the stopwatch for MST time
            testFile.MSTTimeStopwatch.Restart();
            MST test = new MST();
            long cost = test.ConstructingMST(graph.components, pairs);
            //List<Component> components = test.GetComponents();

            foreach (var i in components) //O(sum C (E logE))
            {
                i.SortEdgesByLineMatches();
            }

            MSTExcelWriter excelWriter = new MSTExcelWriter();
            string outFilePath = $@"F:\FCIS\Level 3\Second term\Algorithms\Project\[3] Plagiarism Validation\Test Cases\Sample\{idx} -MST.xlsx";
            excelWriter.WriteToExcel(outFilePath, components);

            // Stop the stopwatch for MST time
            testFile.MSTTimeStopwatch.Stop();
            //restart group stat stopwatch
            testFile.GroupingTimeStopwatch.Restart();
            StatExcelWriter statexcelWriter = new StatExcelWriter();
            string statOutFilePath = $@"F:\FCIS\Level 3\Second term\Algorithms\Project\[3] Plagiarism Validation\Test Cases\Sample\{idx} -stat.xlsx";
            statexcelWriter.WriteToExcel(statOutFilePath, components);
            testFile.GroupingTimeStopwatch.Stop();

            // Stop the stopwatch for total time
            testFile.totalTimeStopwatch.Stop();

            // Get the elapsed time for each component
            long groupAnalyzerElapsedTime = testFile.GroupingTimeStopwatch.ElapsedMilliseconds;
            long mstElapsedTime = testFile.MSTTimeStopwatch.ElapsedMilliseconds;
            long totalElapsedTime = testFile.totalTimeStopwatch.ElapsedMilliseconds;

            // Print the elapsed time for each component
            Console.WriteLine($"Statistics for input number {idx}:");
            Console.WriteLine($"GroupAnalyzer Time: {groupAnalyzerElapsedTime} milliseconds");
            Console.WriteLine($"MST Time: {mstElapsedTime} milliseconds");
            Console.WriteLine($"Total Time: {totalElapsedTime} milliseconds");
            Console.WriteLine();
            StringIdAssigner.Reset();

        }
    }


}
