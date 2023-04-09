using ExpensesTracker.Server.Data;
using ExpensesTracker.Server.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Server.Repositories
{
    public class GoalRepo : IGoal
    {
        private readonly DataContext context;

        public GoalRepo(DataContext context) 
        {
        this.context = context;
        }

        public async Task<ActionResult<Goal>> CreateGoalAsync(Goal goal)
        {
            context.AllGoals.Add(goal);
            await context.SaveChangesAsync();
            return goal;
        }

        public async Task<ActionResult<Goal>> DeleteGoalAsync(Goal goal)
        {
            context.AllGoals.Remove(goal);
            await context.SaveChangesAsync();

            return goal;
        }

        public async Task<List<Goal>> GetGoalsAsync()
        {
            return await context.AllGoals.ToListAsync();
        }

        public async Task<ActionResult<Goal?>> GetSingleGoalAsync(int id)
        {
            return await context.AllGoals.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Goal> UpdateGoalAsync(Goal goal, Goal dbGoal)
        {
            dbGoal.Title = goal.Title;
            dbGoal.DueDate = goal.DueDate;
            dbGoal.Description = goal.Description;
           
            await context.SaveChangesAsync();

            return goal;
        }
    }
}
