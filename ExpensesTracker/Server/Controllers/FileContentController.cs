using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace ExpensesTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileContentController : ControllerBase
    {
        private readonly HttpClient client;
        public List<MonthlyExp> contents { get; set; }

        public FileContentController(HttpClient http)
        {
            http = client;
        }


        [HttpGet("{fileName}")]
        public async Task<ActionResult<List<MonthlyExp>>> GetFileContents(string fileName)
        {
            HttpResponseMessage response = await client.GetAsync(fileName);
            string items = await response.Content.ReadAsStringAsync();
            string[] arr = items.Split(' ');
            contents.Add(new MonthlyExp { Id = Int32.Parse(arr[0]), Money = Int32.Parse(arr[1]), Comment= arr[3] });
            return Ok(contents);
        }

    }
}
