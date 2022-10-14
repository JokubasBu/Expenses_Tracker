using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Shared
{
    public class MonthlyExp : IComparable
    {
        private int _month;
        private int _year;
        private int _day;
        public int Id { get; set; } // Id is always primary key by default
        public double Money { get; set; }
        public string Comment { get; set; } = string.Empty;
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
        public int Year
        {
            get => _year;
            set
            {
                if ((value > 1999) && ( value < 2024))
                {
                    _year = value;
                }
            }
        }
        public int Month
        {
            get => _month;
            set
            {
                if ((value > 0) && (value < 13))
                {
                    _month = value;
                }
            }
        }
        public int Day
        {
            get => _day;
            set
            {
                if ((value > 0) && (value< 32))
                {
                    _day = value;
                }
}
        }

        public int CompareTo(object? obj)
        {
            MonthlyExp incomingexpense = obj as MonthlyExp;
            return this.Money.CompareTo(incomingexpense.Money);
        }
    }
}