using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Models.VMs;
using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Domain.Enum;
using HamburgerciProject.Domain.Repositories;
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

        public SiparisService(ISiparisRepository siparisRepository)
        {
            _siparisRepository = siparisRepository;
        }

        public async Task Create(SiparisDTO model)
        {
            Siparis siparis = new Siparis()
            {
                Id = model.Id,
                Adedi = model.Adedi,
                ToplamTutar = model.ToplamTutar
            };
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
                Adedi = siparis.Adedi,
                ToplamTutar = siparis.ToplamTutar

            };
            return siparisDTO;
        }

        public async Task<List<SiparisDTO>> GetSiparisler()
        {
            var siparis = await _siparisRepository.GetFilteredList(
                select: x => new SiparisDTO
                {
                    Id = x.Id,
                    Adedi = x.Adedi,
                    ToplamTutar = x.ToplamTutar
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

        public async Task<CreateSiparisDTO> SiparisGir()
        {
            CreateSiparisDTO model = new CreateSiparisDTO()
            {
                EkstraMalzemeler = await _ekstraMalzemeRepository.GetFilteredList(
                    select: x=> new EkstraMalzemeVM()
                    {
                        Id = x.Id,
                        EkstraAdi = x.EkstraAdi,
                        EkstraFiyat = x.EkstraFiyat
                    },
                    where: x=>x.Status != Status.Inactive,
                    orderBy: x=>x.OrderBy(x => x.EkstraAdi)
                    ),
                Menuler = await _menusRepository.GetFilteredList(
                    select: x => new MenuVM()
                    {
                        Id = x.Id,
                        ImagePath  = x.ImagePath,
                        MenuAdi = x.MenuAdi,
                        Boyutu = x.Boyutu,
                        MenuFiyati = x.MenuFiyati
                    },
                    where: x => x.Status != Status.Inactive,
                    orderBy: x => x.OrderBy(x => x.MenuAdi)
                    )
            };
            return model;
        }
    }
}
