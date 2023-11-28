using HamburgerciProject.Domain.Entities.BaseEntities;
using HamburgerciProject.Domain.Enum;
using MVC_HamburgerciProjesi.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Domain.Entities.Concrete
{
    public class Sepet : IBaseEntity
    {
        public int Id { get; set; }
        public int? MenuID { get; set; }
        public Menu? Menu { get; set; }
        public int? ExtraMalzemeID { get; set; }
        public EkstraMalzeme? EkstraMalzeme { get; set; }
        public int Adet { get; set; }
        public Boyut Boyut { get; set; }
        public decimal? Fiyat { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
    }
}
