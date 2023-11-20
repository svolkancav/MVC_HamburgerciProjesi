using HamburgerciProject.Domain.Entities.BaseEntities;
using HamburgerciProject.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace HamburgerciProject.Domain.Entities.Concrete
{
    public class EkstraMalzeme : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string EkstraAdi { get; set; }
        public decimal EkstraFiyat { get; set; }

        //IBaseEntity
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
        public ICollection<EkstraMalzemelerSiparis> Siparisler { get; set; }
      

    }
}
