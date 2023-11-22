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
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO registerDTO, string returnUrl)
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
                    _iLogger.Log(LogLevel.Warning, confirmationLink);

                    //if(_signInmanager.IsSignedIn(User) && User.IsInRole("Admin"))
                    //{
                    //    return RedirectToAction("Confirmation", "Account");
                    //}


                    //var message = new Message(new string[] { user.Email }, "Confirmation email link", confirmationLink, null);
                    //await _emailSender.SendEmailAsync(message);



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
                    return View("Error");
                    //return View("Error");
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
            return View(registerDTO);
            //await _signInmanager.SignInAsync(user, isPersistent: false);

            //return View(registerDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Confirmation(string userId, string token)
        {


            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }

        public IActionResult Login(string returnUrl)
        {
            returnUrl = returnUrl is null ? "Index" : returnUrl;
            return View(new LoginDTO() { ReturnUrl = returnUrl });
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByNameAsync(model.UserName);
                if (appUser != null)
                {
                    SignInResult result = await _signInmanager.PasswordSignInAsync(appUser.UserName, model.Password, false, false);
                    if (result.Succeeded)
                        return RedirectToAction("Edit");
                    ModelState.AddModelError("", "Wrong credantion information");
                }

            }
            return View("Menu", "Index");
        }

    }


}