using HamburgerciProject.Application.Extensions;
using HamburgerciProject.Application.Models.VMs;
using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Domain.Enum;
using Microsoft.AspNetCore.Http;
using MVC_HamburgerciProjesi.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Models.DTOs
{
    public class CreateSiparisDTO
    {
        public CreateSiparisDTO()
        {
            Menuler = new List<Menu>();
            Menu = new Menu();
        }
        public int Id { get; set; }
        public string? İçerik { get; set; }
        public decimal? ToplamTutar { get; set; }
        public string MenuAdi { get; set; }
        public List<Menu>? Menuler { get; set; }
        public decimal? MenuFiyatlarToplami { get; set; }
        public List<EkstraMalzemeVM>? EkMalzemeler { get; set; }
        public decimal? EkstraMalzemeToplamTutar { get; set; }
        public int? MenuID { get; set; }
        public int? EkMalzemeID { get; set; }
        public Status Status { get; set; } = Status.Inactive;
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public Boyut Boyut { get; set; }
        public int Adedi { get; set; }
        public List<SepetDTO> Sepettekiler { get; set; }
        public int ekleme { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Menu Menu { get; set; }
    }
}
