namespace ExpensesTracker.Client.Services.IncomesService
{
    public interface IIncomesService
    {
        List<Income> AllIncomes { get; set; }
        Task GetIncomes();
        Task ShowFilters(Income incomeFilter);
        Task<Income> GetSingleIncome(string date);
        Task CreateOrUpdateIncome(Income income);
        Task DeleteIncome(string date);
    }
}
