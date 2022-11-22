using ExpensesTracker.Client.Pages;
using ExpensesTracker.Server.Data;
using ExpensesTracker.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ExpensesTracker.Shared.Extensions.Delegates;

namespace ExpensesTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomesController : ControllerBase
    {
        private readonly IIncome _incomes;

        private static int _year;
        private static int _month;

        public IncomesController(IIncome incomes)
        {
            _incomes = incomes;
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
            var incomes = await _incomes.GetIncomeAsync();

            var currentIncomes = incomes.FilterBy(month: _month);
            currentIncomes = currentIncomes.FilterBy(year: _year);

            return currentIncomes;
        }

        [HttpGet("{id}")] //since we are using id as param in method, we have to specify it here as well
        public async Task<ActionResult<List<Income>>> GetSingleIncome(int id)
        {
            var income = await _incomes.GetSingleIncomeAsync(id);

            if (income == null)
            {
                return NotFound("no entry...");
            }
            return Ok(income.Value);
        }

        [HttpPost("Add")]
        public async Task<ActionResult<List<Income>>> CreateIncome(Income income)
        {
            await _incomes.CreateIncomeAsync(income);
            return Ok(await GetFilteredIncomes());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Income>>> UpdateIncome(Income income, int id)
        {
            var dbIncome = await _incomes.GetSingleIncomeAsync(id);
            if (dbIncome == null)
                return NotFound("Income not found");

            await _incomes.UpdateIncomeAsync(income, dbIncome.Value);

            return Ok(await GetFilteredIncomes());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Income>>> DeleteIncome(int id)
        {
            var dbIncome = await _incomes.GetSingleIncomeAsync(id);
            if (dbIncome == null)
                return NotFound("Income not found");

            await _incomes.DeleteIncomeAsync(dbIncome.Value);

            return Ok(await GetFilteredIncomes());
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<List<Statistic>>> GetStatistics()
        {
            var stats = new Statistic();
            var allIncomes = await _incomes.GetIncomeAsync();
            StatsIncome del = new StatsIncome(CalculateIncome);

            allIncomes = allIncomes.FilterBy(year: DateTime.Now.Year);
            stats.yearStat = del(allIncomes);

            allIncomes = allIncomes.FilterBy(month: DateTime.Now.Month);
            stats.monthStat = del(allIncomes);

            return Ok(stats);
        }
    }
}
    
