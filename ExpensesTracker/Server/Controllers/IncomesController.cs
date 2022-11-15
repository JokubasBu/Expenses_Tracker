using ExpensesTracker.Client.Pages;
using ExpensesTracker.Server.Data;
using ExpensesTracker.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomesController : ControllerBase
    {
        private readonly DataContext context;
        private static int _year;
        private static int _month;

        public IncomesController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Income>>> GetIncomes()
        {
            return Ok(await GetFilteredIncomes());
        }

        [HttpPost]
        public async Task<ActionResult<List<Income>>> ShowFilter(Income incomeFilter)
        {
            _month = incomeFilter.Month;
            _year = incomeFilter.Year;

            return Ok(await GetFilteredIncomes());

        }
        async Task<List<Income>> GetFilteredIncomes()
        {
            var incomes = await context.AllIncomes.ToListAsync();

            var currentIncomes = incomes.FilterBy(month: _month);
            currentIncomes = currentIncomes.FilterBy(year: _year);

            return currentIncomes;
        }

        [HttpGet("{id}")] //since we are using id as param in method, we have to specify it here as well
        public async Task<ActionResult<List<Income>>> GetSingleIncome(string date)
        {
            var income = await context.AllIncomes.FirstOrDefaultAsync(i => i.Date == date);

            if (income == null)
            {
                return NotFound("no entry...");
            }
            return Ok(income);
        }

        [HttpPost("Add")]
        public async Task<ActionResult<List<Income>>> CreateorUpdateIncome(Income income)
        {
            var searchIncome = await context.AllIncomes.FirstOrDefaultAsync(i => i.Date == income.Date);

            if (searchIncome == null) // new entry, date doesnt exist
            {
                context.AllIncomes.Add(income);
                context.SaveChanges();
                return Ok(await GetFilteredIncomes());
            }
            else // date already exists in DB
            {
                searchIncome.Money = searchIncome.Money + income.Money;
                searchIncome.Year = income.Year;
                searchIncome.Month = income.Month;
                searchIncome.Date = income.Year + "-" + income.Month.ToString("D2");

                await context.SaveChangesAsync();

                return Ok(await GetFilteredIncomes());
            }
            
        }
    }
    
}
