using HamburgerciProject.Domain.Entities.BaseEntities;
using HamburgerciProject.Domain.Enum;
using MVC_HamburgerciProjesi.Models.Enum;

namespace HamburgerciProject.Domain.Entities.Concrete
{
    public class Menu : IBaseEntity
    {
        public int Id { get; set; }
        public string MenuAdi { get; set; }
        public decimal MenuFiyati { get; set; }
        public Boyut Boyutu { get; set; }
        public Status Durumu { get; set; }

        //IBaseEntity
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        public int SiparisId { get; set; }
        public Siparis Siparis { get; set; }
    }
}
