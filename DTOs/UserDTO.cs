﻿namespace FitnessTracker.DTOs
{
	public class UserDTO
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string? Role { get; set; } // 👈 nou
	}
}
