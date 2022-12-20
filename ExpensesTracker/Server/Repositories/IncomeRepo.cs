using ExpensesTracker.Server.Data;
using ExpensesTracker.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Server.Repositories
{
    public class IncomeRepo : IIncome
    {
        private readonly DataContext context;

        public IncomeRepo(DataContext context)
        {
            this.context = context;
        }

        public async Task<ActionResult<Income>> CreateIncomeAsync(Income income)
        {
            context.AllIncomes.Add(income);
            await context.SaveChangesAsync();
            return income;
        }

        public async Task<ActionResult<Income>> DeleteIncomeAsync(Income dbIncome)
        {
            context.AllIncomes.Remove(dbIncome);
            await context.SaveChangesAsync();

            return dbIncome;
        }

        public async Task<List<Income>> GetIncomeAsync()
        {
            return await context.AllIncomes.ToListAsync();
        }

        public async Task<ActionResult<Income>> GetSingleIncomeAsync(int id)
        {
            return await context.AllIncomes.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Income> UpdateIncomeAsync(Income income, Income dbIncome)
        {
            dbIncome.Money = income.Money;
            dbIncome.Year = income.Year;
            dbIncome.Month = income.Month;

            await context.SaveChangesAsync();

            return income;

        }
    }
}
