using HamburgerciProject.Domain.Entities.BaseEntities;
using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Domain.Enum;
using MVC_HamburgerciProjesi.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace HamburgerciProject.Domain.Entities.Concrete
{
    public class Siparis : IBaseEntity
    {
        public int Id { get; set; }

        [Required]
        public int Adedi { get; set; }
        public decimal ToplamTutar { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
        public List<Menu> Menuler { get; set; }
        public List<EkstraMalzeme> EkstraMalzemeleri { get; set; }

    }
}
