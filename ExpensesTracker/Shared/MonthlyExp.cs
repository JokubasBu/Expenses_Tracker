using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Shared
{
    public class MonthlyExp : IComparable
    {
        public int Id { get; set; } // Id is always primary key by default
        public double Money { get; set; }
        public string Comment { get; set; } = string.Empty;
        public Category? Category { get; set; }
        public int CategoryId { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public int CompareTo(object? obj)
        {
            MonthlyExp incomingexpense = obj as MonthlyExp;
            return this.Money.CompareTo(incomingexpense.Money);
        }
    }
}