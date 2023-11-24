using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HamburgerciProject.Presentation.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    [AllowAnonymous]
    public class SiparisController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
    }
}
