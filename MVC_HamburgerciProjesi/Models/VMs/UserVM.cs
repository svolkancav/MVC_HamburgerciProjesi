
using HamburgerciProject.Application.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace HamburgerciProject.Presentation.Models.VMs
{
    public class UserVM
    {

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı boş olamaz")]
        [MaxLength(50)]
        [MinLength(3, ErrorMessage = "Kullanıcı Adı 3 karakterden az olamaz")]
        public string UserName { get; set; }


        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre alanı boş olamaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Email Adresiniz")]
        [Required(ErrorMessage = "EMail alanı boş olamaz")]
        public string EMail { get; set; }
    }
}
