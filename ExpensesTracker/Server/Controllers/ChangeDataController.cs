using ExpensesTracker.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChangeDataController : ControllerBase
	{
        private readonly DataContext context;
        public ChangeDataController(DataContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<List<MonthlyExp>>> CreateExp(MonthlyExp expense)
        {
            expense.Category = null;
            context.MonthlyExps.Add(expense);
            context.SaveChanges();
            return Ok(await context.MonthlyExps.Include(e => e.Category).ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<MonthlyExp>>> DelteExpense(int id)
        {
            var dbExpense = await context.MonthlyExps.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);
            if (dbExpense == null)
                return NotFound("There is no such expense :/");

            context.MonthlyExps.Remove(dbExpense);
            await context.SaveChangesAsync();

            MonthlyExpController.currentExpenses.RemoveAll(e => e.Id == id); //Remove(dbExpense) does not work not sure why?

            return Ok(MonthlyExpController.currentExpenses);
        }
    }
}
