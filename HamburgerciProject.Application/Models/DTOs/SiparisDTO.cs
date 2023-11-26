using HamburgerciProject.Application.Models.VMs;
using MVC_HamburgerciProjesi.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Models.DTOs
{
    public class SiparisDTO
    {
       
        public int Id { get; set; }
        public decimal? ToplamTutar { get; set; }
        public List<MenuDTO> Menuler { get; set; }
        public int MenuFiyatlarToplami { get; set; }
        public List<EkstraMalzemeVM> EkMalzemeler { get; set; }
        public int EkstraMalzemeToplamTutar { get; set; }
        public int MenuID { get; set; }
        public int EkMalzemeID { get; set; }


        // MenuFiyatlarToplami = (MenuFiyat * Boyut)
        
        // MenuFiyatlarToplami + EkstraMalzemeToplamTutar


    }
}
