﻿using HamburgerciProject.Application.Validations;
using HamburgerciProject.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HamburgerciProject.Application.Models.DTOs
{
    public class RegisterDTO
    {
        public int Id { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı boş olamaz")]
        [MaxLength(50)]
        [MinLength(3, ErrorMessage = "Kullanıcı Adı 3 karakterden az olamaz")]
        public string UserName { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre alanı boş olamaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
       
        public string UserRole { get; set; } = "User";
        public string Email { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public Status status => Status.Active;
    }
}
