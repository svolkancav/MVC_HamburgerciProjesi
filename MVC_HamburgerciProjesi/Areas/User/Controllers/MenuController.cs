using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Services.MenuServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HamburgerciProject.Presentation.Areas.User.Controllers
{
    //[Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<MenuDTO> menuDTOs = await _menuService.GetMenus();
            return View(menuDTOs);
        }
        [HttpPost]

        public async Task<IActionResult> Edit(MenuDTO menuDTO)
        {
            var menu = _menuService.GetById(menuDTO.Id);
            await _menuService.Update(menuDTO);

            return RedirectToAction("Index");
        }
        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var UpdateMenu = _menuService.GetById(id);

            return View(UpdateMenu);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _menuService.Delete(id);
            return RedirectToAction("Index");

        }

    }
}
