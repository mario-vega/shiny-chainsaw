using Microsoft.AspNetCore.Mvc;
using Shiny.Chainsaw.Model;
using Shiny.Chainsaw.Repository;
using Shiny.Chainsaw.Repository.DbContext;

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
		public async Task<IEnumerable<User>> GetUsers()
		{
			var users = await _repository.Get();
			return users;
		}
	}
}
