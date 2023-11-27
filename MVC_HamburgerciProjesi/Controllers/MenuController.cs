using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Services.MenuServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace HamburgerciProject.Presentation.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IActionResult> Index(string deger, int p = 1)
        {
            if (!string.IsNullOrEmpty(deger))
            {
                List<MenuDTO> menus = await _menuService.GetMenus();
                List<MenuDTO> seciliMenuler = menus.Where(x=>x.MenuAdi.ToLower().Contains(deger.ToLower())).ToList();
                
                return View(seciliMenuler.ToPagedList(p, 3));
            }
            else
            {
                List<MenuDTO> menuDTOs = await _menuService.GetMenus();
                return View(menuDTOs.ToPagedList(p, 3));

            }
            
        }


        [HttpPost]

        public async Task<IActionResult> Create(MenuDTO menuDTO)
        {

            await _menuService.Create(menuDTO);

            return RedirectToAction("Index", "Menu");
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            MenuDTO UpdateMenu = await _menuService.GetById(id);

            return View(UpdateMenu);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(MenuDTO model)
        {

            await _menuService.Update(model);

            return RedirectToAction("Index", "Menu");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _menuService.Delete(id);
            return RedirectToAction("Index", "Menu");

        }

    }
}
