using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Server.Repositories.Interfaces
{
    public interface IIncome
    {
        Task<ActionResult<Income>> CreateIncomeAsync(Income income);
        Task<ActionResult<Income>> DeleteIncomeAsync(Income dbIncome);
        Task<List<Income>> GetIncomeAsync();
        Task<ActionResult<Income>> GetSingleIncomeAsync(int id);
        Task<Income> UpdateIncomeAsync(Income income, Income dbIncome);
    }
}