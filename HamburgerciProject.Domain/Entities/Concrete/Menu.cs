using HamburgerciProject.Domain.Entities.BaseEntities;
using HamburgerciProject.Domain.Enum;
using Microsoft.AspNetCore.Http;
using MVC_HamburgerciProjesi.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HamburgerciProject.Domain.Entities.Concrete
{
    public class Menu : IBaseEntity
    {
        public int Id { get; set; }
        public string MenuAdi { get; set; }
        public decimal MenuFiyati { get; set; }
        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile UploadPath { get; set; }

        [NotMapped]
        public Boyut Boyutu { get; set; }

        //IBaseEntity
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; } = Status.Active;
        public ICollection<MenuSiparis> Siparisler { get; set; }

        //Todo: Menu ayrı Siparişin içerisindeki menüler ayrı tablo olmalı.
    }
}
