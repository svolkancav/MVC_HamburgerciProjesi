using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Services.AppUserService;
using HamburgerciProject.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace HamburgerciProject.Presentation.Controllers
{
    public class UserManagerController : Controller
    {
        private readonly IAppUserService _appUserService;

        public UserManagerController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        public async Task<IActionResult> Index(string deger,int p =1)
        {

            if (!string.IsNullOrEmpty(deger))
            {
                List<RegisterDTO> users = await _appUserService.GetUsers();
                List<RegisterDTO> seciliUsers = users.Where(x => x.UserName.ToLower().Contains(deger.ToLower())).ToList();

                var pagedUsers = seciliUsers.ToPagedList(p, 3);
                return View(pagedUsers);
            }
            else
            {
                List<RegisterDTO> users = await _appUserService.GetUsers();
                var pagedUser = users.ToPagedList(p, 3);

                return View(pagedUser);

            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            UpdateProfileDTO model = await _appUserService.GetById(id);

            return View(model);
        }


        [HttpPost]
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


