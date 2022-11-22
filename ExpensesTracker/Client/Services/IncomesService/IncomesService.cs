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

        public List<Income> AllIncomes { get; set; } = new List<Income>();
        public Statistic Statistics { get; set; } = new Statistic();
        public Income incomeFilter {get; set; } = new Income();
        public Income singleIncome { get; set; } = new Income();

        public IncomesService(HttpClient http, NavigationManager navigationManager)
        {
            this.http = http;
            this.navigationManager = navigationManager;
        }
        
        public async Task CreateIncome(Income income)
        {
            var result = await http.PostAsJsonAsync("/api/incomes/Add", income);
            await SetResults(result);
            navigationManager.NavigateTo("/incomes");
        }

        public async Task DeleteIncome(int id)
        {
            var result = await http.DeleteAsync($"api/incomes/{id}");
            await SetResults(result);
        }

        public async Task GetIncomes()
        {
            var result = await http.GetFromJsonAsync<List<Income>>("api/incomes");
            if (result != null)
            {
                AllIncomes = result;
            }
        }

        public async Task<Income> GetSingleIncome(int id)
        {
            var result = await http.GetFromJsonAsync<Income>($"api/incomes/{id}");
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

        public async Task UpdateIncome(Income income)
        {
            var result = await http.PutAsJsonAsync($"api/incomes/{income.Id}", income);
            await SetResults(result);
            navigationManager.NavigateTo("incomes");
        }

        private async Task SetResults(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Income>>();
            AllIncomes = response;
        }

        public async Task GetStatistics()
        {
            var result = await http.GetFromJsonAsync<Statistic>("api/incomes/statistics");
            if (result != null)
            {
                Statistics = result;
            }
        }

    }
}
