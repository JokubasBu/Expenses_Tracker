using ExpensesTracker.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Server.Data.Repositories
{
    public class ExpenseRepo : IExpense
    {
        private readonly DataContext context;

        public ExpenseRepo(DataContext context)
        {
            this.context = context;
        }

        public async Task<ActionResult<Expense>> CreateExpenseAsync(Expense expense)
        {
            expense.Category = null;
            context.AllExpenses.Add(expense);
            await context.SaveChangesAsync();
            return expense;
        }

        public Task<ActionResult<Expense>> DeleteExpenseAsync(int id)
        {
            //var dbExpense = await context.AllExpenses.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);
            //if (dbExpense == null)
            //    return NotFound("There is no such expense :/");

            //context.AllExpenses.Remove(dbExpense);
            //await context.SaveChangesAsync();

            throw new NotImplementedException();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<List<Expense>> GetExpensesAsync()
        {
            return await context.AllExpenses.Include(e => e.Category).ToListAsync();
        }

        public async Task<ActionResult<Expense>> GetSingleExpenseAsync(int id)
        {
            return await context.AllExpenses.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<ActionResult<List<Expense>>> UpdateExpenseAsync(Expense expense, int id)
        {
            var dbExpense = await context.AllExpenses.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);

            dbExpense.Money = expense.Money;
            dbExpense.Comment = expense.Comment;
            dbExpense.CategoryId = expense.CategoryId;
            dbExpense.Year = expense.Year;
            dbExpense.Month = expense.Month;
            dbExpense.Day = expense.Day;

            await context.SaveChangesAsync();

            //return expense;

        }
    }
}
