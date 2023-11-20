using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Models.DTOs
{
    public class SiparisDTO
    {
        public int Id { get; set; }

        [Required]
        public int Adedi { get; set; }
        public decimal ToplamTutar { get; set; }
    }
}
