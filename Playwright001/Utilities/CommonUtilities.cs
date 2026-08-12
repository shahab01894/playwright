using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Playwright001.Utilities
{
    internal class CommonUtilities
    {
        public Dictionary<string, string> GetExcelData()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDir, "TestData", "Book1.xlsx");

            Dictionary<string,string> dictdata= new Dictionary<string,string>();

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook wb = new XSSFWorkbook(fs);
                var sheet = wb.GetSheetAt(0);

                var headerRow = sheet.GetRow(0);
                var valueRow= sheet.GetRow(1);

                for (int i = 0; i < headerRow.Cells.Count; i++)
                {
                    var headerValue = headerRow.GetCell(i)?.ToString() ?? $"column{i}";
                    var cellValue = valueRow.GetCell(i)?.ToString() ?? string.Empty;

                    dictdata[headerValue] = cellValue;

                }          
            }
            return dictdata;
        }
    }
}
