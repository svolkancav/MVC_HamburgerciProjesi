using HamburgerciProject.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HamburgerciProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(_userManager.Users);
        }


        //Düzennleme user get yeni ekrana gitsin-post listeleme
        //Delete --> listeleme 
        //İnsert --> get yeni ekran/ post-->listeleme

     }
}
