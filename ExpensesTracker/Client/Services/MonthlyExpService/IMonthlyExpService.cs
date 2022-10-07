namespace ExpensesTracker.Client.Services.MonthlyExpService
{
    public interface IMonthlyExpService
    {
        List<MonthlyExp> MonthlyExps { get; set; } 
        List<Category> Categories { get; set; }
        Task GetCategories();
        Task GetMonthlyExps();
        Task GetOrderedMonthlyExps();
        Task ShowCategory(Category category);
        Task<MonthlyExp> GetSingleExp(int id);
        Task CreateExp(MonthlyExp hero);
        Task UpdateExp(MonthlyExp hero);
        Task DeleteExp(int id);
    }
}
