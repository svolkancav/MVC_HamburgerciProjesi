using AutoMapper;
using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Menu, MenuDTO>().ReverseMap();
            CreateMap<EkstraMalzeme, EkstraMalzemeDTO>().ReverseMap();
            CreateMap<Siparis, SiparisDTO>().ReverseMap();
            CreateMap<AppUser, RegisterDTO>().ReverseMap();
            CreateMap<AppUser, LoginDTO>().ReverseMap();
            CreateMap<AppUser, UpdateProfileDTO>().ReverseMap();

        }
        
    }
}
