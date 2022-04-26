using Microsoft.AspNetCore.Mvc;
using Snipefish.WebClient.Models;
using System.Diagnostics;

namespace Snipefish.WebClient.Controllers
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
            return View();
        }

        public IActionResult CreateAdventure(UserViewModel user)
        {
            return View();
        }

        public IActionResult DoAdventure()
        {
            return View();
        }

        public IActionResult Adventure()
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