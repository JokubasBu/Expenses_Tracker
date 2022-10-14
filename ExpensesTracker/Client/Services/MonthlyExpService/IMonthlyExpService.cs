namespace ExpensesTracker.Client.Services.MonthlyExpService
{
    public interface IMonthlyExpService
    {
        List<MonthlyExp> MonthlyExps { get; set; } 
        List<Category> Categories { get; set; }
        Task GetCategories();
        Task GetMonthlyExps();
        Task GetOrderedMonthlyExps();
        Task ShowFilters(MonthlyExp expenseFilter);
        Task<MonthlyExp> GetSingleExp(int id);
        Task CreateExpense(MonthlyExp hero);
        Task UpdateExpense(MonthlyExp hero);
        Task DeleteExpense(int id);
    }
}
