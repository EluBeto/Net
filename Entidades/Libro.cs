using System;
using WebApiAutor.Utilities;

namespace WebApiAutor.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        [PrimeraLetraMayu]
        public string Titulo { get; set; }
    }
}

