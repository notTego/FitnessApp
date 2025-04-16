using System;

namespace FitnessTracker.DTOs
{
	public class ProgressDTO
	{
		public DateTime Date { get; set; }
		public string Metric { get; set; }
		public double Value { get; set; }
	}
}
