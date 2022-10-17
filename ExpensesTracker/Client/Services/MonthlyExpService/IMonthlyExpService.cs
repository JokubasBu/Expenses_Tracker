using Microsoft.AspNetCore.Components.Forms;

namespace ExpensesTracker.Client.Services.MonthlyExpService
{
    public interface IMonthlyExpService
    {
        List<MonthlyExp> AllExpenses { get; set; } 
        List<Category> Categories { get; set; }
        Task GetCategories();
        Task GetExpenses();
        Task GetOrderedExpenses();
        Task ShowFilters(MonthlyExp expenseFilter);
        Task<MonthlyExp> GetSingleExpense(int id);
        Task CreateExpense(MonthlyExp expense);
        Task UpdateExpense(MonthlyExp expense);
        Task DeleteExpense(int id);
    }
}
