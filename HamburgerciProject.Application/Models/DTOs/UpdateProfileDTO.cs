using HamburgerciProject.Domain.Enum;

namespace HamburgerciProject.Application.Models.DTOs
{
    public class UpdateProfileDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
       
        public DateTime UpdateDate => DateTime.Now;
        public Status status { get; set; }
       
    }
}