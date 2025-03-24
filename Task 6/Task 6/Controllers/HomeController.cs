using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Task_6.Models;

namespace Task_6.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString = "Data Source=ERROR\\SQLEXPRESS;Initial Catalog=Orders;Integrated Security=True;";

        public IActionResult Index()
        {
            var orders = GetOrders();
            return View(orders);
        }

        private List<Order> GetOrders()
        {
            var orders = new List<Order>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT * FROM [Orders].[dbo].[Xelshekruleba] 
                      WHERE tarigi_shesrulebis IS NULL 
                      OR (vali_l > 0 OR vali_d > 0)", conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime tarigiDawyebis = reader.GetDateTime(10);
                        DateTime? tarigiDamtavrebis = reader.IsDBNull(12) ? null : reader.GetDateTime(12);

                        int daysRemaining = tarigiDamtavrebis.HasValue
                            ? (int)(tarigiDamtavrebis.Value - tarigiDawyebis).TotalDays
                            : 0;

                        orders.Add(new Order
                        {
                            XelshekrulebaID = reader.GetInt32(0),
                            PersonaliID = reader.GetInt32(1),
                            ShemkvetiID = reader.GetInt32(2),
                            GadasaxdeliL = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                            GadasaxdeliD = reader.IsDBNull(4) ? 0 : reader.GetDouble(4),
                            GadaxdiliL = reader.IsDBNull(5) ? 0 : reader.GetDouble(5),
                            GadaxdiliD = reader.IsDBNull(6) ? 0 : reader.GetDouble(6),
                            ValiL = reader.IsDBNull(7) ? 0 : reader.GetDouble(7),
                            ValiD = reader.IsDBNull(8) ? 0 : reader.GetDouble(8),
                            Kursi = reader.IsDBNull(9) ? 0 : reader.GetDouble(9),
                            TarigiDawyebis = tarigiDawyebis,
                            TarigiDamtavrebis = tarigiDamtavrebis,
                            Shesruleba = !reader.IsDBNull(13),
                            VisiMizezit = reader.IsDBNull(14) ? null : reader.GetString(14),
                            DaysRemaining = daysRemaining
                        });
                    }
                }
            }
            return orders;
        }

        public IActionResult ExportToExcel()
        {
            var orders = GetOrders();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Orders");
                
                string[] headers = {
                    "შეკვეთის ID",
                    "დავალიანება (GEL)",
                    "დავალიანება (USD)",
                    "გადახდილი რაოდენობა",
                    "დარჩენილი გადასახდელი (GEL)",
                    "დარჩენილი გადასაგდები (USD)",
                    "მიზეზი",
                    "დაწყების დრო",
                    "დედლაინი",
                    "დარჩენილი დრო (დღე)"
                };

                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = headers[i];
                }

                int row = 2;
                foreach (var order in orders)
                {
                    worksheet.Cells[row, 1].Value = order.XelshekrulebaID;
                    worksheet.Cells[row, 2].Value = Math.Round(order.GadasaxdeliL, 2);
                    worksheet.Cells[row, 3].Value = Math.Round(order.GadasaxdeliD, 2);
                    worksheet.Cells[row, 4].Value = Math.Round(order.ValiL, 2);
                    worksheet.Cells[row, 5].Value = Math.Round(order.GadaxdiliL, 2);
                    worksheet.Cells[row, 6].Value = Math.Round(order.GadaxdiliD, 2);
                    worksheet.Cells[row, 7].Value = order.Shesruleba ? "კი" : "არა";
                    worksheet.Cells[row, 8].Value = order.TarigiDawyebis.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 9].Value = order.TarigiDamtavrebis?.ToString("yyyy-MM-dd") ?? "";
                    worksheet.Cells[row, 10].Value = order.DaysRemaining;
                    row++;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Orders.xlsx");
            }
        }
    }
}