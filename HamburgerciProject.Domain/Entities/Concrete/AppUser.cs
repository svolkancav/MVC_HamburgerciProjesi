using HamburgerciProject.Domain.Entities.BaseEntities;
using HamburgerciProject.Domain.Enum;
using Microsoft.AspNetCore.Identity;


namespace HamburgerciProject.Domain.Entities.Concrete
{
    public class AppUser :IdentityUser<Guid>, IBaseEntity
    {
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
    }
}
