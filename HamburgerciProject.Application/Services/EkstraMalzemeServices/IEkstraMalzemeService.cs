using HamburgerciProject.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Services.EkstraMalzemeServices
{
    public interface IEkstraMalzemeService
    {
        Task Create(EkstraMalzemeDTO model);
        Task Update(EkstraMalzemeDTO model);
        Task Delete(int id);
        Task<EkstraMalzemeDTO> GetById(int id);
        Task<List<EkstraMalzemeDTO>> GetEkstraMalzemeler();
    }
}
