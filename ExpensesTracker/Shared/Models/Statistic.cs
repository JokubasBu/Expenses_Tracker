using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Shared.Models
{
    public class Statistic
    {
        //public double currentMonthTotalExpenses { get; set; }
        public double previousMonthTotalExpenses { get; set; }
        public double currentYearTotalExpenses { get; set; }
    }
}
