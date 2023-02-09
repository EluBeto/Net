using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutor.DTOs;
using WebApiAutor.Entidades;

namespace WebApiAutor.Controllers
{
    [ApiController]
    [Route("api/libros/{libroId:int}/comentarios")]
    public class ComentariosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        public ComentariosController(ApplicationDbContext context, IMapper mapper)
        {
            dbContext = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ComentarioDto>>> Get(int libroId)
        {
            var exist = await dbContext.Libros.AnyAsync(libroDb => libroDb.Id == libroId);

            if (!exist)
            {
                return NotFound();
            }

            var comentarios = await dbContext.Comentarios
                .Where(c => c.LibroId == libroId).ToListAsync();

            return mapper.Map<List<ComentarioDto>>(comentarios);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int libroId, ComentarioCreacionDto comentarioCreacionDto)
        {
            var exist = await dbContext.Libros.AnyAsync(libroDb => libroDb.Id == libroId);

            if (!exist)
            {
                return NotFound();
            }

            var comentario = mapper.Map<Comentario>(comentarioCreacionDto);
            comentario.LibroId = libroId;
            dbContext.Add(comentario);

            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}

