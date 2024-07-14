using Microsoft.AspNetCore.Mvc;
using Movies.Models;

using System.Diagnostics;

namespace Movie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Movies.Models.Movie> list = Movies.Models.Movie.getAll();
            return View(list);
        }

        public IActionResult Login()
        {
            // Logic to render login view
            return View("/Admin/Login.cshtml");
        }

        public IActionResult sigup()
        {
            // Logic to render login view
            return View();
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
