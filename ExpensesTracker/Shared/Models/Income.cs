using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Shared.Models
{
    public class Income
    {
        private int _month;
        private int _year;

        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+.?[0-9]*$", ErrorMessage = "Use Non-negative Number for Money field!")]
        public double Money { get; set; }
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
    }
}
