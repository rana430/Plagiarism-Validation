using OfficeOpenXml;
using System;
using System.IO;
using System.Collections.Generic;

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
                        // Add hyperlinks to the cells
                        var file1Cell = worksheet.Cells[row, 1];
                        var file2Cell = worksheet.Cells[row, 2];

                        file1Cell.Hyperlink = node.Item2.Source.hyperLink;
                        file1Cell.Value = node.Item2.Source.path;

                        file2Cell.Hyperlink = node.Item2.Destination.hyperLink;
                        file2Cell.Value = (node.Item2.Destination.path);

                        /*// Apply the hyperlink style to make it look like a clickable link
                        file1Cell.Style.Font.UnderLine = true;
                        file1Cell.Style.Font.Color.SetColor(System.Drawing.Color.Blue);

                        file2Cell.Style.Font.UnderLine = true;
                        file2Cell.Style.Font.Color.SetColor(System.Drawing.Color.Blue);*/

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
