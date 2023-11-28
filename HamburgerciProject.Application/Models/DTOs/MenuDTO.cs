using HamburgerciProject.Application.Extensions;
using Microsoft.AspNetCore.Http;
using MVC_HamburgerciProjesi.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Models.DTOs
{
    public class MenuDTO
    {
        public int Id { get; set; }
        public string MenuAdi { get; set; }
        public decimal MenuFiyati { get; set; }
        public IFormFile ImagePath { get; set; }

        [PictureFileExtension]
        public IFormFile UploadPath { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public Boyut Boyut { get; set; }
    }
}
