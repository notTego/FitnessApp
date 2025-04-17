using System.Security.Cryptography;
using System.Text;
using System.Linq;
using FitnessTracker.Data;
using FitnessTracker.DTOs;
using FitnessTracker.Entities;
using FitnessTracker.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Services
{
	public class AuthService : IAuthService
	{
		private readonly ApplicationDbContext _context;
		private readonly TokenService _tokenService;

		public AuthService(ApplicationDbContext context, TokenService tokenService)
		{
			_context = context;
			_tokenService = tokenService;
		}

		public async Task<(string? Token, string? Error)> RegisterAsync(UserDTO dto)
		{
			if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
				return (null, "Username already exists");

			PasswordHelper.CreatePasswordHash(dto.Password, out var hash, out var salt);

			var user = new User
			{
				Username = dto.Username,
				PasswordHash = hash,
				PasswordSalt = salt,
				Role = dto.Role ?? "User"
			};

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			var token = _tokenService.CreateToken(user);
			return (token, null);
		}


		public async Task<(string Token, string Error)> LoginAsync(UserDTO dto)
		{
			var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == dto.Username.ToLower());
			if (user == null) return (null, "Invalid username");

			using var hmac = new HMACSHA512(user.PasswordSalt);
			var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

			if (!computedHash.SequenceEqual(user.PasswordHash))
				return (null, "Invalid password");

			var token = _tokenService.CreateToken(user);
			return (token, null);
		}
	}
}
