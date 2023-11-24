using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HamburgerciProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class EkMalzemeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
