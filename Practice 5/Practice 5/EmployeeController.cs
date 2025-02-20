using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Practice_5.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _repository;
        private readonly ExcelService _excelService;

        public EmployeeController()
        {
            _repository = new EmployeeRepository();
            _excelService = new ExcelService();
        }

        [HttpGet]
        public ActionResult<List<Employee>> GetEmployees()
        {
            return _repository.GetAllEmployees();
        }

        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            _repository.AddEmployee(employee);
            return Ok("Employee Added");
        }

        [HttpGet("export")]
        public IActionResult ExportToExcel()
        {
            var employees = _repository.GetAllEmployees();
            var excelBytes = _excelService.GenerateExcel(employees);
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employees.xlsx");
        }
    }
}