using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Services.SepetServices
{
    public interface ISepetService 
    {
        Task<SepetDTO> GetById(int id);
        Task<bool>Add(SepetDTO item);
        Task<bool> Delete(SepetDTO item);
        Task<bool> DeleteById(int id);
        Task<bool> Update(SepetDTO item);
        Task<List<SepetDTO>> GetAll();


    }
}
