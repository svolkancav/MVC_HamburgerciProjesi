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
using Microsoft.AspNetCore.Http;
using X.PagedList;

namespace HamburgerciProject.Presentation.Controllers
{
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

        public async Task<IActionResult> Index(string deger, int p = 3)
        {
            if (!string.IsNullOrEmpty(deger))
            {
                List<RegisterDTO> users = await _iAppUser.GetUsers();
                List<RegisterDTO> seciliUsers = users.Where(x => x.UserName.ToLower().Contains(deger.ToLower())).ToList();

                return View(seciliUsers.ToPagedList(p, 3));
            }
            else
            {
                List<RegisterDTO> users = await _iAppUser.GetUsers();
                return View(users.ToPagedList(p, 3));

            }

        }
        [HttpGet]
        public IActionResult Register()

        {
            return View();
        }


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
                    var confirmationLink = Url.Action("Confirmation", "Account", new { id = user.Id, token }, Request.Scheme);

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
                    return RedirectToAction("Login", "Account");

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
                UserName = user.UserName

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
                await _iAppUser.UpdateUser(update);
                return RedirectToAction("Login");
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }
        [HttpGet]
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
                if (appUser is not null)
                {
                    if (appUser.Status == Domain.Enum.Status.Active)
                    {
                        SignInResult result = await _signInmanager.PasswordSignInAsync(appUser.UserName, model.Password, false, false);
                        if (result.Succeeded)
                        {
                            //HttpContext.Response.Cookies.Append("UserName", appUser.UserName);
                            //HttpContext.Response.Cookies.Append("UserRole", appUser.UserRole);
                            //HttpContext.Response.Cookies.Append("Id", appUser.Id.ToString());

                            if (appUser.UserRole == "User")
                            {
                                return RedirectToAction("Index", "Siparis");
                            }
                            else
                            {
                                return RedirectToAction("Index", "UserManager");
                            }



                        }
                        ModelState.AddModelError("", "Yanlış kullanıcı");
                    }
                    ModelState.AddModelError("", "Yanlış kullanıcı");
                }
                
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()

        {
            await _signInmanager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }


}