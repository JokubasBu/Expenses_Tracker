using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ExpensesTracker.Client.Services.GoalService
{
    public class GoalService : IGoalService
    {
        private readonly HttpClient http;
        private readonly NavigationManager navigationManager;

        public List<Goal> AllGoals { get; set; } = new List<Goal>();
        public Goal goalFilter { get; set; } = new Goal();
        public Goal singleGoal { get; set; } = new Goal()
        {
            DueDate = DateTime.Now,
        };

        public GoalService(HttpClient http, NavigationManager navigationManager)
        {
            this.http = http;
            this.navigationManager = navigationManager;
        }

        public async Task CreateGoal(Goal goal)
        {
            
            var result = await http.PostAsJsonAsync("/api/goals/Add", goal);
            await SetResults(result);
            navigationManager.NavigateTo("/goals");
        }

        public async Task DeleteGoal(int id)
        {
            var result = await http.DeleteAsync($"api/goals/{id}");
            await SetResults(result);
        }

        public async Task GetGoals()
        {
            var result = await http.GetFromJsonAsync<List<Goal>>("api/goals");
            if (result != null)
            {
                AllGoals = result;
            }
        }

        public async Task<Goal> GetSingleGoal(int id)
        {
            var result = await http.GetFromJsonAsync<Goal>($"api/goals/{id}");
            if (result != null)
            {
                return result;
            }
            throw new Exception("not found whoops");
        }

        public async Task UpdateGoal(Goal goal)
        {
            var result = await http.PutAsJsonAsync($"api/goals/{goal.Id}", goal);
            await SetResults(result);
            navigationManager.NavigateTo("goals");
        }

        private async Task SetResults(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Goal>>();
            AllGoals = response;
        }
    }
}
