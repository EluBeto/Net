﻿using System;
using Microsoft.EntityFrameworkCore;
using WebApiAutor.Entidades;

namespace WebApiAutor
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
    }
}
