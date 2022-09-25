using ExpensesTracker.Server.Data;
using ExpensesTracker.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Server.Controllers
{
    [Route("api/[controller]")] // the route will be taken from MontlyExpController and it will read everything that is before controller
    [ApiController]
    public class MonthlyExpController : ControllerBase
    {
        private readonly DataContext context;

        /*
* was used to create and push to db
public static List<Category> categories = new List<Category>
{
new Category {Id = 1, Title = "Groceries"},
new Category {Id = 2, Title = "Transportation"},
new Category {Id = 3, Title = "Health"},
new Category {Id = 4, Title = "Entertainment"}
};
public static List<MonthlyExp> expenses = new List<MonthlyExp>
{
new MonthlyExp {Id = 1, Money = 435, Comment = "Chicken has become quite expensive", Category = categories[0], CategoryId = 1},
new MonthlyExp {Id = 2, Money = 25, Comment = "Buses are slow", Category = categories[1], CategoryId = 2},
new MonthlyExp {Id = 3, Money = 1000, Comment = "partyyy", Category = categories[3], CategoryId = 3},
};
*/

        public MonthlyExpController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet] // for swagger (api controller knows to look for get methods, swagger not so much?)
        public async Task<ActionResult<List<MonthlyExp>>> GetMonthlyExps() // specify (more comfortable for swagger?)
        {
            //var expenses = await context.MonthlyExps.ToListAsync();
            var expenses = await context.MonthlyExps.Include(e => e.Category).ToListAsync();
            return Ok(expenses); // everything is okay
        }

        [HttpGet("categories")] 
        public async Task<ActionResult<List<Category>>> GetCategories() // specify (more comfortable for swagger?)
        {
            var categories = await context.Categories.ToListAsync();
            return Ok(categories); // everything is okay
        }

        [HttpGet("{id}")] //since we are using id as param in method, we have to specify it here as well
        public async Task<ActionResult<List<MonthlyExp>>> GetSingleExp(int id) 
        {
            var expense = await context.MonthlyExps.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);  // relationship tutorial

            if (expense == null) 
            {
                return NotFound("no entry..."); // error handling?
            }
            return Ok(expense);
        }
    }
}
