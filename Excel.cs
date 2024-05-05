using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml; // Include EPPlus namespace

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
                            Node source = new Node(parts1[0].Trim(), ExtractId(parts1[0].Trim()));
                            Node destination = new Node(parts2[0].Trim(), ExtractId(parts2[0].Trim()));

                            // Create an Edge object and add it to the list
                            Edge edge = new Edge(lineMatches, percentage1, percentage2, source, destination,row);
                            pairs.Add(edge);
                        }
                    }
                }
            }

            return pairs;
        }

        public static int ExtractId(string input)
        {
            // Find the index of "D:/Source/"
            int startIndex = input.IndexOf("D:/SOURCE/");

            // Ensure that "D:/Source/" is found
            //if (startIndex == -1)
            //{
            //    throw new ArgumentException("Invalid input format. 'D:/Source/' not found.");
            //}

            // Start extraction after "D:/Source/"
            startIndex += "D:/Source/".Length;

            // Find the index of the next '/'
            int nextSlashIndex = input.IndexOf('/', startIndex);

            // Ensure that next '/' is found
            if (nextSlashIndex == -1)
            {
                throw new ArgumentException("Invalid input format. '/' not found after 'D:/Source/'.");
            }

            // Extract the substring between "D:/Source/" and the next '/'
            string idString = input.Substring(startIndex, nextSlashIndex - startIndex);

            // Parse the ID string to integer
            int id = int.Parse(idString);

            return id;
        }


    }
}
