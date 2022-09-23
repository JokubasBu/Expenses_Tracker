using ExpensesTracker.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Server.Controllers
{
    [Route("api/[controller]")] // the route will be taken from MontlyExpController and it will read everything that is before controller
    [ApiController]
    public class MonthlyExpController : ControllerBase
    {
        public static List<Category> categories = new List<Category>
        {
            new Category {Id = 1, Title = "Groceries"},
            new Category {Id = 2, Title = "Transportation"},
            new Category {Id = 3, Title = "Health"},
            new Category {Id = 4, Title = "Entertainment"}
        };

        public static List<MonthlyExp> entries = new List<MonthlyExp>
        {
            new MonthlyExp {Id = 1, Money = 435, Comment = "Chicken has become quite expensive", Category = categories[0]},
            new MonthlyExp {Id = 2, Money = 25, Comment = "Buses are slow", Category = categories[1]},
            new MonthlyExp {Id = 3, Money = 1000, Comment = "partyyy", Category = categories[3]},
        };

        [HttpGet] // for swagger (api controller knows to look for get methods, swagger not so much?)
        public async Task<ActionResult<List<MonthlyExp>>> GetMonthlyExps() // specify (more comfortable for swagger?)
        {
            return Ok(entries); // everything is okay
        }

        [HttpGet("{id}")] //since we are using id as param in method, we have to specify it here as well
        public async Task<ActionResult<List<MonthlyExp>>> GetSingleExp(int id) 
        {
            var entry = entries.FirstOrDefault(e => e.Id == id); 

            if (entry == null) 
            {
                return NotFound("no entry..."); // error handling?
            }

            return Ok(entry); 
        }
    }
}
