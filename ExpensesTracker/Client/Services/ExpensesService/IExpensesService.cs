using Microsoft.AspNetCore.Components.Forms;

namespace ExpensesTracker.Client.Services.ExpensesService
{
    public interface IExpensesService
    {
        List<Expense> AllExpenses { get; set; }
        List<Category> Categories { get; set; }
        List<ExpenseSummary> Summary { get; set; }
        List<ExpensesTree> ExpensesTree { get; set; }
        Statistic Statistics { get; set; }
        Task GetCategories();
        Task GetExpenses();
        Task GetOrderedExpenses();
        Task ShowFilters(Expense expenseFilter);
        Task<Expense> GetSingleExpense(int id);
        Task CreateExpense(Expense expense);
        Task UpdateExpense(Expense expense);
        Task DeleteExpense(int id);
        Task GetSummary();
        Task GetStatistics();
        void GetExpensesTree();
    }
}
