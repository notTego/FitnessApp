using FitnessTracker.DTOs;
using FitnessTracker.Entities;

namespace FitnessTracker.Services
{
	public interface IAuthService
	{
		Task<(string Token, string Error)> RegisterAsync(UserDTO dto);
		Task<(string Token, string Error)> LoginAsync(UserDTO dto);
	}
}
