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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [Orders].[dbo].[Xelshekruleba] WHERE tarigi_shesrulebis IS NULL OR vali_l > 0", conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime? tarigiShesrulebis = reader.IsDBNull(11) ? (DateTime?)null : reader.GetDateTime(11);
                        if (!tarigiShesrulebis.HasValue)
                        {
                            tarigiShesrulebis = DateTime.Now;
                        }

                        int daysRemaining = 0;
                        if (tarigiShesrulebis.HasValue)
                        {
                            DateTime today = DateTime.Now;
                            DateTime modifiedCompletionDate = new DateTime(today.Year, tarigiShesrulebis.Value.Month, tarigiShesrulebis.Value.Day);
                            daysRemaining = (modifiedCompletionDate - today).Days;
                        }

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
                            TarigiDawyebis = reader.GetDateTime(10),
                            TarigiShesrulebis = reader.IsDBNull(11) ? (DateTime?)null : reader.GetDateTime(11),
                            Shesruleba = !reader.IsDBNull(12), 
                            VisiMizezit = reader.IsDBNull(13) ? null : reader.GetString(13),
                            DaysRemaining = daysRemaining
                        });
                    }
                }
            }
            return orders.Count > 0 ? orders : new List<Order>();
        }

        public IActionResult ExportToExcel()
        {
            var orders = GetOrders();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Orders");
                worksheet.Cells[1, 1].Value = "შეკვეთის ID";
                worksheet.Cells[1, 2].Value = "დავალიანება (GEL)";
                worksheet.Cells[1, 3].Value = "დავალიანება (USD)";
                worksheet.Cells[1, 4].Value = "გადახდილი რაოდენობა";
                worksheet.Cells[1, 5].Value = "დარჩენილი გადასახდელი (GEL)";
                worksheet.Cells[1, 6].Value = "დარჩენილი გადასაგდები (USD)";
                worksheet.Cells[1, 7].Value = "მიზეზი";
                worksheet.Cells[1, 8].Value = "დაწყების დრო";
                worksheet.Cells[1, 9].Value = "დამთავრების დრო";
                worksheet.Cells[1, 10].Value = "დარჩენილი დრო";

                int row = 2;
                foreach (var order in orders)
                {
                    worksheet.Cells[row, 1].Value = order.XelshekrulebaID;
                    worksheet.Cells[row, 2].Value = order.GadasaxdeliL;
                    worksheet.Cells[row, 3].Value = order.GadasaxdeliD;
                    worksheet.Cells[row, 4].Value = order.ValiL;
                    worksheet.Cells[row, 5].Value = order.GadaxdiliL;
                    worksheet.Cells[row, 6].Value = order.GadaxdiliD;
                    worksheet.Cells[row, 7].Value = order.VisiMizezit ?? "NULL";
                    worksheet.Cells[row, 8].Value = order.TarigiDawyebis.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 9].Value = order.TarigiShesrulebis?.ToString("yyyy-MM-dd");
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