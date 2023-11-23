using HamburgerciProject.Application.Services.SiparisServices;
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
        private readonly ISiparisService _siparisService;

        public SiparisController(ISiparisService siparisService)
        {
            _siparisService = siparisService;
        }

        public async Task<IActionResult> Index()
        {
            return View( await _siparisService.GetSiparisler());
        }








    }
}
