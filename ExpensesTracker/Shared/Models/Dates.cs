namespace ExpensesTracker.Shared.Models

{
    public struct Dates
    {
        public enum Months
        {
            January = 1,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }
        public static IEnumerable<int> Years
        {
            get
            {
                return Enumerable.Range(2000, DateTime.Now.Year - 1999).Reverse();
            }
        }
    }
}
