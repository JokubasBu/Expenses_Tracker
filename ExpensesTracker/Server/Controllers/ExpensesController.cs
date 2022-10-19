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
    [Route("api/[controller]")] // the route will be taken from ExpensesController and it will read everything that is before controller
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly DataContext context;
        static int currentCount = 0; // amomunt of times the button Order was pressed
        static public List<Expense> currentExpenses = new List<Expense>();


        private static int _year;
        private static int _month;
        private static int _categoryId;


        public ExpensesController(DataContext context)

        {
            this.context = context;
        }
        [HttpGet("allExpenses")]
        public async Task<ActionResult<List<Expense>>> GetEveryExpense()
        {
            return Ok(await GetAllExpenses());
        }

        [HttpGet] 
        public async Task<ActionResult<List<Expense>>> GetExpenses()
        {
            return Ok(await GetFilteredExpenses()); 
        }

        [HttpGet("currentCount")] // http methods should all be different, otherwise: The request matched multiple endpoints
        public async Task<ActionResult<List<Expense>>> GetOrderedExpenses()
        {
            currentCount++;
            return Ok(await GetFilteredExpenses());
        }

        [HttpPost]
        public async Task<ActionResult<List<Expense>>> ShowFilter(Expense expenseFilter)
        {
            _categoryId = expenseFilter.CategoryId;
            _month =expenseFilter.Month;
            _year =expenseFilter.Year;

            return Ok(await GetFilteredExpenses());
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
        public async Task<ActionResult<List<Expense>>> GetSingleExpense(int id) 
        {
            var expense = await context.AllExpenses.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id); 

            if (expense == null) 
            {
                return NotFound("no entry..."); 
            }
            return Ok(expense);
        }

        [HttpPost("Add")]
        public async Task<ActionResult<List<Expense>>> CreateExpemse(Expense expense)
        {
            expense.Category = null;
            context.AllExpenses.Add(expense);
            context.SaveChanges();
            return Ok(await GetFilteredExpenses());
        }
       
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Expense>>> DeleteExpense(int id)
        {
            var dbExpense = await context.AllExpenses.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);
            if (dbExpense == null)
                return NotFound("There is no such expense :/");

            context.AllExpenses.Remove(dbExpense);
            await context.SaveChangesAsync();

            return Ok(await GetFilteredExpenses());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Expense>>> UpdateExpense(Expense expense, int id)
        {
            var dbExpense = await context.AllExpenses.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);
            if (dbExpense == null)
                return NotFound("Sorry, but no hero for you. :/");

            dbExpense.Money = expense.Money;
            dbExpense.Comment = expense.Comment;
            dbExpense.CategoryId = expense.CategoryId;
            dbExpense.Year = expense.Year;
            dbExpense.Month = expense.Month;
            dbExpense.Day = expense.Day;

            await context.SaveChangesAsync();

            return Ok(await GetFilteredExpenses());
        }
        async Task<List<Expense>> GetAllExpenses()
        {
            return await context.AllExpenses.Include(e => e.Category).ToListAsync();
        }

        async Task<List<Expense>> GetFilteredExpenses()
        {      
            var expenses = await context.AllExpenses.Include(e => e.Category).ToListAsync();

            currentExpenses = expenses.FilterBy(id: _categoryId);
            currentExpenses = currentExpenses.FilterBy(month: _month);
            currentExpenses = currentExpenses.FilterBy(year: _year);

            //currentExpenses.Sort(); //ascending
            currentExpenses = Order.OrderList(currentExpenses);
            if (currentCount % 2 == 0)
            {
                //currentExpenses.Reverse(); //descending (have to use sort beforehand for reverse to work)
                currentExpenses = Order.OrderList(currentExpenses, Reverse: true);
            }

            return currentExpenses;
        }

    }
}
