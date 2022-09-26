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
