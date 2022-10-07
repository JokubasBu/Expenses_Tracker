using ExpensesTracker.Client.Pages;
using ExpensesTracker.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
        static List<MonthlyExp> currentList = new List<MonthlyExp>();

        public MonthlyExpController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet] // for swagger (api controller knows to look for get methods, swagger not so much)
        public async Task<ActionResult<List<MonthlyExp>>> GetMonthlyExps() // specify (more comfortable for swagger)
        {
            var expenses = await context.MonthlyExps.Include(e => e.Category).ToListAsync();

            return Ok(expenses); 
        }

        [HttpGet("currentCount")] // http methods should all be different, otherwise: The request matched multiple endpoints
        public async Task<ActionResult<List<MonthlyExp>>> GetOrderedMonthlyExps()
        {
            if (!currentList.Any())
            {
                var expenses = await context.MonthlyExps.Include(e => e.Category).ToListAsync();
                currentList = expenses;
            }

            currentList.Sort(); //ascending
            if (currentCount % 2 == 0)
            {
                currentList.Reverse(); //descending (have to use sort beforehand for reverse to work)
            }
            currentCount++;

            return Ok(currentList);
        }

        [HttpPost]
        public async Task<ActionResult<List<MonthlyExp>>> ShowCategory(Category category)
        {
            if (!currentList.Any())
            {
                var expenses = await context.MonthlyExps.Include(e => e.Category).ToListAsync();
                currentList = expenses;
            }
            var expenseC = currentList;
            currentList = currentList.PickCategory(id: category.Id);

            return Ok(currentList);
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
