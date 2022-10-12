namespace ExpensesTracker.Client.Services.FileService
{
    public interface IFileService
    {
        List<MonthlyExp> fileExps{ get; set; }
        Task<List<MonthlyExp>> GetFileEksps(string fileName);

    }
}
