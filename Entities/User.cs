using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Entities
{
	public class User
	{
		public int Id { get; set; }

		[Required]
		public string Username { get; set; }

		[Required]
		public byte[] PasswordHash { get; set; }

		[Required]
		public byte[] PasswordSalt { get; set; }

		public string Role { get; set; } = "User";

		public ICollection<Workout> Workouts { get; set; }

		public ICollection<Progress> Progresses { get; set; }

		public ICollection<Goal> Goals { get; set; }
	}
}
