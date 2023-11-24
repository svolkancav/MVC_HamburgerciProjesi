using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Services.EkstraMalzemeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HamburgerciProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class EkMalzemeController : Controller
    {
        private readonly IEkstraMalzemeService _ekstraMalzemeService;

        public EkMalzemeController(IEkstraMalzemeService ekstraMalzemeService)
        {
            _ekstraMalzemeService = ekstraMalzemeService;
        }

        public async Task<IActionResult> Index()
        {
            List<EkstraMalzemeDTO> ekstraMalzemeDTOs = await _ekstraMalzemeService.GetEkstraMalzemeler();
            return View(ekstraMalzemeDTOs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit (int id)
        {
            var updateMalzeme = await _ekstraMalzemeService.GetById(id);
            return View(updateMalzeme);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EkstraMalzemeDTO model)
        {
            
            await _ekstraMalzemeService.Update(model);
            
            return RedirectToAction("Index","EkMalzeme");
        }

        public async Task<IActionResult> Delete (int id)
        {
            await _ekstraMalzemeService.Delete(id);
            return RedirectToAction("Index", "EkMalzeme");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EkstraMalzemeDTO model)
        {
            await _ekstraMalzemeService.Create(model);
            return RedirectToAction("Index", "EkMalzeme");   
        }

    }
}
