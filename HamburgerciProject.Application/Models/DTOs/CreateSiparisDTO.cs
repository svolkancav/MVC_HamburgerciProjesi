using HamburgerciProject.Application.Extensions;
using HamburgerciProject.Application.Models.VMs;
using HamburgerciProject.Domain.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Models.DTOs
{
    public class CreateSiparisDTO
    {
        public int Id { get; set; }
        public List<string> İçerik { get; set; }
        public decimal? ToplamTutar { get; set; }
        public List<MenuVM>? Menuler { get; set; }
        public decimal? MenuFiyatlarToplami { get; set; }
        public List<EkstraMalzemeVM>? EkMalzemeler { get; set; }
        public decimal? EkstraMalzemeToplamTutar { get; set; }
        public int? MenuID { get; set; }
        public int? EkMalzemeID { get; set; }
        public Status Status { get; set; } = Status.Inactive;

        //public int AppUserID { get; set; }
        //public UpdateProfileDTO AppUser { get; set; }
    }
}
