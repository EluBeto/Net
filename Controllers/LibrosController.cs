using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutor.Entidades;

namespace WebApiAutor.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public LibrosController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            return await dbContext.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var exist = await dbContext.Autores.AnyAsync(x => x.Id == libro.AutorId);

            if (!exist)
            {
                return BadRequest($"No existe el autor id: {libro.AutorId}");
            }

            dbContext.Add(libro);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}

