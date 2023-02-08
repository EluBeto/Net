using System;
using System.ComponentModel.DataAnnotations;
using WebApiAutor.Utilities;

namespace WebApiAutor.DTOs
{
    public class AutorCreacionDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(maximumLength: 50, ErrorMessage = "El campo {0} no debe ser mayor a {1} carácteres")]
        [PrimeraLetraMayu]
        public string Nombre { get; set; }
    }
}

