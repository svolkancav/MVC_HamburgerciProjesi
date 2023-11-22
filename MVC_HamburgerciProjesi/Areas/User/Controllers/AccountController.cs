using HamburgerciProject.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Services.AppUserService;
using MimeKit;
using MailKit.Net.Smtp;

namespace HamburgerciProject.Presentation.Area.Admin.Controllers
{
    [Area("User")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInmanager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly IAppUserService _iAppUser;


        public AccountController(UserManager<AppUser> usermanager, SignInManager<AppUser> signınmanager, IPasswordHasher<AppUser> passwordhasher, IAppUserService iAppUser)
        {
            _userManager = usermanager;
            _signInmanager = signınmanager;
            _passwordHasher = passwordhasher;
            _iAppUser = iAppUser;
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

            AppUser user = new AppUser()
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                CreateDate = registerDTO.CreateDate,
                Password = registerDTO.Password,
                //ConfirmCode = registerDTO.Code,
                Status = Domain.Enum.Status.Inactive
            };

            if (ModelState.IsValid)
            {
                IdentityResult result = await _iAppUser.Register(registerDTO);
                if (result.Succeeded == true)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action(nameof(Confirmation), "Account", new { token, email = user.Email }, Request.Scheme);
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
                    return View(registerDTO);
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
            return View();
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

    }


}