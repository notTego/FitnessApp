using FitnessTracker.DTOs;
using FitnessTracker.Entities;

public interface IGoalService
{
	Task<List<Goal>> GetAllAsync(int userId);
	Task<Goal> GetByIdAsync(int id, int userId);
	Task<Goal> CreateAsync(GoalDTO dto, int userId);
	Task<Goal> UpdateAsync(int id, GoalDTO dto, int userId);
	Task<bool> DeleteAsync(int id, int userId);
}