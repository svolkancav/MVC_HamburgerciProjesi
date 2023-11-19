using HamburgerciProject.Domain.Entities.BaseEntities;
using HamburgerciProject.Domain.Enum;
using MVC_HamburgerciProjesi.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace HamburgerciProject.Domain.Entities.Concrete
{
    public class Menu : IBaseEntity
    {
        public int Id { get; set; }
        public string MenuAdi { get; set; }
        public decimal MenuFiyati { get; set; }

        [NotMapped]
        public Boyut Boyutu { get; set; }

        //IBaseEntity
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
        public List<Siparis> Siparisler { get; set; }

        //Todo: Menu ayrı Siparişin içerisindeki menüler ayrı tablo olmalı.
    }
}
