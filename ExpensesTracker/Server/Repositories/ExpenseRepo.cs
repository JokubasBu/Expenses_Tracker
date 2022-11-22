using ExpensesTracker.Server.Repositories.Interfaces;
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

        public async Task<ActionResult<Expense>> DeleteExpenseAsync(Expense dbExpense)
        {
            context.AllExpenses.Remove(dbExpense);
            await context.SaveChangesAsync();

            return dbExpense;
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

        public async Task<Expense> UpdateExpenseAsync(Expense expense, Expense dbExpense)
        {
            dbExpense.Money = expense.Money;
            dbExpense.Comment = expense.Comment;
            dbExpense.CategoryId = expense.CategoryId;
            dbExpense.Year = expense.Year;
            dbExpense.Month = expense.Month;
            dbExpense.Day = expense.Day;

            await context.SaveChangesAsync();

            return expense;

        }
    }
}
