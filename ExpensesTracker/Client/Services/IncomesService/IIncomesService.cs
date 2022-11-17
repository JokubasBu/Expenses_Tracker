namespace ExpensesTracker.Client.Services.IncomesService
{
    public interface IIncomesService
    {
        List<Income> AllIncomes { get; set; }
        Statistic Statistics { get; set; }
        Income incomeFilter { get; set; }
        Task GetIncomes();
        Task ShowFilters(Income incomeFilter);
        Task<Income> GetSingleIncome(int id);
        Task CreateIncome(Income income);
        Task UpdateIncome(Income income);
        Task DeleteIncome(int id);
        Task GetStatistics();
    }
}
