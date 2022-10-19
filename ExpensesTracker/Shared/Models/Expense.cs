using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Shared.Models
{
    public class Expense : IComparable
    {
        private int _month;
        private int _year;
        private int _day;
        public int Id { get; set; } // Id is always primary key by default
        [Required]
        [RegularExpression(@"^[0-9]+.?[0-9]*$", ErrorMessage = "Use Non-negative Number for Money field!")]
        public double Money { get; set; }
        [Required]
        [RegularExpression(@"^(?=.{1,200}$).*", ErrorMessage = "Comment character limit is 200!")]
        public string Comment { get; set; } = string.Empty;
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
        public int Year
        {
            get => _year;
            set
            {
                if ((value > 1999 && value < Dates.Years.Max() + 1) || (value == 0))
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
                if (value > -1 && value < 13)
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
                if (value > 0 && value < 32)
                {
                    _day = value;
                }
            }
        }

        public int CompareTo(object? obj)
        {
            Expense incomingexpense = obj as Expense;
            return Money.CompareTo(incomingexpense.Money);
        }
    }
}