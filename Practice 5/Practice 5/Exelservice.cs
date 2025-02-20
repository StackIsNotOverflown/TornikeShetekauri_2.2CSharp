using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace Practice_5
{
    public class ExcelService
    {
        public byte[] GenerateExcel(List<Employee> employees)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");

                for (int i = 0; i < employees.Count; i++)
                {
                    worksheet.Cells[i + 1, 1].Value = employees[i].Gvari;
                    worksheet.Cells[i + 1, 2].Value = employees[i].Saxeli;
                    worksheet.Cells[i + 1, 3].Value = employees[i].Ganyofileba;
                    worksheet.Cells[i + 1, 4].Value = employees[i].Qalaqi;
                }

                return package.GetAsByteArray();
            }
        }
    }
}
