using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace ExpensesTracker.Client.Services.IncomesService
{
    public class IncomesService : IIncomesService
    {
        private readonly HttpClient http;
        private readonly NavigationManager navigationManager;

        public IncomesService(HttpClient http, NavigationManager navigationManager)
        {
            this.http = http;
            this.navigationManager = navigationManager;
        }
        public List<Income> AllIncomes { get; set; } = new List<Income>();

        public Task CreateOrUpdateIncome(Income income)
        {
            throw new NotImplementedException();
        }

        public Task DeleteIncome(string date)
        {
            throw new NotImplementedException();
        }

        public async Task GetIncomes()
        {
            var result = await http.GetFromJsonAsync<List<Income>>("api/incomes");
            if (result != null)
            {
                AllIncomes = result;
            }
        }

        public Task<Income> GetSingleIncome(string date)
        {
            throw new NotImplementedException();
        }

        public async Task ShowFilters(Income incomeFilter)
        {
            var result = await http.PostAsJsonAsync("api/incomes", incomeFilter);
            await SetResults(result);
        }

        private async Task SetResults(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Income>>();
            AllIncomes = response;
        }

    }
}
