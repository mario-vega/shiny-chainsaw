﻿using Microsoft.AspNetCore.Mvc;

namespace Shiny.Chainsaw.Controllers
{
	public class UserController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
