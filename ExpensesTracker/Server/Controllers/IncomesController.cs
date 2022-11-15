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
        public async Task<ActionResult<List<Income>>> GetSingleIncome(int id)
        {
            var income = await context.AllIncomes.FirstOrDefaultAsync(i => i.Id == id);

            if (income == null)
            {
                return NotFound("no entry...");
            }
            return Ok(income);
        }

        [HttpPost("Add")]
        public async Task<ActionResult<List<Income>>> CreateIncome(Income income)
        {
            context.AllIncomes.Add(income);
            context.SaveChanges();
            return Ok(await GetFilteredIncomes());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Income>>> UpdateIncome(Income income, int id)
        {
            var incomeSearch = await context.AllIncomes.FirstOrDefaultAsync(e => e.Id == id);
            if (incomeSearch == null)
                return NotFound("Sorry :/");

            incomeSearch.Money = income.Money;
            incomeSearch.Year = income.Year;
            incomeSearch.Month = income.Month;

            await context.SaveChangesAsync();

            return Ok(await GetFilteredIncomes());
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<List<Statistic>>> GetStatistics()
        {
            var stats = new Statistic();
            var allIncomes = await context.AllIncomes.ToListAsync();

            allIncomes = allIncomes.FilterBy(year: DateTime.Now.Year);
            double earnedThisYear = 0;
            foreach (Income inc in allIncomes)
            {
                earnedThisYear = earnedThisYear + inc.Money;
            }
            stats.yearStat = earnedThisYear;

            allIncomes = allIncomes.FilterBy(month: DateTime.Now.Month);
            double earnedThisMonth = 0;
            foreach (Income inc in allIncomes)
            {
                earnedThisMonth = earnedThisMonth + inc.Money;
            }
            stats.monthStat = earnedThisMonth;

            return Ok(stats);
        }
    }
}
    
