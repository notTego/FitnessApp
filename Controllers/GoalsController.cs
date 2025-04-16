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
	public class GoalsController : ControllerBase
	{
		private readonly IGoalService _service;

		public GoalsController(IGoalService service)
		{
			_service = service;
		}

		private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

		[HttpGet]
		public async Task<IActionResult> GetAll() =>
			Ok(await _service.GetAllAsync(GetUserId()));

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var goal = await _service.GetByIdAsync(id, GetUserId());
			return goal == null ? NotFound() : Ok(goal);
		}

		[HttpPost]
		public async Task<IActionResult> Create(GoalDTO dto)
		{
			var created = await _service.CreateAsync(dto, GetUserId());
			return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, GoalDTO dto)
		{
			var updated = await _service.UpdateAsync(id, dto, GetUserId());
			return updated == null ? NotFound() : Ok(updated);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var deleted = await _service.DeleteAsync(id, GetUserId());
			return deleted ? NoContent() : NotFound();
		}
	}
}
