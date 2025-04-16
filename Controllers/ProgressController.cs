using FitnessTracker.DTOs;
using FitnessTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessTracker.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class ProgressController : ControllerBase
	{
		private readonly IProgressService _service;

		public ProgressController(IProgressService service)
		{
			_service = service;
		}

		private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

		[HttpGet]
		public async Task<IActionResult> GetAll() =>
			Ok(await _service.GetAllAsync(GetUserId()));

		[HttpPost]
		public async Task<IActionResult> Add(ProgressDTO dto)
		{
			var added = await _service.AddAsync(dto, GetUserId());
			return Ok(added);
		}
	}
}
