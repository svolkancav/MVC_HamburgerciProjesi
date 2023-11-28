using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Domain.Enum;
using MVC_HamburgerciProjesi.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Models.DTOs
{
    public class SepetDTO
    {
        public SepetDTO()
        {
            Sepettekiler = new();
        }
        public int SepetId { get; set; }
        public List<Sepet> Sepettekiler { get; set; }
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
        public Status Status { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

    }
}
