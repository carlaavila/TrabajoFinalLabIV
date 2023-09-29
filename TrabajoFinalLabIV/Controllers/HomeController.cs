using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TrabajoFinalLabIV.Data;
using TrabajoFinalLabIV.Models;
using TrabajoFinalLabIV.ViewModel;

namespace TrabajoFinalLabIV.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _context;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			var viewModel = new ClubesViewModel
			{
				Clubes = _context.Clubes.OrderBy(club => club.FechaNacimiento).ToList()
			};

			return View(viewModel);
		}


		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Clubes == null)
			{
				return NotFound();
			}

			var club = await _context.Clubes
				.Include(c => c.Categoria)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (club == null)
			{
				return NotFound();
			}

			return View(club);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}