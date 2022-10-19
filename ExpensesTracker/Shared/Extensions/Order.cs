using ExpensesTracker.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Shared.Extensions
{
    public static class Order
    {
        public static List<Expense> OrderList(List<Expense> expensesList, bool Reverse = false)
        {
            Expense[] expenses = expensesList.ToArray();

            for (int i = 0; i < expenses.Length; i++)
            {
                for (int j = i + 1; j < expenses.Length; j++)
                {
                    if ((expenses[i].CompareTo(expenses[j]) > 0) && !Reverse)
                    {
                        swap<Expense>(ref expenses[i], ref expenses[j]);
                    }
                    else if ((expenses[i].CompareTo(expenses[j]) <= 0) && Reverse)
                    {
                        swap<Expense>(ref expenses[i], ref expenses[j]);
                    }
                }
            }
            return expenses.ToList();
        }

        static void swap<T>(ref T first, ref T second)
        {
            T temp = first;
            first = second;
            second = temp;
        }

    }
}