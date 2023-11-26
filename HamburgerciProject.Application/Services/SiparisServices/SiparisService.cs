using AutoMapper;
using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Models.VMs;
using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Domain.Enum;
using HamburgerciProject.Domain.Repositories;
using MVC_HamburgerciProjesi.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Services.SiparisServices
{
    public class SiparisService : ISiparisService
    {
        private readonly ISiparisRepository _siparisRepository;
        private readonly IEkstraMalzemeRepository _ekstraMalzemeRepository;
        private readonly IMenuRepository _menusRepository;
        private readonly IMapper _mapper;

        public SiparisService(ISiparisRepository siparisRepository, IMapper mapper)
        {
            _siparisRepository = siparisRepository;
            _mapper = mapper;
        }

        public async Task Create(CreateSiparisDTO model)
        {
            Siparis siparis = _mapper.Map<Siparis>(model);
            await _siparisRepository.Create(siparis);
        }

        public async Task Delete(int id)
        {
            Siparis siparis = await _siparisRepository.GetDefault(x => x.Equals(id));
            siparis.DeleteDate = DateTime.Now;
            siparis.Status = Status.Inactive;
            await _siparisRepository.Delete(siparis);
        }

        public async Task<SiparisDTO> GetById(int id)
        {
            Siparis siparis = await _siparisRepository.GetDefault(x => x.Id == id);
            SiparisDTO siparisDTO = new SiparisDTO()
            {
                Id = id,
                ToplamTutar = siparis.ToplamTutar

            };
            return siparisDTO;
        }

        public async Task<List<CreateSiparisDTO>> GetSiparisler()
        {
            var siparis = await _siparisRepository.GetFilteredList(
                select: x => new CreateSiparisDTO
                {
                    Id = x.Id,
                    ToplamTutar = x.ToplamTutar,
                    İçerik = x.İçerik

                },
                where: x => x.Status != Status.Inactive,
                orderBy: x => x.OrderBy(x => x.Id)
                );
            return siparis;
        }

        public async Task Update(SiparisDTO model)
        {
            Siparis siparis = await _siparisRepository.GetDefault(x => x.Id == model.Id);
            if (siparis != null)
            {
                await _siparisRepository.Update(siparis);
            }
        }

        public async Task<CreateSiparisDTO> SiparisListele()
        {
            CreateSiparisDTO model = new CreateSiparisDTO()
            {
                EkMalzemeler = await _ekstraMalzemeRepository.GetFilteredList(
                    select: x => new EkstraMalzemeVM()
                    {
                        Id = x.Id,
                        EkstraAdi = x.EkstraAdi,
                        EkstraFiyat = x.EkstraFiyat
                    },
                    where: x => x.Status != Status.Inactive,
                    orderBy: x => x.OrderBy(x => x.EkstraAdi)
                    ),
                Menuler = await _menusRepository.GetFilteredList(
                    select: x => new MenuVM()
                    {
                        Id = x.Id,
                        ImagePath = x.ImagePath,
                        MenuAdi = x.MenuAdi,
                        Boyutu = x.Boyutu,
                        MenuFiyati = x.MenuFiyati
                    },
                    where: x => x.Status != Status.Inactive,
                    orderBy: x => x.OrderBy(x => x.MenuAdi)
                    
                    ),
            };
            return model;
        }

        public async Task<CreateSiparisDTO> SiparisOlustur(List<MenuVM> menuler, List<EkstraMalzemeVM> ekMalzemeler)
        {
            
            decimal? toplamFiyatMenuler = 0;
            decimal? tutar = 0;
            decimal? toplamFiyatEkMalzemeler = 0;
            


            foreach (var menu in menuler)
            {
                int menuAdedi = menu.MenuAdedi;
                switch (menu.Boyutu)
                {
                    case Boyut.Büyük:
                        tutar = 1.4m * (menu.MenuFiyati) * menu.MenuAdedi;
                        break;
                    case Boyut.Orta:
                        tutar = 1.2m * (menu.MenuFiyati) * menu.MenuAdedi;
                        break;
                    case Boyut.Küçük:
                        tutar = menu.MenuFiyati * menu.MenuAdedi;
                        break;
                    default:
                        break;
                }


                toplamFiyatMenuler += tutar;
            }


            foreach (var ek in ekMalzemeler)
            {
                toplamFiyatEkMalzemeler += ek.EkstraFiyat * ek.EkstraMalzemeAdedi;
            }

            
            CreateSiparisDTO createSiparisDTO = new CreateSiparisDTO();
            createSiparisDTO.Menuler = menuler;
            createSiparisDTO.EkMalzemeler = ekMalzemeler;
            createSiparisDTO.MenuFiyatlarToplami = toplamFiyatMenuler;
            createSiparisDTO.EkstraMalzemeToplamTutar = toplamFiyatEkMalzemeler;
            createSiparisDTO.ToplamTutar = createSiparisDTO.MenuFiyatlarToplami + createSiparisDTO.EkstraMalzemeToplamTutar;
         
            List<string> list = new List<string>();

            foreach (var item in menuler)
            {
                list.Append($"{item.MenuAdi} {item.Boyutu} {item.MenuAdedi}");
            }

            createSiparisDTO.İçerik = list;


            // menuadi + Boy + Adedi + (ekstra Malzemeler)

            return createSiparisDTO;

        }
    }
}
