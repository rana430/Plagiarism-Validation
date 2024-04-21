using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OfficeOpenXml;


namespace Plagiarism_Validation
{
    public class Excel
    {
        public static void Read(string filePath)
        {

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            // Read the Excel file
            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {

                // Get the first worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                // Get the number of rows and columns in the worksheet
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                // Loop through each row and column to read the data
                for (int row = 1; row <= rowCount; row++)
                {
                    for (int col = 1; col <= colCount; col++)
                    {
                        // Read the cell value
                        object cellValue = worksheet.Cells[row, col].Value;

                        // Do something with the cell value (e.g., print it)
                        Console.Write(cellValue + "\t");
                    }
                    Console.WriteLine(); // Move to the next row
                }
            }
        }
    }
}
