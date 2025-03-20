using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

            try
            {
                using (var stream = new StreamReader(file.OpenReadStream()))
                {
                    var json = await stream.ReadToEndAsync();

                  
                    if (json.TrimStart().StartsWith("["))
                    {
                       
                        _events = JsonConvert.DeserializeObject<List<Event>>(json, new JsonSerializerSettings
                        {
                            Error = (sender, args) => args.ErrorContext.Handled = true
                        });
                    }
                    else
                    {
                        
                        var jsonObject = JObject.Parse(json);
                        if (jsonObject["events"] != null)
                        {
                            _events = jsonObject["events"].ToObject<List<Event>>();
                        }
                        else
                        {
                        
                            var singleEvent = JsonConvert.DeserializeObject<Event>(json);
                            if (singleEvent != null)
                            {
                                _events = new List<Event> { singleEvent };
                            }
                        }
                    }

                    if (_events == null)
                    {
                        _events = new List<Event>();
                    }

                    // ნულლებზეეე
                    foreach (var eventItem in _events)
                    {
                        eventItem.SrcNumber = eventItem.SrcNumber ?? "Unknown";
                        eventItem.DstNumber = eventItem.DstNumber ?? "Unknown";
                        eventItem.SrcLoc = eventItem.SrcLoc ?? new List<double?> { null, null };
                        eventItem.DstLoc = eventItem.DstLoc ?? new List<double?> { null, null };
                    }
                }

                return Ok(new { message = "File processed successfully.", events = _events });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error processing file: {ex.Message}");
            }
        }

        [HttpGet("customers")]
        public IActionResult GetCustomers()
        {
            var customers = _events
                .SelectMany(e => new[] { e.SrcNumber, e.DstNumber })
                .Where(num => !string.IsNullOrEmpty(num) && num != "Unknown")
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
            using (var package = new ExcelPackage())
            {
                
                var eventsWorksheet = package.Workbook.Worksheets.Add("Events");

                eventsWorksheet.Cells[1, 1].Value = "Type";
                eventsWorksheet.Cells[1, 2].Value = "Source Number";
                eventsWorksheet.Cells[1, 3].Value = "Destination Number";
                eventsWorksheet.Cells[1, 4].Value = "Time";
                eventsWorksheet.Cells[1, 5].Value = "Duration";
                eventsWorksheet.Cells[1, 6].Value = "Source Location Longitude";
                eventsWorksheet.Cells[1, 7].Value = "Source Location Latitude";
                eventsWorksheet.Cells[1, 8].Value = "Destination Location Longitude";
                eventsWorksheet.Cells[1, 9].Value = "Destination Location Latitude";

                int row = 2;
                foreach (var eventItem in _events)
                {
                    eventsWorksheet.Cells[row, 1].Value = eventItem.Type;
                    eventsWorksheet.Cells[row, 2].Value = eventItem.SrcNumber;
                    eventsWorksheet.Cells[row, 3].Value = eventItem.DstNumber;
                    eventsWorksheet.Cells[row, 4].Value = eventItem.Time.ToString("yyyy-MM-dd HH:mm:ss");
                    eventsWorksheet.Cells[row, 5].Value = eventItem.Duration.HasValue ? eventItem.Duration.Value.ToString() : "N/A";

                    // ლოკაციები ( ეს სკოლის პროექტზეც ძაან დამეხმარა)
                    if (eventItem.SrcLoc != null && eventItem.SrcLoc.Count >= 2)
                    {
                        eventsWorksheet.Cells[row, 6].Value = eventItem.SrcLoc[0].HasValue ? eventItem.SrcLoc[0].Value.ToString() : "N/A";
                        eventsWorksheet.Cells[row, 7].Value = eventItem.SrcLoc[1].HasValue ? eventItem.SrcLoc[1].Value.ToString() : "N/A";
                    }
                    else
                    {
                        eventsWorksheet.Cells[row, 6].Value = "N/A";
                        eventsWorksheet.Cells[row, 7].Value = "N/A";
                    }

                    if (eventItem.DstLoc != null && eventItem.DstLoc.Count >= 2)
                    {
                        eventsWorksheet.Cells[row, 8].Value = eventItem.DstLoc[0].HasValue ? eventItem.DstLoc[0].Value.ToString() : "N/A";
                        eventsWorksheet.Cells[row, 9].Value = eventItem.DstLoc[1].HasValue ? eventItem.DstLoc[1].Value.ToString() : "N/A";
                    }
                    else
                    {
                        eventsWorksheet.Cells[row, 8].Value = "N/A";
                        eventsWorksheet.Cells[row, 9].Value = "N/A";
                    }

                    row++;
                }

                
                var customersWorksheet = package.Workbook.Worksheets.Add("Customers");
                customersWorksheet.Cells[1, 1].Value = "Phone Number";
                customersWorksheet.Cells[1, 2].Value = "Last Seen";

                var customers = _events
                    .SelectMany(e => new[]
                    {
                        new { Number = e.SrcNumber, Time = e.Time },
                        new { Number = e.DstNumber, Time = e.Time }
                    })
                    .Where(c => !string.IsNullOrEmpty(c.Number) && c.Number != "Unknown")
                    .GroupBy(c => c.Number)
                    .Select(g => new
                    {
                        PhoneNumber = g.Key,
                        LastSeen = g.Max(c => c.Time)
                    })
                    .ToList();

                row = 2;
                foreach (var customer in customers)
                {
                    customersWorksheet.Cells[row, 1].Value = customer.PhoneNumber;
                    customersWorksheet.Cells[row, 2].Value = customer.LastSeen.ToString("yyyy-MM-dd HH:mm:ss");
                    row++;
                }

                // Auto-fit columns
                eventsWorksheet.Cells.AutoFitColumns();
                customersWorksheet.Cells.AutoFitColumns();

                var excelFile = package.GetAsByteArray();
                return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "events.xlsx");
            }
        }
    }

    public class Event
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("src_number")]
        public string SrcNumber { get; set; }

        [JsonProperty("dst_number")]
        public string DstNumber { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("duration")]
        public int? Duration { get; set; }

        [JsonProperty("src_loc")]
        public List<double?> SrcLoc { get; set; }

        [JsonProperty("dst_loc")]
        public List<double?> DstLoc { get; set; }
    }

    public class Customer
    {
        public string PhoneNumber { get; set; }
    }
}