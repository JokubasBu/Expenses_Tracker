using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Server.Interfaces
{
    public interface IExpense
    {
        Task<List<Expense>> GetFilteredExpensesAsync();
        Task<ActionResult<Expense>> GetSingleExpenseAsync(int id);
        Task<ActionResult<Expense>> CreateExpenseAsync(Expense expense);
        Task<ActionResult<Expense>> DeleteExpenseAsync(int id);
        Task<ActionResult<List<Category>>> GetCategoriesAsync();
    }
}
