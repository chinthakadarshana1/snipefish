using Microsoft.AspNetCore.Mvc;
using Snipefish.Application.Commands.UserAdventures;
using Snipefish.Application.Responses;
using Snipefish.WebClient.Configurations;
using Snipefish.WebClient.Helpers;
using Snipefish.WebClient.Models;

namespace Snipefish.WebClient.Controllers
{
    public class AdventureController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SnipefishWebApi _snipefishWebApi;

        public AdventureController(ILogger<HomeController> logger, SnipefishWebApi snipefishWebApi)
        {
            _logger = logger;
            _snipefishWebApi = snipefishWebApi;
        }

        public async Task<IActionResult> Index(UserViewModel? user = null)
        {
            if (!string.IsNullOrEmpty(user?.UserName))
            {
                LoginUserCommand loginUserCommand = new LoginUserCommand { UserName = user.UserName ?? throw new InvalidOperationException("Invalid User Name") };
                var loggedInUser = await _snipefishWebApi.UserLogin(loginUserCommand, default);
                HttpContext.Session.Set<UserAdventuresResponse>(SnipefishWebConfiguration.UserSessionKey, loggedInUser!);
            }
            
            return View();
        }
    }
}
