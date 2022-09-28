namespace ExpensesTracker.Client.Services.MonthlyExpService
{
    public interface IMonthlyExpService
    {
        List<MonthlyExp> MonthlyExps { get; set; } //IEnumerable<MonthlyExp> MonthlyExps { get; set; }
        List<Category> Categories { get; set; }
        Task GetCategories();
        Task GetMonthlyExps();
        Task<MonthlyExp> GetSingleExp(int id);
        Task CreateExp(MonthlyExp hero);
        Task UpdateExp(MonthlyExp hero);
        Task DeleteExp(int id);
    }
}
