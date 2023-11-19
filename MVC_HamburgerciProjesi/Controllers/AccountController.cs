﻿using HamburgerciProject.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;


namespace HamburgerciProject.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly SignInManager<AppUser> _signınmanager;
        private readonly IPasswordHasher<AppUser> _passwordhasher;

        public AccountController(UserManager<AppUser> usermanager, SignInManager<AppUser> signınmanager, IPasswordHasher<AppUser> passwordhasher)
        {
            _usermanager = usermanager;
            _signınmanager = signınmanager;
            _passwordhasher = passwordhasher;
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}