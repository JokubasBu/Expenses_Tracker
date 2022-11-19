using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Server.Interfaces
{
    public interface IExpense
    {
        Task<List<Expense>> GetExpensesAsync();
        Task<ActionResult<Expense>> GetSingleExpenseAsync(int id);
        Task<ActionResult<Expense>> CreateExpenseAsync(Expense expense);
        Task<ActionResult<Expense>> DeleteExpenseAsync(int id);
        Task<ActionResult<List<Expense>>> UpdateExpenseAsync(Expense expense, int id);
        Task<List<Category>> GetCategoriesAsync();
    }
}
