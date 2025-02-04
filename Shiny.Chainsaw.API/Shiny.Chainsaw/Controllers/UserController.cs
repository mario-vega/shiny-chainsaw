using Microsoft.AspNetCore.Mvc;
using Shiny.Chainsaw.DTO;
using Shiny.Chainsaw.Repository;

namespace Shiny.Chainsaw.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
			_repository = repository;
        }

        [HttpGet]
		[Route("GetUsers")]
		public async Task<IActionResult> GetUsers()
		{
			try
			{
				var users = await _repository.Get();
				return Ok(users);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				if (id == 0)
					return BadRequest();

				var user = await _repository.Get(id);
				return Ok(user);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] User user)
		{
			try
			{
				if (user == null)
					return BadRequest();

				var id = await _repository.Add(user);
				return CreatedAtAction(nameof(Get), new { id }, user);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPatch]
		public async Task<IActionResult> Update([FromBody]User user)
		{
			try
			{
				if (user == null)
					return BadRequest();

				await _repository.Update(user);

				return NoContent();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
