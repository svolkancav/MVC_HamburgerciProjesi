using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Domain.Entities.Concrete
{
    public class EkstraMalzemelerSiparis
    {
        public EkstraMalzeme ekstraMalzeme { get; set; }
        public int EkstraMalzemeId { get; set; }
        public Siparis siparis { get; set; }
        public int SiparisId { get; set; }

    }
}
