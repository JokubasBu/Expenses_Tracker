using ExpensesTracker.Client.Pages;
using ExpensesTracker.Server.Data;
using ExpensesTracker.Shared.Extensions;
using ExpensesTracker.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        static public List<MonthlyExp> currentExpenses = new List<MonthlyExp>();

        public MonthlyExpController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet] 
        public async Task<ActionResult<List<MonthlyExp>>> GetMonthlyExps()
        {
            currentExpenses = await GetAllExpenses();

            return Ok(await GetAllExpenses()); 
        }

        [HttpGet("currentCount")] // http methods should all be different, otherwise: The request matched multiple endpoints
        public async Task<ActionResult<List<MonthlyExp>>> GetOrderedMonthlyExps()
        {
            currentExpenses.Sort(); //ascending
            if (currentCount % 2 == 0)
            {
                currentExpenses.Reverse(); //descending (have to use sort beforehand for reverse to work)
            }
            currentCount++;

            return Ok(currentExpenses);
        }

        [HttpPost]
        public async Task<ActionResult<List<MonthlyExp>>> ShowFilter(MonthlyExp expenseFilter)
        {
            var expenses = await context.MonthlyExps.Include(e => e.Category).ToListAsync();
            currentCount = 0; //restart order
            currentExpenses = expenses.PickCategory(id: expenseFilter.CategoryId);
            currentExpenses = currentExpenses.PickMonth(monthNr: expenseFilter.Month);
            currentExpenses = currentExpenses.PickYear(year: expenseFilter.Year);

            // call filters for year and month (date)

            return Ok(currentExpenses);
        }


        [HttpGet("categories")] 
        public async Task<ActionResult<List<Category>>> GetCategories() 
        {
            var categories = await context.Categories.ToListAsync();
            return Ok(categories); 
        }

        [HttpGet("SetCurrent")]
        public async Task SetCurrentExpenses()
        {
            currentExpenses = await GetAllExpenses();
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

        [HttpPost("Add")]
        public async Task<ActionResult<List<MonthlyExp>>> CreateExp(MonthlyExp expense)
        {
            expense.Category = null;
            context.MonthlyExps.Add(expense);
            context.SaveChanges();
            currentExpenses.Add(expense);
            return Ok(await GetAllExpenses());
        }
       
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<MonthlyExp>>> DelteExpense(int id)
        {
            var dbExpense = await context.MonthlyExps.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);
            if (dbExpense == null)
                return NotFound("There is no such expense :/");

            context.MonthlyExps.Remove(dbExpense);
            await context.SaveChangesAsync();

            currentExpenses.RemoveAll(e => e.Id == id); //Remove(dbExpense) does not work not sure why?

            return Ok(currentExpenses);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<MonthlyExp>>> UpdateExpense(MonthlyExp expense, int id)
        {
            var dbExpense = await context.MonthlyExps.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);
            if (dbExpense == null)
                return NotFound("Sorry, but no hero for you. :/");

            dbExpense.Money = expense.Money;
            dbExpense.Comment = expense.Comment;
            dbExpense.CategoryId = expense.CategoryId;
            dbExpense.Year = expense.Year;
            dbExpense.Month = expense.Month;
            dbExpense.Day = expense.Day;

            await context.SaveChangesAsync();

            return Ok(await GetAllExpenses());
        }
        async Task<List<MonthlyExp>> GetAllExpenses()
        {
            return await context.MonthlyExps.Include(e => e.Category).ToListAsync();
        }

    }
}
