using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiAutor.Utilities
{
    public class PrimeraLetraMayuAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var primneraLetra = value.ToString()[0].ToString();
            if (primneraLetra != primneraLetra.ToUpper())
            {
                return new ValidationResult("La primera letra debe ser mayúscula.");
            }

            return ValidationResult.Success;
        }
    }
}

