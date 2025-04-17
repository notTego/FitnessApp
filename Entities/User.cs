namespace FitnessTracker.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; } = "";
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
		public string Role { get; set; } = "User";

		public ICollection<Workout> Workouts { get; set; }
		public ICollection<Progress> Progresses { get; set; }
		public ICollection<Goal> Goals { get; set; }
	}
}
