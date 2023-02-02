using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiAutor.Utilities;

namespace WebApiAutor.Entidades
{
    public class Autor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(maximumLength: 50, ErrorMessage = "El campo {0} no debe ser mayor a {1} carácteres")]
        [PrimeraLetraMayu]
        public string Nombre { get; set; }
    }
}

