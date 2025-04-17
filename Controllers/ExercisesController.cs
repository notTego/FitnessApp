using FitnessTracker.DTOs;
using FitnessTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class ExercisesController : ControllerBase
	{
		private readonly IExerciseService _service;

		public ExercisesController(IExerciseService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] string? search = null) =>
			Ok(await _service.GetAllAsync(search));

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var exercise = await _service.GetByIdAsync(id);
			return exercise == null ? NotFound() : Ok(exercise);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create(ExerciseDTO dto)
		{
			var created = await _service.CreateAsync(dto);
			return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Update(int id, ExerciseDTO dto)
		{
			var updated = await _service.UpdateAsync(id, dto);
			return updated == null ? NotFound() : Ok(updated);
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int id)
		{
			var deleted = await _service.DeleteAsync(id);
			return deleted ? NoContent() : NotFound();
		}
	}
}
