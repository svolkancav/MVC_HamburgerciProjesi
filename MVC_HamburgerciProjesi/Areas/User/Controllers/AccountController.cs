using HamburgerciProject.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Services.AppUserService;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace HamburgerciProject.Presentation.Areas.User.Controllers
{
    [Area("User")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInmanager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly IAppUserService _iAppUser;
        private readonly ILogger<AccountController> _iLogger;
       

        public AccountController(UserManager<AppUser> usermanager, SignInManager<AppUser> signınmanager, IPasswordHasher<AppUser> passwordhasher, IAppUserService iAppUser, ILogger<AccountController> iLogger)
        {
            _userManager = usermanager;
            _signInmanager = signınmanager;
            _passwordHasher = passwordhasher;
            _iAppUser = iAppUser;
            _iLogger = iLogger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        
        {
            return View();
        }
        AppUser appUser;

        [HttpPost]
        [ValidateAntiForgeryToken]
     
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {

            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                   
                    UserName = registerDTO.UserName,
                    Email = registerDTO.Email,
                    CreateDate = registerDTO.CreateDate,
                    Password = registerDTO.Password,
                    //ConfirmCode = registerDTO.Code,
                    Status = Domain.Enum.Status.Inactive
                };
                IdentityResult result = await _userManager.CreateAsync(user, user.Password);
                //IdentityResult result = await _iAppUser.Register(registerDTO);
                if (result.Succeeded == true)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("Confirmation", "Account", new { id = user.Id, token = token }, Request.Scheme);

                    MimeMessage mimeMessage = new MimeMessage();
                    MailboxAddress mailboxFrom = new MailboxAddress("Admin", "hamburgercinizboost@gmail.com");
                    MailboxAddress mailboxTo = new MailboxAddress("User", user.Email);

                    mimeMessage.From.Add(mailboxFrom);
                    mimeMessage.To.Add(mailboxTo);

                    var bodybuilder = new BodyBuilder();
                    bodybuilder.TextBody = "Kayıt işlemini gerçekleştirmek için linke tıklayınız: " + confirmationLink;
                    mimeMessage.Body = bodybuilder.ToMessageBody();

                    mimeMessage.Subject = "Onay linkiniz";


                    SmtpClient smtpClient = new SmtpClient();

                    smtpClient.Connect("smtp.gmail.com", 587, false);
                    smtpClient.Authenticate("hamburgercinizboost@gmail.com", "owoz rpqs bgnd qbtz");
                    smtpClient.Send(mimeMessage);
                    smtpClient.Disconnect(true);


                    ViewBag.ErrorTitle = "Registration successful";
                    ViewBag.ErrorMessage = "Before you can Login, please confirm your " +
                            "email, by clicking on the confirmation link we have emailed you";
                    return View();
                  
                }

                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);

                    }
                    return View();
                }

            }
            return RedirectToAction("Confirmation");
          
        }

        [HttpGet]
        public async Task<IActionResult> Confirmation(int id, string token)
        {
            

            if (id == null || token == null)
            {
                return RedirectToAction("index", "home");
            }
          

            var user = await _userManager.FindByIdAsync(id.ToString());
            UpdateProfileDTO update = new UpdateProfileDTO()
            {
                Id = user.Id,
                status = user.Status,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.Password

            };
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {id} is invalid";
                return View("NotFound");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
               update.status = Domain.Enum.Status.Active;
               await   _iAppUser.UpdateUser(update);
                return RedirectToAction("Login");
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }

        public IActionResult Login()
            {
            //returnUrl = returnUrl is null ? "Index" : returnUrl;
            //return View(new LoginDTO() { ReturnUrl = returnUrl });
        return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByNameAsync(model.UserName);
                if (appUser.Status == Domain.Enum.Status.Active)
                {
                    SignInResult result = await _signInmanager.PasswordSignInAsync(appUser.UserName, model.Password, false, false);
                        if (result.Succeeded)
                            return RedirectToAction("Index", "Siparis");

                }

            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInmanager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }


}