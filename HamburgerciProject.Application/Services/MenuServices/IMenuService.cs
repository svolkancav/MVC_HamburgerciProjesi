using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Models.VMs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Services.MenuServices
{
    public interface IMenuService
    {
        Task Create(MenuDTO model);
        Task Update(MenuDTO model);
        Task Delete(int id);
        Task <MenuDTO> GetById(int id);
        Task<List<MenuDTO>> GetMenus();

    }
}
