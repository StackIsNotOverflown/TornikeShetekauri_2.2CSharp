using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.ComponentModel;

namespace Practice_5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private string _connectionString = "Data Source=ERROR\\SQLEXPRESS;Initial Catalog=Orders;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"; 

        
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = new List<Employee>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                        SELECT TOP (1000) 
                            [personaliID],
                            [gvari],
                            [saxeli],
                            [ganyofileba],
                            [qalaqi],
                            [regioni],
                            [raioni],
                            [xelfasi],
                            [asaki],
                            [staji],
                            [tarigi_dabadebis],
                            [sqesi],
                            [misamarti_saxlis],
                            [teleponi_saxlis],
                            [mobiluri],
                            [email],
                            [ierarqia]
                        FROM [Orders].[dbo].[Personali]";
                var command = new SqlCommand(query, connection);

                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    employees.Add(new Employee
                    {
                        PersonaliID = (int)reader["personaliID"],
                        Gvari = reader["gvari"].ToString(),
                        Saxeli = reader["saxeli"].ToString(),
                        Ganyofileba = (DateTime)reader["ganyofileba"],
                        Qalaqi = reader["qalaqi"].ToString(),
                        Regioni = reader["regioni"].ToString(),
                        Raioni = reader["raioni"].ToString(),
                        Xelfasi = reader["xelfasi"].ToString(),
                        Asaki = reader["asaki"].ToString(),
                        Staji = (int)reader["staji"],
                        TarigiDabadebis = (DateTime)reader["tarigi_dabadebis"],
                        Sqesi = reader["sqesi"].ToString(),
                        MisamartiSaxlis = reader["misamarti_saxlis"].ToString(),
                        TeleponiSaxlis = reader["teleponi_saxlis"].ToString(),
                        Mobiluri = reader["mobiluri"].ToString(),
                        Email = reader["email"].ToString(),
                        Ierarqia = reader["ierarqia"].ToString()
                    });
                }
            }

            return Ok(employees);
        }

        
        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                        INSERT INTO [Orders].[dbo].[Personali] (
                            [gvari], [saxeli], [ganyofileba], [qalaqi], [regioni], [raioni],
                            [xelfasi], [asaki], [staji], [tarigi_dabadebis], [sqesi],
                            [misamarti_saxlis], [teleponi_saxlis], [mobiluri], [email], [ierarqia]
                        ) VALUES (
                            @Gvari, @Saxeli, @Ganyofileba, @Qalaqi, @Regioni, @Raioni,
                            @Xelfasi, @Asaki, @Staji, @TarigiDabadebis, @Sqesi,
                            @MisamartiSaxlis, @TeleponiSaxlis, @Mobiluri, @Email, @Ierarqia
                        )";
                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Gvari", employee.Gvari);
                command.Parameters.AddWithValue("@Saxeli", employee.Saxeli);
                command.Parameters.AddWithValue("@Ganyofileba", employee.Ganyofileba);
                command.Parameters.AddWithValue("@Qalaqi", employee.Qalaqi);
                command.Parameters.AddWithValue("@Regioni", employee.Regioni);
                command.Parameters.AddWithValue("@Raioni", employee.Raioni);
                command.Parameters.AddWithValue("@Xelfasi", employee.Xelfasi);
                command.Parameters.AddWithValue("@Asaki", employee.Asaki);
                command.Parameters.AddWithValue("@Staji", employee.Staji);
                command.Parameters.AddWithValue("@TarigiDabadebis", employee.TarigiDabadebis);
                command.Parameters.AddWithValue("@Sqesi", employee.Sqesi);
                command.Parameters.AddWithValue("@MisamartiSaxlis", employee.MisamartiSaxlis);
                command.Parameters.AddWithValue("@TeleponiSaxlis", employee.TeleponiSaxlis);
                command.Parameters.AddWithValue("@Mobiluri", employee.Mobiluri);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@Ierarqia", employee.Ierarqia);

                connection.Open();
                command.ExecuteNonQuery();
            }

            return Ok();
        }

        
        [HttpGet]
        public IActionResult ExportEmployeesToExcel()
        {
            var employees = new List<Employee>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                        SELECT TOP (1000) 
                            [personaliID],
                            [gvari],
                            [saxeli],
                            [ganyofileba],
                            [qalaqi],
                            [regioni],
                            [raioni],
                            [xelfasi],
                            [asaki],
                            [staji],
                            [tarigi_dabadebis],
                            [sqesi],
                            [misamarti_saxlis],
                            [teleponi_saxlis],
                            [mobiluri],
                            [email],
                            [ierarqia]
                        FROM [Orders].[dbo].[Personali]";
                var command = new SqlCommand(query, connection);

                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    employees.Add(new Employee
                    {
                        PersonaliID = (int)reader["personaliID"],
                        Gvari = reader["gvari"].ToString(),
                        Saxeli = reader["saxeli"].ToString(),
                        Ganyofileba = (DateTime)reader["ganyofileba"],
                        Qalaqi = reader["qalaqi"].ToString(),
                        Regioni = reader["regioni"].ToString(),
                        Raioni = reader["raioni"].ToString(),
                        Xelfasi = reader["xelfasi"].ToString(),
                        Asaki = reader["asaki"].ToString(),
                        Staji = (int)reader["staji"],
                        TarigiDabadebis = (DateTime)reader["tarigi_dabadebis"],
                        Sqesi = reader["sqesi"].ToString(),
                        MisamartiSaxlis = reader["misamarti_saxlis"].ToString(),
                        TeleponiSaxlis = reader["teleponi_saxlis"].ToString(),
                        Mobiluri = reader["mobiluri"].ToString(),
                        Email = reader["email"].ToString(),
                        Ierarqia = reader["ierarqia"].ToString()
                    });
                }
            }

            var fileContents = GenerateExcelFile(employees);
            return new FileContentResult(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = "Employees.xlsx"
            };
        }

        private byte[] GenerateExcelFile(List<Employee> employees, object excelPackage)
        {
            ExcelPackage.LicenseContext = LicenseContext.Noncommercial;
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Employees");

            worksheet.Cells[1, 1].Value = "Personali ID";
            worksheet.Cells[1, 2].Value = "Gvari";
            worksheet.Cells[1, 3].Value = "Saxeli";
            worksheet.Cells[1, 4].Value = "Ganyofileba";
            worksheet.Cells[1, 5].Value = "Qalaqi";
            worksheet.Cells[1, 6].Value = "Regioni";
            worksheet.Cells[1, 7].Value = "Raioni";
            worksheet.Cells[1, 8].Value = "Xelfasi";
            worksheet.Cells[1, 9].Value = "Asaki";
            worksheet.Cells[1, 10].Value = "Staji";
            worksheet.Cells[1, 11].Value = "Tarigi Dabadebis";
            worksheet.Cells[1, 12].Value = "Sqesi";
            worksheet.Cells[1, 13].Value = "Misamarti Saxlis";
            worksheet.Cells[1, 14].Value = "Teleponi Saxlis";
            worksheet.Cells[1, 15].Value = "Mobiluri";
            worksheet.Cells[1, 16].Value = "Email";
            worksheet.Cells[1, 17].Value = "Ierarqia";

            for (int i = 0; i < employees.Count; i++)
            {
                var employee = employees[i];
                worksheet.Cells[i + 2, 1].Value = employee.PersonaliID;
                worksheet.Cells[i + 2, 2].Value = employee.Gvari;
                worksheet.Cells[i + 2, 3].Value = employee.Saxeli;
                worksheet.Cells[i + 2, 4].Value = employee.Ganyofileba.ToString("yyyy-MM-dd");
                worksheet.Cells[i + 2, 5].Value = employee.Qalaqi;
                worksheet.Cells[i + 2, 6].Value = employee.Regioni;
                worksheet.Cells[i + 2, 7].Value = employee.Raioni;
                worksheet.Cells[i + 2, 8].Value = employee.Xelfasi;
                worksheet.Cells[i + 2, 9].Value = employee.Asaki;
                worksheet.Cells[i + 2, 10].Value = employee.Staji;
                worksheet.Cells[i + 2, 11].Value = employee.TarigiDabadebis.ToString("yyyy-MM-dd");
                worksheet.Cells[i + 2, 12].Value = employee.Sqesi;
                worksheet.Cells[i + 2, 13].Value = employee.MisamartiSaxlis;
                worksheet.Cells[i + 2, 14].Value = employee.TeleponiSaxlis;
                worksheet.Cells[i + 2, 15].Value = employee.Mobiluri;
                worksheet.Cells[i + 2, 16].Value = employee.Email;
                worksheet.Cells[i + 2, 17].Value = employee.Ierarqia;
            }

            return package.GetAsByteArray();
        }
    }
}
