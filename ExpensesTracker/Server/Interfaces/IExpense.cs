using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Server.Interfaces
{
    public interface IExpense
    {
        Task<List<Expense>> GetExpensesAsync();
        Task<ActionResult<Expense>> GetSingleExpenseAsync(int id);
        Task<ActionResult<Expense>> CreateExpenseAsync(Expense expense);
        Task<ActionResult<Expense>> DeleteExpenseAsync(Expense expense);
        Task<Expense> UpdateExpenseAsync(Expense expense, Expense dbExpense);
        Task<List<Category>> GetCategoriesAsync();
    }
}
