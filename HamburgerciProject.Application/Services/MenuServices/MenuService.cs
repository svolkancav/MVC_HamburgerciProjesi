using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Domain.Repositories;
using AutoMapper;

namespace HamburgerciProject.Application.Services.MenuServices
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public MenuService(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task Create(MenuDTO model)
        {
            Menu menu = new Menu();

            if (model.UploadPath != null)
            {
               
                var extension = Path.GetExtension(model.ImagePath.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", newimagename);
                var memoryStream = new MemoryStream();
                var stream = new FileStream(location, FileMode.Create);
                model.ImagePath.CopyTo(memoryStream);
                menu.ImagePath = newimagename;
                // using SixLabors.ImageSharp;
                //Image image = Image.Load(model.UploadPath.OpenReadStream());
                //image.Mutate(x => x.Resize(600, 560));

                //Guid guid = new Guid();

                //image.Save($"wwwroot/images/{guid}"); //foler'ın altına kaydettim.

                //menu.ImagePath = $"/images/{guid}.jpg";
            }
            else
            {
                menu.ImagePath = $"/images/defaultpost.jpg";
            }

            menu.MenuAdi = model.MenuAdi;
            menu.MenuFiyati = model.MenuFiyati;
            menu.CreateDate = model.CreateDate;

            await _menuRepository.Create(menu);
        }

        public async Task Delete(int id)
        {
            Menu menu = await _menuRepository.GetDefault(x => x.Id == id);
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
                },
                where: x => x.Status != Domain.Enum.Status.Inactive,
                orderBy: x => x.OrderBy(x => x.Id)
                );
            return menu;
        }

        public async Task Update(MenuDTO model)
        {
            var menu = _mapper.Map<Menu>(model);
            //Menu menu = await _menuRepository.GetDefault(x => x.Id == model.Id);

            if (menu != null)
            {
                await _menuRepository.Update(menu);
            }
            

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
