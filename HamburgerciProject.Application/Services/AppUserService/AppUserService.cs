using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Domain.Repositories;
using Microsoft.AspNetCore.Identity;


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


        public async Task<UpdateProfileDTO> GetByUserName(string userName)
        {
            
            UpdateProfileDTO result = await _appUserRepository.GetFilteredFirstOrDefault(
                select: x => new UpdateProfileDTO
                {
                    UserName = x.UserName,
                    Id = x.Id.ToString(),
                    Password = x.PasswordHash,
                    Email = x.Email,
                    
                },
                where: x => x.UserName == userName
                );
            return result;
        }

        public async Task<SignInResult> Login(LoginDTO model)
        {
            return await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterDTO model)
        {
            AppUser user = new AppUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                CreateDate = model.CreateDate,
            };
            IdentityResult result = await _UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            
                await _signInManager.SignInAsync(user, isPersistent: false);
                return result;
        }

        public async Task UpdateUser(UpdateProfileDTO model)
        {
            AppUser user = await _appUserRepository.GetDefault(x => x.Id.ToString() == model.Id);
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
