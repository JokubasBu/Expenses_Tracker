using ExpensesTracker.Client.Pages;
using ExpensesTracker.Server.Data;
using ExpensesTracker.Server.Repositories.Interfaces;
using ExpensesTracker.Server.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using static ExpensesTracker.Shared.Extensions.Delegates;
using static System.Net.WebRequestMethods;

namespace ExpensesTracker.Server.Controllers
{
    [Route("api/[controller]")] // the route will be taken from GoalsController and it will read everything that is before controller
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly IGoal _goals;
        static int currentCount = 0; // amomunt of times the button Order was pressed

        private static int _year;
        private static int _month;
        private static int _categoryId;


        public GoalsController(IGoal _goals)
        {
            this._goals = _goals;
        }

        [HttpGet]
        public async Task<ActionResult<List<Goal>>> GetGoals()
        {
            return Ok(await GetFilteredGoals());
        }

        [HttpGet("currentCount")] // http methods should all be different, otherwise: The request matched multiple endpoints
        public async Task<ActionResult<List<Goal>>> GetOrderedGoals()
        {
            currentCount++;
            return Ok(await GetFilteredGoals());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Goal>> GetSingleGoal(int id)
        {
            var goal = await _goals.GetSingleGoalAsync(id);

            if (goal == null)
            {
                return NotFound("no entry...");
            }
            return Ok(goal.Value);
        }

        [HttpPost("Add")]
        public async Task<ActionResult<List<Goal>>> CreateGoal(Goal goal)
        {
            await _goals.CreateGoalAsync(goal);
            return Ok(await GetFilteredGoals());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Goal>>> DeleteGoal(int id)
        {
            var dbGoal = await _goals.GetSingleGoalAsync(id);
            if (dbGoal == null)
                return NotFound("There is no such goal :/");

            await _goals.DeleteGoalAsync(dbGoal.Value);

            return Ok(await GetFilteredGoals());
        }

        async Task<List<Goal>> GetFilteredGoals()
        {
            return await _goals.GetGoalsAsync();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Goal>>> UpdateGoal(Goal goal, int id)
        {
            var dbGoal = await _goals.GetSingleGoalAsync(id);
            if (dbGoal == null)
                return NotFound("Sorry, but no goal for you. :/");

            await _goals.UpdateGoalAsync(goal, dbGoal.Value);

            return Ok(await GetFilteredGoals());
        } 

    }
}