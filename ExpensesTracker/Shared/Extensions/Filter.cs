using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpensesTracker.Shared.Models;

namespace ExpensesTracker.Shared.Extensions
{
    public static class Filter
    {
        public static List<Expense> FilterBy(this List<Expense> expensesList, int id = 0, int month = 0, int year = 0)
        {
            if (id != 0) // nothing is selected
            {
                var expenseCategory =
                    from allExpense in expensesList
                    where allExpense.CategoryId == id
                    select allExpense;
                return expenseCategory.ToList();
            }
            if (month != 0) 
            {
                var expenseMonth =
                    from allExpense in expensesList
                    where allExpense.Month == month
                    select allExpense;
                return expenseMonth.ToList();
            }
            if (year != 0) 
            {
                var expenseYear =
                    from allExpense in expensesList
                    where allExpense.Year == year
                    select allExpense;
                return expenseYear.ToList();
            }
            return expensesList;

        }

        public static List<Income> FilterBy(this List<Income> incomesList, int month = 0, int year = 0)
        {
            if (month != 0)
            {
                var incomeMonth =
                    from allIncome in incomesList
                    where allIncome.Month == month
                    select allIncome;
                return incomeMonth.ToList();
            }
            if (year != 0)
            {
                var incomeYear =
                    from allIncome in incomesList
                    where allIncome.Year == year
                    select allIncome;
                return incomeYear.ToList();
            }
            return incomesList;

        }

    }
}
