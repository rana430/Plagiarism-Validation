using OfficeOpenXml;
using System;
using System.IO;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiarism_Validation
{
    public class MSTExcelWriter
    {
        public void WriteToExcel(string filePath, List<Component> components)
        {
            using (var package = new ExcelPackage())
            {
                // Add a new worksheet to the Excel package
                var worksheet = package.Workbook.Worksheets.Add("Component");

                // Set column names
                worksheet.Cells[1, 1].Value = "File 1";
                worksheet.Cells[1, 2].Value = "File 2";
                worksheet.Cells[1, 3].Value = "Line Matches";

                // Set column widths
                worksheet.Column(1).Width = 20;
                worksheet.Column(2).Width = 20;
                worksheet.Column(3).Width = 20;

                // Write data to Excel
                int row = 2;
                foreach (var comp in components)
                {
                    foreach (var node in comp.nodes)
                    {
                        worksheet.Cells[row, 1].Value = node.Item2.Source.path;
                        worksheet.Cells[row, 2].Value = node.Item2.Destination.path;
                        worksheet.Cells[row, 3].Value = node.Item2.lineMatches;
                        row++;
                    }
                }
                worksheet.Cells.AutoFitColumns();
                // Save Excel file
                FileInfo excelFile = new FileInfo(filePath);
                package.SaveAs(excelFile);
            }
        }
    }
}