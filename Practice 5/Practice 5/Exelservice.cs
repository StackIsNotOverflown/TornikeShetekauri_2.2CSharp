using OfficeOpenXml;
using System.Collections.Generic;

namespace Practice_5
{
    public class ExcelService
    {
        public byte[] GenerateExcel(List<Employee> employees)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");

                worksheet.Cells[1, 1].Value = "personalID";
                worksheet.Cells[1, 2].Value = "gvari";
                worksheet.Cells[1, 3].Value = "saxeli";
                worksheet.Cells[1, 4].Value = "ganyofileba";
                worksheet.Cells[1, 5].Value = "qalaqi";
                worksheet.Cells[1, 6].Value = "regioni";
                worksheet.Cells[1, 7].Value = "raioni";
                worksheet.Cells[1, 8].Value = "xelfasi";
                worksheet.Cells[1, 9].Value = "asaki";
                worksheet.Cells[1, 10].Value = "staji";
                worksheet.Cells[1, 11].Value = "tarigi_dabadebis";
                worksheet.Cells[1, 12].Value = "sqesi";
                worksheet.Cells[1, 13].Value = "misamarti_saxlis";
                worksheet.Cells[1, 14].Value = "teleponi_saxlis";
                worksheet.Cells[1, 15].Value = "mobiluri";
                worksheet.Cells[1, 16].Value = "email";

                for (int i = 0; i < employees.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = employees[i].personalID;
                    worksheet.Cells[i + 2, 2].Value = employees[i].gvari;
                    worksheet.Cells[i + 2, 3].Value = employees[i].saxeli;
                    worksheet.Cells[i + 2, 4].Value = employees[i].ganyofileba;
                    worksheet.Cells[i + 2, 5].Value = employees[i].qalaqi;
                    worksheet.Cells[i + 2, 6].Value = employees[i].regioni;
                    worksheet.Cells[i + 2, 7].Value = employees[i].raioni;
                    worksheet.Cells[i + 2, 8].Value = employees[i].xelfasi;
                    worksheet.Cells[i + 2, 9].Value = employees[i].asaki;
                    worksheet.Cells[i + 2, 10].Value = employees[i].staji;
                    worksheet.Cells[i + 2, 11].Value = employees[i].tarigi_dabadebis;
                    worksheet.Cells[i + 2, 12].Value = employees[i].sqesi;
                    worksheet.Cells[i + 2, 13].Value = employees[i].misamarti_saxlis;
                    worksheet.Cells[i + 2, 14].Value = employees[i].teleponi_saxlis;
                    worksheet.Cells[i + 2, 15].Value = employees[i].mobiluri;
                    worksheet.Cells[i + 2, 16].Value = employees[i].email;
                }

                return package.GetAsByteArray();
            }
        }
    }
}