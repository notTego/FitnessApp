using System;
using System.Collections.Generic;

namespace FitnessTracker.DTOs
{
	public class WorkoutDTO
	{
		public string Name { get; set; }
		public DateTime Date { get; set; }
		public List<int> ExerciseIds { get; set; }
	}
}
