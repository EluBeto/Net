using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiAutor.Utilities;

namespace WebApiAutor.Entidades
{
    public class Autor : IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(maximumLength: 50, ErrorMessage = "El campo {0} no debe ser mayor a {1} carácteres")]
        //[PrimeraLetraMayu]
        public string Nombre { get; set; }
        //[Range(18, 120)]
        //[NotMapped]
        //public int Edad { get; set; }
        //[CreditCard]
        //[NotMapped]
        //public string Tarjeta { get; set; }
        public List<Libro> Libros { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La priemra letra debe ser mayúscula",
                        new string[] { nameof(Nombre) });
                }
            }

            /*if(string.IsNullOrEmpty(Tarjeta))
            {
                yield return new ValidationResult("La tarjeta es requerida",
                    new string[] { nameof(Tarjeta) });
            }*/
        }
    }
}

