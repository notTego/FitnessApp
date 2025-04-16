using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Entities
{
	public class Exercise
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		public ICollection<WorkoutExercise> WorkoutExercises { get; set; }
	}
}
