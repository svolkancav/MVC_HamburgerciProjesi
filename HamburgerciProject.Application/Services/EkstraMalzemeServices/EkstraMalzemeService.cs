using AutoMapper;
using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Services.EkstraMalzemeServices
{
    public class EkstraMalzemeService : IEkstraMalzemeService
    {
        private readonly IEkstraMalzemeRepository _repository;
        private readonly IMapper _mapper;

        public EkstraMalzemeService(IEkstraMalzemeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task Create(EkstraMalzemeDTO model)
        {
            EkstraMalzeme ekstraMalzeme = _mapper.Map<EkstraMalzeme>(model);
            await _repository.Create(ekstraMalzeme);
        }

        public async Task Delete(int id)
        {
            EkstraMalzeme ekstraMalzeme = await _repository.GetDefault(x => x.Id == id);
            ekstraMalzeme.DeleteDate = DateTime.Now;
            ekstraMalzeme.Status = Domain.Enum.Status.Inactive;
            await _repository.Delete(ekstraMalzeme);
        }

        public async Task<EkstraMalzemeDTO> GetById(int id)
        {
            EkstraMalzeme ekstraMalzeme = await _repository.GetDefault(x => x.Id == id);
            EkstraMalzemeDTO ekstraMalzemeDTO = new EkstraMalzemeDTO()
            {
                Id = ekstraMalzeme.Id,
                EkstraAdi = ekstraMalzeme.EkstraAdi,
                EkstraFiyat = ekstraMalzeme.EkstraFiyat
            };
            return ekstraMalzemeDTO;
        }

        public async Task<List<EkstraMalzemeDTO>> GetEkstraMalzemeler()
        {
            var ekstraMalzeme = await _repository.GetFilteredList(
                select: x => new EkstraMalzemeDTO
                {
                    Id = x.Id,
                    EkstraAdi = x.EkstraAdi,
                    EkstraFiyat = x.EkstraFiyat
                },
                where: x => x.Status != Domain.Enum.Status.Inactive,
                orderBy: x => x.OrderBy(x => x.Id)
                );
            return ekstraMalzeme;
        }

        public async Task Update(EkstraMalzemeDTO model)
        {
            var ekstraMalzeme = _mapper.Map<EkstraMalzeme>(model);
            if (ekstraMalzeme != null)
            {
                await _repository.Update(ekstraMalzeme);
            }
        }

    }
}
