using HamburgerciProject.Domain.Entities.BaseEntities;
using HamburgerciProject.Domain.Enum;
using Microsoft.AspNetCore.Identity;


namespace HamburgerciProject.Domain.Entities.Concrete
{
    public class AppUser : IdentityUser<int>, IBaseEntity
    {
        //public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; } = "User";
        public int? ConfirmCode { get; set; }
        public ICollection<Siparis> Siparisler { get; set; }

    }
}
