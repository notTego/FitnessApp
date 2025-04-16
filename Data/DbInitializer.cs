using FitnessTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Data
{
	public static class DbInitializer
	{
		public static void Initialize(ApplicationDbContext context)
		{
			context.Database.Migrate();

			// Seed default exercises
			if (!context.Exercises.Any())
			{
				var exercises = new List<Exercise>
				{
					new Exercise { Name = "Push-up", Description = "Upper body bodyweight exercise" },
					new Exercise { Name = "Squat", Description = "Lower body strength exercise" },
					new Exercise { Name = "Bench Press", Description = "Chest strength with weights" },
					new Exercise { Name = "Deadlift", Description = "Full-body barbell movement" },
					new Exercise { Name = "Plank", Description = "Core stabilization" }
				};

				context.Exercises.AddRange(exercises);
			}

			// Seed default admin user
			if (!context.Users.Any(u => u.Username == "admin"))
			{
				using var hmac = new System.Security.Cryptography.HMACSHA512();
				var admin = new User
				{
					Username = "admin",
					Role = "Admin",
					PasswordSalt = hmac.Key,
					PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("admin123"))
				};

				context.Users.Add(admin);
			}

			context.SaveChanges();
		}
	}
}
