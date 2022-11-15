namespace ExpensesTracker.Client.Services.IncomesService
{
    public class IncomesService : IIncomesService
    {
        public List<Income> AllIncomes { get; set; } = new List<Income>();
    }
}
