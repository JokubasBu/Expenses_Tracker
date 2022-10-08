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
        public static List<MonthlyExp> PickCategory(this List<MonthlyExp> expensesList, int id)
        {
            if (id != 0) // nothing is selected
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

        // implement methods for filtering by date (year and month, maybe seperately)

    }
}
