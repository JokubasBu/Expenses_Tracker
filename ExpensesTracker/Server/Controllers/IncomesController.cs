using ExpensesTracker.Server.Data;
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
    }
    
}
