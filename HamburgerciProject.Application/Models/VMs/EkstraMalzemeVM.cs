using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Models.VMs
{
    public class EkstraMalzemeVM
    {
        public int Id { get; set; }
        public string EkstraAdi { get; set; }
        public decimal EkstraFiyat { get; set; }
        public int EkstraMalzemeAdedi { get; set; }
    }
}
