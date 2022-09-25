using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ExpensesTracker.Client.Services.MonthlyExpService
{
    public class MonthlyExpService : IMonthlyExpService
    {
        private readonly HttpClient http;
        private readonly NavigationManager navigationManager;

        public MonthlyExpService(HttpClient http, NavigationManager navigationManager)
        {
            this.http = http;
            this.navigationManager = navigationManager;
        }
        public List<MonthlyExp> MonthlyExps { get; set; } = new List<MonthlyExp>();
        public List<Category> Categories { get; set; } = new List<Category>();

        public Task CreateExp(MonthlyExp hero)
        {
            throw new NotImplementedException();
        }

        public Task DeleteExp(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetCategories()
        {
            throw new NotImplementedException();
        }

        public async Task GetMonthlyExps()
        {
            var result = await http.GetFromJsonAsync<List<MonthlyExp>>("api/monthlyexp");
            if (result != null)
            {
                MonthlyExps = result;
            }
        }

        public async Task<MonthlyExp> GetSingleExp(int id)
        {
            var result = await http.GetFromJsonAsync<MonthlyExp>("api/monthlyexp/{id}");
            if (result != null)
            {
                return result;
            }
            throw new Exception("not found whoops");
        }

        public Task UpdateExp(MonthlyExp hero)
        {
            throw new NotImplementedException();
        }
    }
}
