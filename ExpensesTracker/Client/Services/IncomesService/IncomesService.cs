using ExpensesTracker.Client.Pages;
using ExpensesTracker.Shared.Models;
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

        public async Task CreateOrUpdateIncome(Income income)
        {
            var result = await http.PostAsJsonAsync("/api/incomes/Add", income);
            await SetResults(result);
            navigationManager.NavigateTo("incomes");
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

        public async Task<Income> GetSingleIncome(string date)
        {
            var result = await http.GetFromJsonAsync<Income>($"api/incomes/{date}");
            if (result != null)
            {
                return result;
            }
            throw new Exception("not found whoops");
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
