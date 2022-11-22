using ExpensesTracker.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Shared.Extensions
{
    public class Delegates
    {
        public delegate double StatsIncome(List<Income> allIncomes);
        public delegate double StatsExpense(List<Expense> allIncomes);
        public static double CalculateIncome(List<Income> allIncomes)
        {
            double earned = 0;
            foreach (Income inc in allIncomes)
            {
                earned = earned + inc.Money;
            }
            return earned;
        }

        public static double CalculateExpense(List<Expense> allExpenses)
        {
            double earned = 0;
            foreach (Expense exp in allExpenses)
            {
                earned = earned + exp.Money;
            }
            return earned;
        }
    }
}