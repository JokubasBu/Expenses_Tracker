using ExpensesTracker.Client.Pages;
using ExpensesTracker.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using static System.Net.WebRequestMethods;

namespace ExpensesTracker.Server.Controllers
{
    [Route("api/[controller]")] // the route will be taken from MonthlyExpController and it will read everything that is before controller
    [ApiController]
    public class MonthlyExpController : ControllerBase
    {
        private readonly DataContext context;
        static int currentCount = 0; // amomunt of times the button Order was pressed
        static List<MonthlyExp> currentExpenses = new List<MonthlyExp>();

        public MonthlyExpController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet] 
        public async Task<ActionResult<List<MonthlyExp>>> GetMonthlyExps()
        {
            var expenses = await context.MonthlyExps.Include(e => e.Category).ToListAsync();

            return Ok(expenses); 
        }

        [HttpGet("currentCount")] // http methods should all be different, otherwise: The request matched multiple endpoints
        public async Task<ActionResult<List<MonthlyExp>>> GetOrderedMonthlyExps()
        {
            if (!currentExpenses.Any())
            {
                var expenses = await context.MonthlyExps.Include(e => e.Category).ToListAsync();
                currentExpenses = expenses;
            }

            currentExpenses.Sort(); //ascending
            if (currentCount % 2 == 0)
            {
                currentExpenses.Reverse(); //descending (have to use sort beforehand for reverse to work)
            }
            currentCount++;

            return Ok(currentExpenses);
        }

        [HttpPost]
        public async Task<ActionResult<List<MonthlyExp>>> ShowCategory(Category category)
        {
            var expenses = await context.MonthlyExps.Include(e => e.Category).ToListAsync();
            currentExpenses = expenses.PickCategory(id: category.Id);
            currentCount = 0; //restart order

            return Ok(currentExpenses);
        }


        [HttpGet("categories")] 
        public async Task<ActionResult<List<Category>>> GetCategories() 
        {
            var categories = await context.Categories.ToListAsync();
            return Ok(categories); 
        }

        [HttpGet("{id}")] //since we are using id as param in method, we have to specify it here as well
        public async Task<ActionResult<List<MonthlyExp>>> GetSingleExp(int id) 
        {
            var expense = await context.MonthlyExps.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id); 

            if (expense == null) 
            {
                return NotFound("no entry..."); 
            }
            return Ok(expense);
        }
    }
}
