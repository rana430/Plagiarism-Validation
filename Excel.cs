using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml; // Include EPPlus namespace
using System.Text.RegularExpressions;

namespace Plagiarism_Validation
{
    public class Excel
    {
        public static List<Edge> ReadFilePairs(string filePath)
        {
            List<Edge> pairs = new List<Edge>(); // Initialize the list here

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                return pairs;
            }

            // Read the Excel file
            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Get the first worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                int rowCount = worksheet.Dimension.Rows;

                // Loop to read the data
                for (int row = 1; row <= rowCount; row++)
                {
                    string cellValue1 = worksheet.Cells[row, 1].Value?.ToString();
                    string cellValue2 = worksheet.Cells[row, 2].Value?.ToString();
                    string cellValue3 = worksheet.Cells[row, 3].Value?.ToString(); // Ensure to handle null value
                    // Remove % from the percentage strings to convert them to int
                    string percentageString1 = cellValue1?.Replace("%", ""); // Ensure to handle null value
                    string percentageString2 = cellValue2?.Replace("%", ""); // Ensure to handle null value


                    //save the hyperLink for each file and if its valid or not
                    Uri cellLink1 = worksheet.Cells[row, 1].Hyperlink;
                    Uri cellLink2 = worksheet.Cells[row, 2].Hyperlink;

                    // split cell values into file path and percentage
                    string[] parts1 = percentageString1?.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] parts2 = percentageString2?.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] parts3 = cellValue3?.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

                    // Check if all required parts are available
                    if (parts1?.Length >= 2 && parts2?.Length >= 2 && parts3?.Length >= 1)
                    {
                        int percentage1, percentage2, lineMatches;

                        // Parse the percentages and line matches
                        if (int.TryParse(parts1[1], out percentage1) &&
                            int.TryParse(parts2[1], out percentage2) &&
                            int.TryParse(parts3[0], out lineMatches))
                        {
                            Node source = new Node(cellValue1, ExtractId(parts1[0].Trim()),cellLink1);
                            Node destination = new Node(cellValue2, ExtractId(parts2[0].Trim()),cellLink2);
                            /*Console.Write(source.id);
                            Console.Write("  ");
                            Console.WriteLine(source.idString);
                            Console.Write(destination.id);Console.Write("  "); Console.WriteLine(destination.idString);*/
                            // Create an Edge object and add it to the list
                            Edge edge = new Edge(lineMatches, percentage1, percentage2, source, destination,row);
                            pairs.Add(edge);
                        }
                    }
                }
            }

            return pairs;
        }

        public static string ExtractId(string input)
        {
            string id = "";
            foreach( var c in input)
            {
                if (c >= '0' && c <= '9')
                {
                    // Append the digit to the ID string
                    id += c;
                }
                else
                {
                    if (id.Length > 0) break;
                }


            }
            return id;

        }

        public static int ParseId(string input)
        {
            // Regular expression to extract the ID
            string pattern = @"(\d+)/";

            // Match the pattern in the input string
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                // Extract the matched ID and convert it to an integer
                if (int.TryParse(match.Groups[1].Value, out int id))
                {
                    return id;
                }
            }

            // Return -1 if parsing fails or no match is found
            return -1;
        }


    }
}
