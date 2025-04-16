using FitnessTracker.DTOs;
using FitnessTracker.Entities;

public interface IWorkoutService
{
	Task<List<Workout>> GetAllAsync(int userId);
	Task<Workout> GetByIdAsync(int id, int userId);
	Task<Workout> CreateAsync(WorkoutDTO dto, int userId);
	Task<Workout> UpdateAsync(int id, WorkoutDTO dto, int userId);
	Task<bool> DeleteAsync(int id, int userId);
}
