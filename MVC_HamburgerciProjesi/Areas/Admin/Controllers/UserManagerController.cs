using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Services.AppUserService;
using HamburgerciProject.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HamburgerciProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class UserManagerController : Controller
    {
        private readonly IAppUserService _appUserService;

        public UserManagerController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<RegisterDTO> users = await _appUserService.GetUsers();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


       
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Edit(int id)
        {
            UpdateProfileDTO model = await _appUserService.GetById(id);

            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Edit(UpdateProfileDTO model)
        {

            await _appUserService.UpdateUser(model);

            return RedirectToAction("Index", "UserManager");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _appUserService.Delete(id);
            return RedirectToAction("Index", "UserManager");

        }

    }
}



//Düzennleme user get yeni ekrana gitsin-post listeleme
//Delete --> listeleme 
//İnsert --> get yeni ekran/ post-->listeleme


