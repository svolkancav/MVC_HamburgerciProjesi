using HamburgerciProject.Application.Models.VMs;
using HamburgerciProject.Domain.Entities.Concrete;
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
        public string MenuAdi { get; set; }
        public List<MenuDTO> Menuler { get; set; }
        public int MenuFiyatlarToplami { get; set; }
        public List<EkstraMalzemeVM> EkMalzemeler { get; set; }
        public int EkstraMalzemeToplamTutar { get; set; }
        public int MenuID { get; set; }
        public int EkMalzemeID { get; set; }
        public Boyut Boyut { get; set; }
        public int Adedi { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public List<SepetDTO> Sepettekiler { get; set; }
        public int ekleme { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        // MenuFiyatlarToplami = (MenuFiyat * Boyut)

        // MenuFiyatlarToplami + EkstraMalzemeToplamTutar


    }
}
