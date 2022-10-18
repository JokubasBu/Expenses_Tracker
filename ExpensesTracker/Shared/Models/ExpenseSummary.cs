using ExpensesTracker.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Shared.Models
{
    public class ExpenseSummary
    {
        public string category { get; set; } = string.Empty;
        public double totalExpenses { get; set; }

        public List<ExpenseSummary> GetSummary(List<Expense> allExpenses, int month)
        {
            var summary = new List<ExpenseSummary>();
            allExpenses = allExpenses.PickMonth(month);
            for (int i = 1; i < 5; i++)
            {
                ExpenseSummary temp = new ExpenseSummary();
                
                temp.totalExpenses = 0;
                bool once = true;
                foreach (Expense expense in allExpenses)
                {
                    
                    if (expense.CategoryId == i)
                    {
                        temp.totalExpenses += expense.Money;
                        if (once)
                        {
                            temp.category = expense.Category.Title;
                            once = false;
                        }
                    }                   
                }
                if(temp.totalExpenses > 0)
                {
                    summary.Add(new ExpenseSummary() { category = temp.category, totalExpenses = temp.totalExpenses });
                }  
            }
            return summary;
        }
    }
}
