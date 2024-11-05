using Microsoft.AspNetCore.Mvc;
using Shiny.Chainsaw.Model;
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
			var users = await _repository.Get();
			return Ok(users);		}

		[HttpGet]
		public async Task<IActionResult> Get(int id)
		{
			if (id == 0)
				return BadRequest();

			var user = await _repository.Get(id);
			return Ok(user);
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] User user)
		{
			if (user == null)
				return BadRequest();

			var id = await _repository.Add(user);
			return CreatedAtAction(nameof(Get), new { id }, user);
		}
	}
}
