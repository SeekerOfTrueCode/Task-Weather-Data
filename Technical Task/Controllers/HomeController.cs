using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Technical_Task.Core.Logic;
using Technical_Task.Models;

namespace Technical_Task.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["AdminEmail"] = JsonConfigValuesProvider.Config["AdminSettings"]["UserEmail"];
            ViewData["AdminPassword"] = JsonConfigValuesProvider.Config["AdminSettings"]["UserPassword"];
            ViewData["StandardUserEmail"] = JsonConfigValuesProvider.Config["StandardUserSettings"]["UserEmail"];
            ViewData["StandardUserPassword"] = JsonConfigValuesProvider.Config["StandardUserSettings"]["UserPassword"];
            return View();
        }
        [HttpGet]
        [Authorize] //standard user
        public IActionResult BrowseWeatherData()
        {
            return View();
        }
        //[ValidateAntiForgeryToken]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult ManageWeatherData()
        {
            return View();
        }

        [HttpGet]
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
