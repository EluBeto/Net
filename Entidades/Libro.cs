using System;
using System.ComponentModel.DataAnnotations;
using WebApiAutor.Utilities;

namespace WebApiAutor.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        [Required]
        [PrimeraLetraMayu]
        public string Titulo { get; set; }
        public List<Comentario> Comentarios { get; set; }
    }
}

