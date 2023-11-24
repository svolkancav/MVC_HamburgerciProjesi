using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Models.VMs;
using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using MVC_HamburgerciProjesi.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Services.MenuServices
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task Create(MenuDTO model)
        {
            Menu menu = new Menu()
            {
                Id = model.Id,
                MenuAdi = model.MenuAdi,
                MenuFiyati = model.MenuFiyati,
                Boyutu = model.Boyutu,

            };
            if (model.UploadPath != null)
            {
                // using SixLabors.ImageSharp;
                Image image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));

                Guid guid = new Guid();
                image.Save($"wwwroot/images/{guid}"); //foler'ın altına kaydettim.

                menu.ImagePath = $"/images/{guid}.jpg";
            }
            else
                menu.ImagePath = $"/images/defaultpost.jpg";
            await _menuRepository.Create(menu);
        }

        public async Task Delete(int id)
        {
            Menu menu = await _menuRepository.GetDefault(x => x.Equals(id));
            menu.DeleteDate = DateTime.Now;
            menu.Status = Domain.Enum.Status.Inactive;
            await _menuRepository.Delete(menu);
        }

        public async Task<MenuDTO> GetById(int id)
        {
            Menu menu = await _menuRepository.GetDefault(x => x.Id == id);
            MenuDTO menuDTO = new MenuDTO()
            {
                Id = menu.Id,
                MenuAdi = menu.MenuAdi,
                MenuFiyati = menu.MenuFiyati,
                Boyutu = menu.Boyutu,
                ImagePath = menu.ImagePath
            };
            return menuDTO;
        }

        public async Task<List<MenuDTO>> GetMenus()
        {
            var menu = await _menuRepository.GetFilteredList(
                select: x => new MenuDTO
                {
                    Id = x.Id,
                    MenuAdi = x.MenuAdi,
                    MenuFiyati = x.MenuFiyati,
                    Boyutu = x.Boyutu,
                },
                where: x => x.Status != Domain.Enum.Status.Inactive,
                orderBy: x => x.OrderBy(x => x.Id)
                );
            return menu;
        }

        public async Task Update(MenuDTO model)
        {

            Menu user = await _menuRepository.GetDefault(x => x.Id == model.Id);
            user.Id = model.Id;
            user.MenuAdi = model.MenuAdi;
            user.MenuFiyati = model.MenuFiyati;

            await _menuRepository.Update(user);

            //    if (menu != null)
            //    {
            //        if (model.UploadPath != null)
            //        {
            //            // using SixLabors.ImageSharp;
            //            Image image = Image.Load(model.UploadPath.OpenReadStream());
            //            image.Mutate(x => x.Resize(600, 560));

            //            Guid guid = new Guid();
            //            image.Save($"wwwroot/images/{guid}"); //foler'ın altına kaydettim.

            //            menu.ImagePath = $"/images/{guid}.jpg";
            //        }
            //        else
            //            menu.ImagePath = $"/images/defaultpost.jpg";
            //        await _menuRepository.Update(menu);
            //    }
        }


    }
}
