﻿using Microsoft.AspNetCore.Mvc;
using Shiny.Chainsaw.Model;
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
			var result = await _repository.GetByCustomer(id);
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Add(Checkin check)
		{
			int id = await _repository.Add(check);
			return CreatedAtAction(nameof(Get), new { id = check.Id }, check);
		}
	}
}
