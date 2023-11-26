using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Models.VMs;
using HamburgerciProject.Application.Services.SiparisServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;

namespace HamburgerciProject.Presentation.Areas.User.Controllers
{
    [Area("User")]
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
            return View(await _siparisService.GetSiparisler());
        }

        [HttpPost]
        public async Task<IActionResult> Create(List<MenuVM> menuler, List<EkstraMalzemeVM> ekMalzemeler)
        {
            CreateSiparisDTO siparis = await _siparisService.SiparisOlustur(menuler, ekMalzemeler);

            await _siparisService.Create(siparis);

            return RedirectToAction("Index");


        }


    }
}
