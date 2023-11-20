using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Services.MenuServices;
using Microsoft.AspNetCore.Mvc;

namespace HamburgerciProject.Presentation.Areas.User.Controllers
{
    [Area("User")]
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
    }
}
