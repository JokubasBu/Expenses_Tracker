using System.Net.Http.Json;

namespace ExpensesTracker.Client.Services.FileService
{
    public class FileService : IFileService
    {
        public List<MonthlyExp> fileExps { get; set; } = new List<MonthlyExp>();
        private readonly HttpClient client;

        public FileService(HttpClient http)
        {
            client = http;
        }
        public async Task<List<MonthlyExp>> GetFileEksps(string fileName)
        {
            return await client.GetFromJsonAsync<List<MonthlyExp>>("/api/FileContent/{fileName}");
        }
    }
}
