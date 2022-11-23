using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Shared.Extensions
{
    public class Sort
    {
        public static List<T> ListSort<T>(List<T> data, string sortExpression, bool descending = false)
        {
            List<T>? sortedList = new List<T>();
            if (!descending)
            {
                sortedList = (from n in data
                              orderby GetSortProperty(n, sortExpression) ascending
                              select n).ToList();
            }
            else
            {
                sortedList = (from n in data
                              orderby GetSortProperty(n, sortExpression) descending
                              select n).ToList();
            }
            return sortedList;
        }

        public static object GetSortProperty(object item, string propName)
        {
            return item.GetType().GetProperty(propName).GetValue(item, null);
        }
    }
}
