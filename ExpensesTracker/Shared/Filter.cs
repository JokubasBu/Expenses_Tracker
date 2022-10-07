using ExpensesTracker.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Shared
{
    public static class Filter
    {
        public static List<MonthlyExp> PickCategory(this List<MonthlyExp> expenses, int id)
        {
            var expenseWithHealth =
                from allExpense in expenses
                where allExpense.CategoryId == id
                select allExpense;
            return expenseWithHealth.ToList();
        }
    }
}
