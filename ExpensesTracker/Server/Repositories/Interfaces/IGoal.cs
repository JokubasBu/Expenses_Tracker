using ExpensesTracker.Server.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ExpensesTracker.Server.Repositories.Interfaces
{
    public interface IGoal
    {
        Task<List<Goal>> GetGoalsAsync();
        Task<ActionResult<Goal?>> GetSingleGoalAsync(int id);
        Task<ActionResult<Goal>> CreateGoalAsync(Goal Goal);
        Task<ActionResult<Goal>> DeleteGoalAsync(Goal Goal);
        Task<Goal> UpdateGoalAsync(Goal Goal, Goal dbGoal);
    }
}
