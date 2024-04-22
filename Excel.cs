using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

public class Excel
{
    public static List<(string file1, int percentage1, string file2, int percentage2)> ReadFilePairs(string filePath)
    {
        List<(string file1, int percentage1, string file2, int percentage2)> pairs = new List<(string file1, int percentage1, string file2, int percentage2)>();

        //check if the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File does not exist.");
            return pairs;
        }

        //read the Excel file
        using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
        {
            //get the first worksheet
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            int rowCount = worksheet.Dimension.Rows;

            //loop to read the data
            for (int row = 1; row <= rowCount; row++)
            {
                //Console.WriteLine("hello");
                string cellValue1 = worksheet.Cells[row, 1].Value?.ToString();
                string cellValue2 = worksheet.Cells[row, 2].Value?.ToString(); 


                // Remove % from the percentage strings 3shan a7awelo le int
                string percentageString1 = cellValue1.Replace("%", "");
                string percentageString2 = cellValue2.Replace("%", "");

                // split cell values into file path and percentage
                string[] parts1 = percentageString1.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                string[] parts2 = percentageString2.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                //Console.WriteLine("hello");

                int.TryParse(parts1[1], out int percentage1);
                int.TryParse(parts2[1], out int percentage2);


                //if (parts1.Length == 2 && parts2.Length == 2 && int.TryParse(parts1[1], out int percentage1) && int.TryParse(parts2[1], out int percentage2))
                //{
                   pairs.Add((parts1[0].Trim(), percentage1, parts2[0].Trim(), percentage2));
                // }
            }
        }

        return pairs;
    }
}