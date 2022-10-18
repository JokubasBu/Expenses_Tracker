﻿using ExpensesTracker.Client.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;


namespace ExpensesTracker.Client.Services.ExpensesService
{
    public class ExpensesService : IExpensesService
    {
        private readonly HttpClient http;
        private readonly NavigationManager navigationManager;

        public ExpensesService(HttpClient http, NavigationManager navigationManager)
        {
            this.http = http;
            this.navigationManager = navigationManager;
        }
        public List<Expense> AllExpenses { get; set; } = new List<Expense>(); 
        public List<Category> Categories { get; set; } = new List<Category>();

        public async Task CreateExpense(Expense expense)
        {
            if (expense.Comment.Replace(" ", "").Equals(String.Empty))
            {
                expense.Comment = "New Expense";
            }

            var result = await http.PostAsJsonAsync("/api/expenses/Add", expense);
            await SetResults(result);
        }

        public async Task DeleteExpense(int id)
        {
            var result = await http.DeleteAsync($"api/expenses/{id}");
            await SetResults(result);
        }

        public async Task GetCategories()
        {
            var result = await http.GetFromJsonAsync<List<Category>>("api/expenses/categories");
            if (result != null)
            {
                Categories = result;
            }
        }

        public async Task GetExpenses()
        {
            var result = await http.GetFromJsonAsync<List<Expense>>("api/expenses");
            if (result != null)
            {
                AllExpenses = result;
            }
        }

        public async Task GetOrderedExpenses()
        {
            var result = await http.GetFromJsonAsync<List<Expense>>("api/expenses/currentCount");
            if (result != null)
            {
                AllExpenses = result;
            }
        }
        public async Task ShowFilters(Expense expenseFilter)
        {
            var result = await http.PostAsJsonAsync("api/expenses", expenseFilter);
            await SetResults(result);
        }

        private async Task SetResults(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Expense>>();
            AllExpenses = response;
            // navigationManager.NavigateTo("monthlyexp");
        }

        public async Task<Expense> GetSingleExpense(int id)
        {
            var result = await http.GetFromJsonAsync<Expense>($"api/expenses/{id}");
            if (result != null)
            {
                return result;
            }
            throw new Exception("not found whoops");
        }

        public async Task UpdateExpense(Expense expense)
        {
            if (expense.Comment.Replace(" ", "").Equals(String.Empty))
            {
                expense.Comment = "Updated expense";
            }

            var result = await http.PutAsJsonAsync($"api/expenses/{expense.Id}", expense);
            await SetResults(result);
            navigationManager.NavigateTo("expenses");
        }
    }
}

