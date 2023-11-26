using HamburgerciProject.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Models.VMs
{
    public class SiparisVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string  Username { get; set; }
        public decimal? ToplamTutar { get; set; }
        public List<MenuDTO> Menuler { get; set; }
        public List<EkstraMalzemeDTO> EkstraMalzemeler { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }




    }
}
