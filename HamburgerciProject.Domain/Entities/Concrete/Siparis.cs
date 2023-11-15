using HamburgerciProject.Domain.Entities.BaseEntities;
using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Domain.Enum;
using MVC_HamburgerciProjesi.Models.Enum;

namespace HamburgerciProject.Domain.Entities.Concrete
{
    public class Siparis : IBaseEntity
    {
        public Menu SeciliMenusu { get; set; }
        public List<EkstraMalzeme> EkstraMalzemeleri { get; set; }

        public Boyut Boyutu { get; set; }

        public int Adedi { get; set; }
        public decimal ToplamTutar { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
    }
}
