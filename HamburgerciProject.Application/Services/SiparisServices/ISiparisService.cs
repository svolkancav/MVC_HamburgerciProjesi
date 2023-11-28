using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Services.SiparisServices
{
    public interface ISiparisService
    {
        Task Create(CreateSiparisDTO model);
        Task Update(SiparisDTO model);
        Task Delete(int id);
        Task<SiparisDTO> GetById(int id);
        Task<List<CreateSiparisDTO>> GetSiparisler();
        //Task<CreateSiparisDTO> SiparisGir();

        //Task<CreateSiparisDTO> SiparisOlustur(List<MenuVM> menuler, List<EkstraMalzemeVM> ekMalzemeler);
    }
}
