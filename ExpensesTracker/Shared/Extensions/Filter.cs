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
        public static List<Expense> PickCategory(this List<Expense> expensesList, int id)
        {
            if (id != 0) 
            {
                var expenseCategory =
                    from allExpense in expensesList
                    where allExpense.CategoryId == id
                    select allExpense;
                return expenseCategory.ToList();
            }
            else
            {
                return expensesList;
            }
        }

        public static List<Expense> PickMonth(this List<Expense> expensesList, int monthNr)
        {
            if (monthNr != 0)
            {
                var expenseMonth =
                    from allExpense in expensesList
                    where allExpense.Month == monthNr
                    select allExpense;
                return expenseMonth.ToList();
            }
            else
            {
                return expensesList;
            }
        }
        public static List<Expense> PickYear(this List<Expense> expensesList, int year)
        {
            if (year != 0)
            {
                var expenseYear =
                    from allExpense in expensesList
                    where allExpense.Year == year
                    select allExpense;
                return expenseYear.ToList();
            }
            else
            {
                return expensesList;
            }
        }

    }
}
