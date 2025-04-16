using FitnessTracker.Data;
using FitnessTracker.DTOs;
using FitnessTracker.Entities;
using FitnessTracker.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Services
{
	public class GoalService : IGoalService
	{
		private readonly ApplicationDbContext _context;
		private readonly EmailService _emailService;

		public GoalService(ApplicationDbContext context, EmailService emailService)
		{
			_context = context;
			_emailService = emailService;
		}

		public async Task<List<Goal>> GetAllAsync(int userId) =>
			await _context.Goals.Where(g => g.UserId == userId).ToListAsync();

		public async Task<Goal> GetByIdAsync(int id, int userId) =>
			await _context.Goals.FirstOrDefaultAsync(g => g.Id == id && g.UserId == userId);

		public async Task<Goal> CreateAsync(GoalDTO dto, int userId)
		{
			var goal = new Goal
			{
				Description = dto.Description,
				Metric = dto.Metric,
				TargetValue = dto.TargetValue,
				UserId = userId
			};

			_context.Goals.Add(goal);
			await _context.SaveChangesAsync();
			return goal;
		}

		public async Task<Goal> UpdateAsync(int id, GoalDTO dto, int userId)
		{
			var goal = await GetByIdAsync(id, userId);
			if (goal == null) return null;

			goal.Description = dto.Description;
			goal.Metric = dto.Metric;
			goal.TargetValue = dto.TargetValue;

			await _context.SaveChangesAsync();
			return goal;
		}

		public async Task<bool> DeleteAsync(int id, int userId)
		{
			var goal = await GetByIdAsync(id, userId);
			if (goal == null) return false;

			_context.Goals.Remove(goal);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
