using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutor.DTOs;
using WebApiAutor.Entidades;

namespace WebApiAutor.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private IMapper Amapper;

        public LibrosController(ApplicationDbContext context, IMapper mapper)
        {
            dbContext = context;
            Amapper = mapper;
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDto>> Get(int id)
        {
            var libro = await dbContext.Libros.FirstOrDefaultAsync(x => x.Id == id);

            return Amapper.Map<LibroDto>(libro);
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDto libro)
        {
            /*
            var exist = await dbContext.Autores.AnyAsync(x => x.Id == libro.AutorId);

            if (!exist)
            {
                return BadRequest($"No existe el autor id: {libro.AutorId}");
            }
            */
            var libroDto = Amapper.Map<Libro>(libro);

            dbContext.Add(libroDto);

            await dbContext.SaveChangesAsync();

            return Ok();
        }
        
    }
}

