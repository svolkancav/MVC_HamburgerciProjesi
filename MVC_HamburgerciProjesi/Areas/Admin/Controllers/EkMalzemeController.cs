using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Services.EkstraMalzemeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HamburgerciProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
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
            var updateMalzeme = _ekstraMalzemeService.GetById(id);
            return View(updateMalzeme);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EkstraMalzemeDTO model)
        {
            var ekstraMalzeme = await _ekstraMalzemeService.GetById(model.Id);
            await _ekstraMalzemeService.Update(ekstraMalzeme);
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete (int id)
        {
            await _ekstraMalzemeService.Delete(id);
            return RedirectToAction("Index");
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
            return RedirectToAction("Index");   
        }

    }
}
