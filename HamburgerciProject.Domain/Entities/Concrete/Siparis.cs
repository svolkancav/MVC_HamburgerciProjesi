using HamburgerciProject.Domain.Entities.BaseEntities;
using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Domain.Enum;
using MVC_HamburgerciProjesi.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HamburgerciProject.Domain.Entities.Concrete
{
    public class Siparis : IBaseEntity
    {
        public int Id { get; set; }

        [Required]
        public int Adedi { get; set; }
        public decimal? ToplamTutar { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
        [NotMapped]
        public string? İçerik { get; set; }
        public AppUser appUser { get; set; }

        [ForeignKey(nameof(AppUser))]

        public int AppUserId { get; set; }
        public ICollection<MenuSiparis> Menuler{ get; set; }
        public ICollection<EkstraMalzemelerSiparis> EkstraMalzemeler{ get; set; }



    }
}
