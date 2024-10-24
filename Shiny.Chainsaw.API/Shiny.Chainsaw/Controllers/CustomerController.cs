using Microsoft.AspNetCore.Mvc;

namespace Shiny.Chainsaw.Controllers
{
	public class CustomerController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
