using MVC_HamburgerciProjesi.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Models.VMs
{
    public class MenuVM
    {
        public int Id { get; set; }
        public string MenuAdi { get; set; }
        public decimal MenuFiyati { get; set; }
        public Boyut Boyutu { get; set; }
        public string ImagePath { get; set; }
        public int MenuAdedi { get; set; }
    }
}
