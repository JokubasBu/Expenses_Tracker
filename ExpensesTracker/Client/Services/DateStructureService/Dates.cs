namespace ExpensesTracker.Client.Services.DateStructureService

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

        private IEnumerable<int> _years;
        
        public IEnumerable<int> Years
        { 
            get{
                return _years;
            }
            set
            {
                _years = Enumerable.Range(2000, DateTime.Now.Year - 1999);
            }
        }
    }
}
