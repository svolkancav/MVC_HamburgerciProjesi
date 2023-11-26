using HamburgerciProject.Application.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Services.AppUserService
{
    public interface IAppUserService
    {
        //Task<IdentityResult> Register(RegisterDTO model);
        Task<SignInResult> Login(LoginDTO model);
        Task<UpdateProfileDTO> GetById(int id);
        Task<UpdateProfileDTO> GetByUserName(string userName);
        Task UpdateUser(UpdateProfileDTO model);
        Task Logout();
        Task<List<RegisterDTO>> GetUsers();

        Task Delete(int id);



    }
}
