using Microsoft.AspNetCore.Mvc;
using Shiny.Chainsaw.DTO;
using Shiny.Chainsaw.Repository;

namespace Shiny.Chainsaw.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class CheckinController : ControllerBase
	{
		private readonly ICheckinRepository _repository;
		public CheckinController(ICheckinRepository repository) 
		{
			_repository = repository;
		}

		[HttpGet]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var result = await _repository.GetByCustomer(id);
				return Ok(result);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Add(Checkin check)
		{
			try
			{
				int id = await _repository.Add(check);
				return CreatedAtAction(nameof(Get), new { id = check.Id }, check);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
