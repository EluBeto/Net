﻿using System;
using AutoMapper;
using WebApiAutor.DTOs;
using WebApiAutor.Entidades;

namespace WebApiAutor.Utilities
{
    public class AutomapperProfiles: Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<AutorCreacionDto, Autor>();
            CreateMap<Autor, AutorDto>();
            CreateMap<LibroCreacionDto, Libro>();
            CreateMap<Libro, LibroDto>();
            CreateMap<ComentarioCreacionDto, Comentario>();
            CreateMap<Comentario, ComentarioDto>();
        }
    }
}

