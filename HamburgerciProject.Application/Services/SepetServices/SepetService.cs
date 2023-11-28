using AutoMapper;
using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Domain.Repositories;
using HamburgerciProject.Infrastructure.Context;
using HamburgerciProject.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Services.SepetServices
{
    public class SepetService : ISepetService
    {
        private readonly ISepetRepository _sepetRepository;
        private readonly IMapper _mapper;

        public SepetService(ISepetRepository sepetRepository, IMapper mapper)
        {
            _sepetRepository = sepetRepository;
            _mapper = mapper;
        }
        public async Task<bool> Add(SepetDTO item)
        {
            if (item == null)
                return false;
            else
            {
                var sepet = _mapper.Map<Sepet>(item);
                await _sepetRepository.Create(sepet);
                return true;

            }


        }

        public async Task<bool> Delete(SepetDTO item)
        {
            if (item is not null)
            {
                var sepet = _mapper.Map<Sepet>(item);
                sepet.DeleteDate = DateTime.Now;
                sepet.Status = Domain.Enum.Status.Inactive;
                await _sepetRepository.Delete(sepet);
                return true;
            }
            else
                return false;
            
        }

        public async Task<bool> DeleteById(int id)
        {
            if (id!=null)
            {
                Sepet sepet = await _sepetRepository.GetDefault(x => x.Id == id);
                sepet.DeleteDate = DateTime.Now;
                sepet.Status = Domain.Enum.Status.Inactive;
                await _sepetRepository.Delete(sepet);
                return true;
            }
            else
                return false;

        }

        public async Task<List<SepetDTO>> GetAll()
        {
            var sepet = await _sepetRepository.GetFilteredList(
            select: x => new SepetDTO
            {
                SepetId = x.Id,
                UserId = x.UserId,
               
            },
            where: x => x.Status != Domain.Enum.Status.Inactive,
            orderBy: x => x.OrderBy(x => x.Id)
            );
            return sepet;
        }



        public async Task<bool> Update(SepetDTO item)
        {
            var sepet = _mapper.Map<Sepet>(item);

            if (sepet != null)
            {
                await _sepetRepository.Update(sepet);
                return true;
            }
            else { return false; }
        }


        public async Task<SepetDTO> GetById(int id)
        {
            Sepet sepet = new Sepet();
            sepet = await _sepetRepository.GetDefault(x => x.Id == id);
            SepetDTO menuDTO = new SepetDTO()
            {
                SepetId = sepet.Id,
                UserId = sepet.UserId
            };
            return menuDTO;
        }

    }
}
