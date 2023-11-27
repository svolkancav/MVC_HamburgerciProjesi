using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Services.EkstraMalzemeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using X.PagedList;

namespace HamburgerciProject.Presentation.Controllers
{
    public class EkMalzemeController : Controller
    {
        private readonly IEkstraMalzemeService _ekstraMalzemeService;

        public EkMalzemeController(IEkstraMalzemeService ekstraMalzemeService)
        {
            _ekstraMalzemeService = ekstraMalzemeService;
        }

        public async Task<IActionResult> Index(string deger, int p = 1)
        {


            if (!string.IsNullOrEmpty(deger))
            {
                List<EkstraMalzemeDTO> ekstraMalzeme = await _ekstraMalzemeService.GetEkstraMalzemeler();
                List<EkstraMalzemeDTO> seciliekstraMalzeme = ekstraMalzeme.Where(x => x.EkstraAdi.ToLower().Contains(deger.ToLower())).ToList();

                return View(seciliekstraMalzeme.ToPagedList(p,3));
            }
            else
            {
                List<EkstraMalzemeDTO> ekstraMalzemeDTOs = await _ekstraMalzemeService.GetEkstraMalzemeler();
                return View(ekstraMalzemeDTOs.ToPagedList(p,3));

            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var updateMalzeme = await _ekstraMalzemeService.GetById(id);
            return View(updateMalzeme);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EkstraMalzemeDTO model)
        {

            await _ekstraMalzemeService.Update(model);

            return RedirectToAction("Index", "EkMalzeme");
        }

        public async Task<IActionResult> Delete(int id)
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
