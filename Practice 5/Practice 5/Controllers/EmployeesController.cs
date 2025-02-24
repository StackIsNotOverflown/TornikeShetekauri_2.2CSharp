using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using OfficeOpenXml;
using System.IO;

[Route("api/employees")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly string connectionString = "your_connection_string_here"; // Update this

    [HttpGet]
    public IActionResult GetEmployees()
    {
        using (var conn = new SqlConnection(connectionString))
        using (var cmd = new SqlCommand("SELECT TOP 1000 * FROM Orders.dbo.Personali", conn))
        {
            conn.Open();
            var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return Ok(dt);
        }
    }

    [HttpGet("export")]
    public IActionResult ExportEmployeesToExcel()
    {
        using (var conn = new SqlConnection(connectionString))
        using (var cmd = new SqlCommand("SELECT TOP 1000 * FROM Orders.dbo.Personali", conn))
        {
            conn.Open();
            var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add("Employees");
                sheet.Cells.LoadFromDataTable(dt, true);
                var stream = new MemoryStream(package.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employees.xlsx");
            }
        }
    }

    [HttpPost]
    public IActionResult AddEmployee([FromBody] Employee emp)
    {
        using (var conn = new SqlConnection(connectionString))
        using (var cmd = new SqlCommand("INSERT INTO Orders.dbo.Personali (gvari, saxeli, qalaqi, xelfasi) VALUES (@gvari, @saxeli, @qalaqi, @xelfasi)", conn))
        {
            cmd.Parameters.AddWithValue("@gvari", emp.gvari);
            cmd.Parameters.AddWithValue("@saxeli", emp.saxeli);
            cmd.Parameters.AddWithValue("@qalaqi", emp.qalaqi);
            cmd.Parameters.AddWithValue("@xelfasi", emp.xelfasi);
            conn.Open();
            cmd.ExecuteNonQuery();
            return Ok("Employee added successfully");
        }
    }
}

public class Employee
{
    public string gvari { get; set; }
    public string saxeli { get; set; }
    public string qalaqi { get; set; }
    public decimal xelfasi { get; set; }
}
