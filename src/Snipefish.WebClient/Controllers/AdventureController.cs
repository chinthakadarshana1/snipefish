using Microsoft.AspNetCore.Mvc;
using Snipefish.Application.Commands.UserAdventures;
using Snipefish.Application.Responses;
using Snipefish.Domain.Entities;
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

        public async Task<IActionResult> MyAdventures()
        {
            var loggedInUser = HttpContext.Session.Get<UserAdventuresResponse>(SnipefishWebConfiguration.UserSessionKey);

            if (loggedInUser != null)
            {
                var userAdventures = await _snipefishWebApi.GetAdventuresByUserId(loggedInUser.UserId, default);
                return View(userAdventures);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> StartAdventure(Adventure adventure)
        {
            var loggedInUser = HttpContext.Session.Get<UserAdventuresResponse>(SnipefishWebConfiguration.UserSessionKey);

            if (loggedInUser != null)
            {
                var userAdventures = await _snipefishWebApi.GetAdventuresByUserId(loggedInUser.UserId, default);

                if (userAdventures?.Adventures != null)
                {
                    var selectedAdventure =
                        userAdventures.Adventures.FirstOrDefault(x => x.Name.ToLower() == adventure.Name.ToLower());

                    if (selectedAdventure != null)
                    {
                        return View(selectedAdventure);
                    }
                    return RedirectToAction("MyAdventures", "Adventure");
                }
                return RedirectToAction("MyAdventures", "Adventure");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
