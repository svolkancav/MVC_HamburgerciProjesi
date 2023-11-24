using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Models.VMs
{
    public class UpdateMenuVM
    {
        public int Id { get; set; }
        public string MenuAdi { get; set; }
        public string MenuFiyat { get; set; }
    }
}
