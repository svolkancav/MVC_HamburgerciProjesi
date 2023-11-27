using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Models.VMs;
using HamburgerciProject.Application.Services.SiparisServices;
using HamburgerciProject.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using X.PagedList;

namespace HamburgerciProject.Presentation.Controllers
{
    public class SiparisController : Controller
    {

        private readonly ISiparisService _siparisService;

        public SiparisController(ISiparisService siparisService)
        {
            _siparisService = siparisService;
        }
        public async Task<IActionResult> Index(string deger, int p = 3)
        {
            if (!string.IsNullOrEmpty(deger))
            {
                List<CreateSiparisDTO> siparis = await _siparisService.GetSiparisler();
                List<CreateSiparisDTO> seciliSiparisler = siparis.Where(x => x.İçerik.Contains(deger.ToLower())).ToList();

                return View(seciliSiparisler.ToPagedList(p, 3));
            }
            else
            {
                List<CreateSiparisDTO> siparisler = await _siparisService.GetSiparisler();
                return View(siparisler.ToPagedList(p, 3));

            }

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
