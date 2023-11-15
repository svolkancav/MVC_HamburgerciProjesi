﻿using HamburgerciProject.Domain.Entities.BaseEntities;
using HamburgerciProject.Domain.Enum;

namespace HamburgerciProject.Domain.Entities.Concrete
{
    public class EkstraMalzeme : IBaseEntity
    {
        public string EkstraAdi { get; set; }
        public decimal EkstraFiyat { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
    }
}
