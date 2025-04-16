using FitnessTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Workout> Workouts { get; set; }
		public DbSet<Exercise> Exercises { get; set; }
		public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
		public DbSet<Progress> Progresses { get; set; }
		public DbSet<Goal> Goals { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Many-to-Many between Workout and Exercise
			modelBuilder.Entity<WorkoutExercise>()
				.HasKey(we => new { we.WorkoutId, we.ExerciseId });

			modelBuilder.Entity<WorkoutExercise>()
				.HasOne(we => we.Workout)
				.WithMany(w => w.WorkoutExercises)
				.HasForeignKey(we => we.WorkoutId);

			modelBuilder.Entity<WorkoutExercise>()
				.HasOne(we => we.Exercise)
				.WithMany(e => e.WorkoutExercises)
				.HasForeignKey(we => we.ExerciseId);

			//User - Workout (1:N)
			modelBuilder.Entity<Workout>()
				.HasOne(w => w.User)
				.WithMany(u => u.Workouts)
				.HasForeignKey(w => w.UserId);

			//User - Progress (1:N)
			modelBuilder.Entity<Progress>()
				.HasOne(p => p.User)
				.WithMany(u => u.Progresses)
				.HasForeignKey(p => p.UserId);

			//User - Goal (1:N)
			modelBuilder.Entity<Goal>()
				.HasOne(g => g.User)
				.WithMany(u => u.Goals)
				.HasForeignKey(g => g.UserId);
		}
	}
}
