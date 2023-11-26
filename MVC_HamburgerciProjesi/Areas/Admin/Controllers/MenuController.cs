using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Services.MenuServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HamburgerciProject.Presentation.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IActionResult> Index()
        {
			const string personelRolu = "UserRolu";
			List<MenuDTO> menuDTOs = await _menuService.GetMenus();
            return View(menuDTOs);
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

            return RedirectToAction("Index","Menu");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _menuService.Delete(id);
            return RedirectToAction("Index", "Menu");

        }

    }
}
