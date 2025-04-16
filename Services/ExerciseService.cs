using FitnessTracker.Data;
using FitnessTracker.DTOs;
using FitnessTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Services
{
	public class ExerciseService : IExerciseService
	{
		private readonly ApplicationDbContext _context;

		public ExerciseService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<Exercise>> GetAllAsync(string search = null)
		{
			var query = _context.Exercises.AsQueryable();
			if (!string.IsNullOrEmpty(search))
			{
				query = query.Where(e => e.Name.Contains(search));
			}

			return await query.ToListAsync();
		}

		public async Task<Exercise> GetByIdAsync(int id) =>
			await _context.Exercises.FindAsync(id);

		public async Task<Exercise> CreateAsync(ExerciseDTO dto)
		{
			var exercise = new Exercise { Name = dto.Name, Description = dto.Description };
			_context.Exercises.Add(exercise);
			await _context.SaveChangesAsync();
			return exercise;
		}

		public async Task<Exercise> UpdateAsync(int id, ExerciseDTO dto)
		{
			var exercise = await _context.Exercises.FindAsync(id);
			if (exercise == null) return null;

			exercise.Name = dto.Name;
			exercise.Description = dto.Description;
			await _context.SaveChangesAsync();
			return exercise;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var exercise = await _context.Exercises.FindAsync(id);
			if (exercise == null) return false;

			_context.Exercises.Remove(exercise);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
