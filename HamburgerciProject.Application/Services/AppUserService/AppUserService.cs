using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Domain.Repositories;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;


using System.Security.Claims;


namespace HamburgerciProject.Application.Services.AppUserService
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _UserManager;
        public AppUserService(IAppUserRepository appUserRepository, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager = null)
        {
            _appUserRepository = appUserRepository;
            _signInManager = signInManager;
            _UserManager = userManager;
        }

        public async Task<RegisterDTO> GetById(int id)
        {
            AppUser appuser = await _appUserRepository.GetDefault(x => x.Id == id);
            RegisterDTO register = new RegisterDTO()
            {
                Id = appuser.Id,
                Code = appuser.ConfirmCode,
                Email = appuser.Email
            };
            return register;
        }

        public async Task<UpdateProfileDTO> GetByUserName(string userName)
        {
            
            UpdateProfileDTO result = await _appUserRepository.GetFilteredFirstOrDefault(
                select: x => new UpdateProfileDTO
                {
                    UserName = x.UserName,
                    Id = x.Id,
                    Password = x.Password,
                    Email = x.Email,
                    
                },
                where: x => x.UserName == userName
                );
            return result;
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> Login(LoginDTO model)
        {
            return await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        
        public async Task<IdentityResult> Register(RegisterDTO model)
        {


            Random rnd = new Random();
            int code;
            code = rnd.Next(100000, 1000000);
            AppUser user = new AppUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                CreateDate = model.CreateDate,
                Password= model.Password,
                ConfirmCode=model.Code,
                Status = Domain.Enum.Status.Inactive
            };

            
            IdentityResult result = await _UserManager.CreateAsync(user, model.Password);
        
            return result;
        }

        public async Task UpdateUser(UpdateProfileDTO model)
        {
            AppUser user = await _appUserRepository.GetDefault(x => x.Id == model.Id);
            if (model.Password != null)
            {
                _UserManager.PasswordHasher.HashPassword(user, model.Password);
                await _UserManager.UpdateAsync(user);
            }
            if (model.Email != null)
            {
                AppUser isUserEmailExists = await _UserManager.FindByEmailAsync(model.Email);
                if (isUserEmailExists == null)
                    await _UserManager.SetEmailAsync(user, model.Email);

            }
        }
    }
}
