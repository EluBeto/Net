using System;
using WebApiAutor.Utilities;

namespace WebApiAutor.DTOs
{
    public class LibroCreacionDto
    {
        [PrimeraLetraMayu]
        public string Titulo { get; set; }
    }
}

