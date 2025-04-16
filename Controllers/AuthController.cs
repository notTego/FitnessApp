using FitnessTracker.DTOs;
using FitnessTracker.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly IAuthService _authService;

	public AuthController(IAuthService authService)
	{
		_authService = authService;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register(UserDTO dto)
	{
		var (token, error) = await _authService.RegisterAsync(dto);
		if (error != null) return BadRequest(error);
		return Ok(new { token });
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login(UserDTO dto)
	{
		var (token, error) = await _authService.LoginAsync(dto);
		if (error != null) return Unauthorized(error);
		return Ok(new { token });
	}
}
