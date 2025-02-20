using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace Practice_5
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly ExcelService _excelService;

        public EmployeesController(EmployeeRepository employeeRepository, ExcelService excelService)
        {
            _employeeRepository = employeeRepository;
            _excelService = excelService;
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportEmployeesToExcel()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();
            var excelData = _excelService.GenerateExcel(employees);
            var fileName = "Employees.xlsx";
            return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }

}