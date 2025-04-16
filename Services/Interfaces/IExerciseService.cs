using FitnessTracker.DTOs;
using FitnessTracker.Entities;

public interface IExerciseService
{
	Task<List<Exercise>> GetAllAsync(string search = null);
	Task<Exercise> GetByIdAsync(int id);
	Task<Exercise> CreateAsync(ExerciseDTO dto);
	Task<Exercise> UpdateAsync(int id, ExerciseDTO dto);
	Task<bool> DeleteAsync(int id);
}