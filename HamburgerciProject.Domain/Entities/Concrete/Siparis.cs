using MVC_HamburgerciProjesi.Models.Enum;

namespace MVC_HamburgerciProjesi.Models.Entities.Concrete
{
    public class Siparis
    {
        public Menu SeciliMenusu { get; set; }
        public List<EkstraMalzeme> EkstraMalzemeleri { get; set; }

        public Boyut Boyutu { get; set; }

        public int Adedi { get; set; }
        public decimal ToplamTutar { get; set; }
    }
}
