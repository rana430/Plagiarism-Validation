using OfficeOpenXml;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiarism_Validation
{
    public class StatExcelWriter
    {
        public void WriteToExcel(string filePath, List<Component> components)
        {

            using (var package = new ExcelPackage())
            {
                // Add a new worksheet to the Excel package
                var worksheet = package.Workbook.Worksheets.Add("Component");

                // Set column names
                worksheet.Cells[1, 1].Value = "Component Index";
                worksheet.Cells[1, 2].Value = "Vertices";
                worksheet.Cells[1, 3].Value = "Average Similarity";
                worksheet.Cells[1, 4].Value = "Component Count";

                // Set column widths
                worksheet.Column(1).Width = 20;
                worksheet.Column(2).Width = 40; // Adjust width to fit vertices
                worksheet.Column(3).Width = 20;
                worksheet.Column(4).Width = 20;

                // Write data to Excel
                int row = 2;
                foreach (var comp in components)
                {

                    /*// Convert the list of integers to a comma-separated string
                    string verticesString = string.Join(", ", comp.edges);

                    // Write data to cells
                    worksheet.Cells[row, 1].Value = row - 1; // Component Index starts from 1
                    worksheet.Cells[row, 2].Value = verticesString;
                    worksheet.Cells[row, 3].Value = Math.Round((float)(comp.avgSim / comp.edgeCount), 1);
                    worksheet.Cells[row, 4].Value = comp.edges.Count;
                    row++;*/
                    // Convert the list of integers to a comma-separated string
                    comp.SortIds();
                    string verticesString = string.Join(", ", comp.ids);

                    // Write data to cells
                    worksheet.Cells[row, 1].Value = row - 1; // Component Index starts from 1
                    worksheet.Cells[row, 2].Value = verticesString;
                    worksheet.Cells[row, 3].Value = Math.Round((float)(comp.avgSim / comp.edgeCount), 1);
                    worksheet.Cells[row, 4].Value = comp.stringIds.Count;
                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                // Save Excel file
                FileInfo excelFile = new FileInfo(filePath);
                package.SaveAs(excelFile);
            }
        }

    }
}