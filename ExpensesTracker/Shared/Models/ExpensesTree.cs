using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Shared.Models
{
    public class ExpensesTree
    {
        public string name { get; set; } = string.Empty;
        public ExpensesTreeChildren[]? children; 
}
    public class ExpensesTreeChildren
    {
        public string name { get; set; } = string.Empty;
        public double value { get; set; }
    }
}
