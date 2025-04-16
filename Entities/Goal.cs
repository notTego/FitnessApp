using System;
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Entities
{
	public class Goal
	{
		public int Id { get; set; }

		public string Description { get; set; }

		public string Metric { get; set; }

		public double TargetValue { get; set; }

		public bool IsAchieved { get; set; }

		public DateTime AchievedDate { get; set; }

		public int UserId { get; set; }

		public User User { get; set; }
	}
}
