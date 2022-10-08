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
            var expenseCategory =
                from allExpense in expensesList
                where allExpense.CategoryId == id
                select allExpense;
            return expenseCategory.ToList();

            //List<MonthlyExp> expenseCategory = new List<MonthlyExp>();
            //foreach(MonthlyExp exp in expensesList)
            //{
            //    if(exp.CategoryId == id)
            //    {
            //        expenseCategory.Add(exp);
            //    }
            //}
            //return expenseCategory;
        }
    }
}
