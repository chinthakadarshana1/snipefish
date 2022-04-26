using Microsoft.AspNetCore.Mvc;
using Snipefish.WebClient.Models;
using System.Diagnostics;
using Snipefish.Application.Commands.UserAdventures;
using Snipefish.Application.Responses;
using Snipefish.WebClient.Configurations;
using Snipefish.WebClient.Helpers;

namespace Snipefish.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SnipefishWebApi _snipefishWebApi;

        public HomeController(ILogger<HomeController> logger, SnipefishWebApi snipefishWebApi)
        {
            _logger = logger;
            _snipefishWebApi = snipefishWebApi;
        }

        public IActionResult Index()
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