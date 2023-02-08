using System;
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
        }
    }
}

