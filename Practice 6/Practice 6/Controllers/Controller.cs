using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    }
}
