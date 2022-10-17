using Microsoft.AspNetCore.Components.Forms;

namespace ExpensesTracker.Client.Services.MonthlyExpService
{
    public interface IMonthlyExpService
    {
        List<MonthlyExp> AllExpenses { get; set; } 
        List<Category> Categories { get; set; }
        Task GetCategories();
        Task GetMonthlyExps();
        Task GetOrderedMonthlyExps();
        Task ShowFilters(MonthlyExp expenseFilter);
        Task<MonthlyExp> GetSingleExp(int id);
        Task CreateExpense(MonthlyExp expense);
        Task UpdateExpense(MonthlyExp expense);
        Task DeleteExpense(int id);
    }
}
