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
    }
}
