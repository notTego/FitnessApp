using FitnessTracker.Data;
using FitnessTracker.DTOs;
using FitnessTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Services
{
	public class WorkoutService : IWorkoutService
	{
		private readonly ApplicationDbContext _context;

		public WorkoutService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<Workout>> GetAllAsync(int userId)
		{
			return await _context.Workouts
				.Include(w => w.WorkoutExercises)
				.ThenInclude(we => we.Exercise)
				.Where(w => w.UserId == userId)
				.ToListAsync();
		}

		public async Task<Workout> GetByIdAsync(int id, int userId)
		{
			return await _context.Workouts
				.Include(w => w.WorkoutExercises)
				.ThenInclude(we => we.Exercise)
				.FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);
		}

		public async Task<Workout> CreateAsync(WorkoutDTO dto, int userId)
		{
			var workout = new Workout
			{
				Name = dto.Name,
				Date = dto.Date,
				UserId = userId,
				WorkoutExercises = dto.ExerciseIds.Select(eid => new WorkoutExercise
				{
					ExerciseId = eid
				}).ToList()
			};

			_context.Workouts.Add(workout);
			await _context.SaveChangesAsync();
			return workout;
		}

		public async Task<Workout> UpdateAsync(int id, WorkoutDTO dto, int userId)
		{
			var workout = await GetByIdAsync(id, userId);
			if (workout == null) return null;

			workout.Name = dto.Name;
			workout.Date = dto.Date;

			workout.WorkoutExercises.Clear();
			foreach (var eid in dto.ExerciseIds)
			{
				workout.WorkoutExercises.Add(new WorkoutExercise { ExerciseId = eid });
			}

			await _context.SaveChangesAsync();
			return workout;
		}

		public async Task<bool> DeleteAsync(int id, int userId)
		{
			var workout = await GetByIdAsync(id, userId);
			if (workout == null) return false;

			_context.Workouts.Remove(workout);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
