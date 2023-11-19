using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Domain.Entities.Concrete
{
    public class MenuSiparis
    {
        public Menu menu { get; set; }
        public int MenuId { get; set; }
        public Siparis siparis { get; set; }
        public int SiparisId { get; set; }
    }
}
