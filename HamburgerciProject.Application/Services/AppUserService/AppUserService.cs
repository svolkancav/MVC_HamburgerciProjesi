using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;


using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using HamburgerciProject.Infrastructure.Repositories;

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

        public async Task<UpdateProfileDTO> GetById(int id)
        {
            AppUser appuser = await _appUserRepository.GetDefault(x => x.Id == id);
            UpdateProfileDTO model = new UpdateProfileDTO()
            {
                Id = appuser.Id,
                UserName = appuser.UserName,
                status = appuser.Status,
                Email = appuser.Email,
                UserRole=appuser.UserRole
            };
            return model;
        }

        public async Task<UpdateProfileDTO> GetByUserName(string userName)
        {

            UpdateProfileDTO result = await _appUserRepository.GetFilteredFirstOrDefault(
                select: x => new UpdateProfileDTO
                {
                    UserName = x.UserName,
                    Id = x.Id,
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


        //public async Task<IdentityResult> Register(RegisterDTO model)
        //{


        //    Random rnd = new Random();
        //    int code;
        //    code = rnd.Next(100000, 1000000);
        //    AppUser user = new AppUser()
        //    {
        //        UserName = model.UserName,
        //        Email = model.Email,
        //        CreateDate = model.CreateDate,
        //        Password= model.Password,
        //        ConfirmCode=model.Code,
        //        Status = Domain.Enum.Status.Inactive
        //    };


        //    //IdentityResult result = await _UserManager.CreateAsync(user, model.Password);

        //    return View();
        //}

        public async Task UpdateUser(UpdateProfileDTO model)
        {
            AppUser user = await _appUserRepository.GetDefault(x => x.Id == model.Id);
            user.Status = model.status;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.UpdateDate = DateTime.Now;

            await _appUserRepository.Update(user);

         

        }

        public async Task<List<RegisterDTO>> GetUsers()
        {
            var users = await _appUserRepository.GetFilteredList(
                select: x => new RegisterDTO
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    UserRole = x.UserRole
                },
                where: x => x.Status != Domain.Enum.Status.Inactive,
                orderBy: x => x.OrderBy(x => x.Id)
                );
            return users;
        }

        public async Task Delete(int id)
        {
            AppUser appUser = await _appUserRepository.GetDefault(x => x.Id == id);
            appUser.DeleteDate = DateTime.Now;
            appUser.Status = Domain.Enum.Status.Inactive;
            await _appUserRepository.Delete(appUser);
        }


    }
}

