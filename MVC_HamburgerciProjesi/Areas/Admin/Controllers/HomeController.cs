using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_HamburgerciProjesi.Models;
using System.Data;
using System.Diagnostics;

namespace HamburgerciProject.Presentation.Areas.Admin.Controllers
{
    //[Area("Admin")]
    //[Authorize(Roles = "Admin")]
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