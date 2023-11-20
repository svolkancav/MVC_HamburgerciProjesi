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
        [PictureFileExtension]
        public IFormFile UploadPath { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
        public decimal ToplamTutar { get; set; }
        public int Adedi { get; set; }
        public string ImagePath { get; set; }
        public List<EkstraMalzemeVM> EkstraMalzemeler { get; set; }
        public List<MenuVM> Menuler { get; set; }
    }
}
