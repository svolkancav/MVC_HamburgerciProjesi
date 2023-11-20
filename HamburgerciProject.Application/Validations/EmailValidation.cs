using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Application.Validations
{
    public class EmailValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string controlValue;
            if (value == null)
            {
                return false;
            }

            controlValue = value.ToString();

            if (controlValue.Where(k => k == ' ').ToList().Count > 0)
                return false;

            if (controlValue.Split("@").Count() > 2) //birden fazla @ varsa 
                return false;

            if (controlValue.EndsWith("@hotmail.com") || controlValue.EndsWith("@gmail.com") || controlValue.EndsWith("@bilgeadam.com"))
                return true;

            return false;
        }
    }
}
