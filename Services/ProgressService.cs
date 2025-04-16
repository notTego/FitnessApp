using FitnessTracker.Data;
using FitnessTracker.DTOs;
using FitnessTracker.Entities;
using FitnessTracker.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Services
{
	public class ProgressService : IProgressService
	{
		private readonly ApplicationDbContext _context;
		private readonly EmailService _emailService;

		public ProgressService(ApplicationDbContext context, EmailService emailService)
		{
			_context = context;
			_emailService = emailService;
		}

		public async Task<List<Progress>> GetAllAsync(int userId) =>
			await _context.Progresses.Where(p => p.UserId == userId).ToListAsync();

		public async Task<Progress> AddAsync(ProgressDTO dto, int userId)
		{
			var progress = new Progress
			{
				Date = dto.Date,
				Metric = dto.Metric,
				Value = dto.Value,
				UserId = userId
			};

			_context.Progresses.Add(progress);
			await _context.SaveChangesAsync();

			var goal = await _context.Goals
				.FirstOrDefaultAsync(g => g.Metric == dto.Metric && g.UserId == userId && !g.IsAchieved && dto.Value >= g.TargetValue);

			if (goal != null)
			{
				goal.IsAchieved = true;
				goal.AchievedDate = DateTime.UtcNow;
				await _context.SaveChangesAsync();

				var user = await _context.Users.FindAsync(userId);
				await _emailService.SendAsync(user.Username, $"🎯 Goal Achieved: {goal.Description}", $"Felicitări! Ai atins obiectivul '{goal.Description}' cu {dto.Value} {dto.Metric}.");
			}

			return progress;
		}
	}
}
