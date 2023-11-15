using HamburgerciProject.Domain.Entities.BaseEntities;
using HamburgerciProject.Domain.Enum;

namespace HamburgerciProject.Domain.Entities.Concrete
{
    public class Menu : IBaseEntity
    {
        public string MenuAdi { get; set; }
        public decimal MenuFiyati { get; set; }

        //IBaseEntity
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
    }
}
