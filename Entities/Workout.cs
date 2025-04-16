using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Entities
{
	public class Workout
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public DateTime Date { get; set; }

		public int UserId { get; set; }

		public User User { get; set; }

		public ICollection<WorkoutExercise> WorkoutExercises { get; set; }
	}
}
