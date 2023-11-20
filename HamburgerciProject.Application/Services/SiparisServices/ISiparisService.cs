using HamburgerciProject.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Services.SiparisServices
{
    public interface ISiparisService
    {
        Task Create(SiparisDTO model);
        Task Update(SiparisDTO model);
        Task Delete(int id);
        Task<SiparisDTO> GetById(int id);
        Task<List<SiparisDTO>> GetSiparisler();
        Task<CreateSiparisDTO> SiparisGir();
    }
}
