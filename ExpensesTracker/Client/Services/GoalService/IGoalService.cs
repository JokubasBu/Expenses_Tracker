namespace ExpensesTracker.Client.Services.GoalService
{
    public interface IGoalService
    {
		Goal singleGoal { get; set; }
		List<Goal> AllGoals { get; set; }
        Task GetGoals();
        Task<Goal> GetSingleGoal(int id);
        Task CreateGoal(Goal goal);
        Task UpdateGoal(Goal goal);
        Task DeleteGoal(int id);
    } 
}
