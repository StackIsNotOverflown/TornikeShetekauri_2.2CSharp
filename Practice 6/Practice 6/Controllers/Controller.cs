using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.IO;
using static Practice_6.Model_events;

namespace Practice_6.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private static List<Event> _events = new List<Event>();

        [HttpPost("upload")]
        public async Task<IActionResult> UploadJsonFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                var json = await stream.ReadToEndAsync();
                var jsonData = JsonConvert.DeserializeObject<dynamic>(json);
                _events = JsonConvert.DeserializeObject<List<Event>>(jsonData["events"].ToString());
            }

            return Ok("File processed successfully.");
        }

        [HttpGet("customers")]
        public IActionResult GetCustomers()
        {
            var customers = _events
                .SelectMany(e => new[] { e.SrcNumber, e.DstNumber })
                .Distinct()
                .Select(num => new Customer { PhoneNumber = num })
                .ToList();

            return Ok(customers);
        }

        [HttpGet("all")]
        public IActionResult GetEvents()
        {
            return Ok(_events);
        }

        [HttpGet("download")]
        public IActionResult DownloadEventsAsExcel()
        {
            // Create a new Excel package
            using (var package = new ExcelPackage())
            {
                // Create a worksheet
                var worksheet = package.Workbook.Worksheets.Add("Events");

                // Add headers to the worksheet
                worksheet.Cells[1, 1].Value = "Type";
                worksheet.Cells[1, 2].Value = "SrcNumber";
                worksheet.Cells[1, 3].Value = "DstNumber";
                worksheet.Cells[1, 4].Value = "Time";
                worksheet.Cells[1, 5].Value = "Duration";

                // Populate the worksheet with event data
                int row = 2;
                foreach (var eventItem in _events)
                {
                    worksheet.Cells[row, 1].Value = eventItem.Type;
                    worksheet.Cells[row, 2].Value = eventItem.SrcNumber;
                    worksheet.Cells[row, 3].Value = eventItem.DstNumber;
                    worksheet.Cells[row, 4].Value = eventItem.Time.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Cells[row, 5].Value = eventItem.Duration ?? (object)"N/A";  // Handle null Duration
                    row++;
                }

                // Set the content type and file name
                var excelFile = package.GetAsByteArray();
                return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "events.xlsx");
            }
        }
    }
}
