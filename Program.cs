using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiarism_Validation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Path to your Excel file
            string filePath = @"D:\uni\Algo\file.xlsx";

            // Read the Excel file using the Excel class
            Excel.Read(filePath);
        }
    }
}
