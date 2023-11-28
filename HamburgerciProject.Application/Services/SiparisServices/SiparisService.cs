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
                    İçerik = x.İçerik,
                    UserId = x.AppUserId,
                    MenuAdi = x.İçerik,
                    MenuFiyatlarToplami = x.ToplamTutar,

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
                    select: x => new Menu()
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

        

        //public async Task<CreateSiparisDTO> SipariseMenuEkle(MenuVM menu, List<EkstraMalzemeVM> ekMalzemeler)
        //{
        //    decimal? tutar = 0;
        //    decimal? toplamFiyatEkMalzemeler = 0;
        //    string icerik = ($"{menu.MenuAdi} - {menu.Boyutu}  x {menu.MenuAdedi} "); // Todo: Ekmalzemeler tek tek buraya eklenmeli.
        //    // menuadi + Boy + Adedi + (ekstra Malzemeler)


        //    switch (menu.Boyutu)
        //    {
        //        case Boyut.Büyük:
        //            tutar = 1.4m * (menu.MenuFiyati) * menu.MenuAdedi;
        //            break;
        //        case Boyut.Orta:
        //            tutar = 1.2m * (menu.MenuFiyati) * menu.MenuAdedi;
        //            break;
        //        case Boyut.Küçük:
        //            tutar = menu.MenuFiyati * menu.MenuAdedi;
        //            break;
        //        default:
        //            break;
        //    }

        //    foreach (var ek in ekMalzemeler)
        //    {
        //        toplamFiyatEkMalzemeler += ek.EkstraFiyat * ek.EkstraMalzemeAdedi;
        //    }

        //    string ekMalzemeIcerik;

        //    List<CreateSiparisDTO> girilenSiparisListesi = new List<CreateSiparisDTO>()
        //    {
        //        new CreateSiparisDTO
        //        {
        //            MenuAdi = menu.MenuAdi,
        //            EkMalzemeler = ekMalzemeler,
        //            MenuFiyatlarToplami = tutar,
        //            EkstraMalzemeToplamTutar = toplamFiyatEkMalzemeler,
        //            ToplamTutar = tutar+toplamFiyatEkMalzemeler,
        //            İçerik = icerik
        //        }
        //    };
        //}

        //public Task<CreateSiparisDTO> SiparisOlustur(List<MenuVM> menuler, List<EkstraMalzemeVM> ekMalzemeler)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
