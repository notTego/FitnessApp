using System;
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Entities
{
	public class Progress
	{
		public int Id { get; set; }

		public DateTime Date { get; set; }

		public string Metric { get; set; }

		public double Value { get; set; }

		public int UserId { get; set; }

		public User User { get; set; }
	}
}
