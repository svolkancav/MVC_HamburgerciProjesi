using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Services.MenuServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HamburgerciProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        
        public async Task<IActionResult> Index()
        {
            List<MenuDTO> menuDTOs = await _menuService.GetMenus();
            return View(menuDTOs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var UpdateMenu = _menuService.GetById(id);

            return View(UpdateMenu);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MenuDTO menuDTO)
        {
            var menu = await _menuService.GetById(menuDTO.Id);
            await _menuService.Update(menu);

            return RedirectToAction("Index");
        }

        

        public async Task<IActionResult> Delete(int id)
        {
            await _menuService.Delete(id);
            return RedirectToAction("Index");

        }

    }
}
