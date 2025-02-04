using Microsoft.AspNetCore.Mvc;
using Shiny.Chainsaw.Model;
using Shiny.Chainsaw.Repository;

namespace Shiny.Chainsaw.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerRepository _repository;

		public CustomerController(ICustomerRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				if (id == 0)
					return BadRequest();

				Customer customer = await _repository.Get(id);
				return Ok(customer);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost]
		public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
		{
			try
			{
				if (customer == null)
					return BadRequest();

				int id = await _repository.Add(customer);
				return CreatedAtAction(nameof(AddCustomer), new { id }, customer);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
