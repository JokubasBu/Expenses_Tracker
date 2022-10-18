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
        public static List<Expense> OrderByDesc(List<Expense> expensesList, bool Money = false, bool Date = false)
        {
            Expense[] expenses = expensesList.ToArray();

            if (Money)
            {
                for (int i = 0; i < expenses.Length; i++)
                {
                    for (int j = i + 1; j < expenses.Length; j++)
                    {
                        if (expenses[i].CompareTo(expenses[j]) > 0)
                        {
                            swap<Expense>(ref expenses[i], ref expenses[j]);
                        }
                    }
                }
                return expenses.ToList();
            }
            if (Date)
            {
                for (int i = 0; i < expenses.Length; i++)
                {
                    for (int j = i + 1; j < expenses.Length; j++)
                    {
                        if (expenses[i].CompareTo(expenses[j]) > 0)
                        {
                            swap<Expense>(ref expenses[i], ref expenses[j]);
                        }
                    }
                }
                return expenses.ToList();
            }
            return expenses.ToList();
        }

        public static List<Expense> OrderByAsc(List<Expense> expensesList, bool Money = false, bool Date = false)
        {
            Expense[] expenses = expensesList.ToArray();

            if (Money)
            {
                for (int i = 0; i < expenses.Length; i++)
                {
                    for (int j = i + 1; j < expenses.Length; j++)
                    {
                        if (expenses[i].CompareTo(expenses[j]) <= 0)
                        {
                            swap<Expense>(ref expenses[i], ref expenses[j]);
                        }
                    }
                }
                return expenses.ToList();
            }
            if (Date)
            {
                for (int i = 0; i < expenses.Length; i++)
                {
                    for (int j = i + 1; j < expenses.Length; j++)
                    {
                        if (expenses[i].CompareTo(expenses[j]) <= 0)
                        {
                            swap<Expense>(ref expenses[i], ref expenses[j]);
                        }
                    }
                }
                return expenses.ToList();
            }
            return expensesList;
        }

        static void swap<T>(ref T first, ref T second)
        {
            T temp = first;
            first = second;
            second = temp;
        }

    }
}