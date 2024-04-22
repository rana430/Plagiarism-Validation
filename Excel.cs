using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

public class Excel
{
    public static List<(string file1, int percentage1, string file2, int percentage2)> ReadFilePairs(string filePath)
    {
        List<(string file1, int percentage1, string file2, int percentage2)> pairs = new List<(string file1, int percentage1, string file2, int percentage2)>();

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

            // Get the number of rows in the worksheet
            int rowCount = worksheet.Dimension.Rows;

            // Loop through each row to read the data
            for (int row = 1; row <= rowCount; row++)
            {
                //Console.WriteLine("hello");
                string cellValue1 = worksheet.Cells[row, 1].Value?.ToString(); // Assuming file1 is in column 1
                string cellValue2 = worksheet.Cells[row, 2].Value?.ToString(); // Assuming file2 is in column 2


                // Remove the '%' symbol from the percentage strings
                string percentageString1 = cellValue1.Replace("%", "");
                string percentageString2 = cellValue2.Replace("%", "");

                // Split cell values to extract file path and percentage
                string[] parts1 = percentageString1.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                string[] parts2 = percentageString2.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                //Console.WriteLine("hello");

                Console.WriteLine(parts1[1] + " " + parts2[1]);
                int.TryParse(parts1[1], out int percentage1);
                int.TryParse(parts2[1], out int percentage2);
                Console.WriteLine(percentage1 + " " + percentage2);
                Console.WriteLine(parts2.Length);

                //if (parts1.Length == 2 && parts2.Length == 2 && int.TryParse(parts1[1], out int percentage1) && int.TryParse(parts2[1], out int percentage2))
                //{
                   pairs.Add((parts1[0].Trim(), percentage1, parts2[0].Trim(), percentage2));
                // }
            }
        }

        return pairs;
    }
}