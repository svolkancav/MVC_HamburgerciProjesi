using HamburgerciProject.Domain.Entities.BaseEntities;
using HamburgerciProject.Domain.Enum;

namespace HamburgerciProject.Domain.Entities.Abstract
{
    public abstract class Urun : IBaseEntity
    {
        public decimal Fiyati { get; set; }
        public string Adi { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
    }
}
