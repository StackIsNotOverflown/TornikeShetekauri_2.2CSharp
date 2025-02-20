using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

public class Exelservice
{ 
    public byte[] GenerateExel(List<Employee> employees)
    {


        using (var package = new ExelPackage())
        {
            Worksheet.Cells[1,1].Value = "personalID";
            Worksheet.Cells[1, 2].Value = "gvari";
            Worksheet.Cells[1, 3].Value = "saxeli";
            Worksheet.Cells[1, 4].Value = "ganyofileba";
            Worksheet.Cells[1, 5].Value = "qalaqi";
            Worksheet.Cells[1, 6].Value = "regioni";
            Worksheet.Cells[1, 7].Value = "raioni";
            Worksheet.Cells[1, 8].Value = "xelfasi";
            Worksheet.Cells[1, 9].Value = "asaki";
            Worksheet.Cells[1, 10].Value = "staji";
            Worksheet.Cells[1, 11].Value = "tarigi_dabadebis";
            Worksheet.Cells[1, 12].Value = "sqesi";
            Worksheet.Cells[1, 13].Value = "misamarti_saxlis";
            Worksheet.Cells[1, 14].Value = "teleponi_saxlis";
            Worksheet.Cells[1, 15].Value = "mobiluri";
            Worksheet.Cells[1, 16].Value = "email";
            for (int i = 0; i < employees.Count; i++)
            {
                Worksheet.Cells[i + 2, 1].Value = employees[i].personaliID;
                Worksheet.Cells[i + 2, 2].Value = employees[i].gvari;
                Worksheet.Cells[i + 2, 3].Value = employees[i].saxeli;
                Worksheet.Cells[i + 2, 4].Value = employees[i].ganyofileba;
                Worksheet.Cells[i + 2, 5].Value = employees[i].qalaqi;
                Worksheet.Cells[i + 2, 6].Value = employees[i].regioni;
                Worksheet.Cells[i + 2, 7].Value = employees[i].raioni;
                Worksheet.Cells[i + 2, 8].Value = employees[i].xelfasi;
                Worksheet.Cells[i + 2, 9].Value = employees[i].asaki;
                Worksheet.Cells[i + 2, 10].Value = employees[i].staji;
                Worksheet.Cells[i + 2, 11].Value = employees[i].tarigi_dabadebis;
                Worksheet.Cells[i + 2, 12].Value = employees[i].sqesi;
                Worksheet.Cells[i + 2, 13].Value = employees[i].misamarti_saxlis;
                Worksheet.Cells[i + 2, 14].Value = employees[i].teleponi_saxlis;
                Worksheet.Cells[i + 2, 15].Value = employees[i].mobiluri;
                Worksheet.Cells[i + 2, 16].Value = employees[i].email;

            }
            return package.GetAsByteArray();
        }
    }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        }