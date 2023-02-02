using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutor.Entidades;
using WebApiAutor.Filtros;

namespace WebApiAutor.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public AutoresController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        [HttpGet] // api/autores
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await dbContext.Autores.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> Get(int id)
        {
            var autor = await dbContext.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }

        [HttpGet("nombre{nom}")]
        public async Task<ActionResult<Autor>> Nombre(string nom)
        {
            var autor = await dbContext.Autores.FirstOrDefaultAsync(x => x.Nombre.Contains(nom));

            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Autor autor)
        {
            var autorExiste = await dbContext.Autores.AnyAsync(x => x.Nombre == autor.Nombre);

            if (autorExiste)
            {
                return BadRequest($"El autor ya existe con el nombre {autor.Nombre}");
            }

            dbContext.Add(autor);
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if (autor.Id != id)
            {
                return BadRequest("Id invalido");
            }

            var existe = await dbContext.Autores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            dbContext.Update(autor);
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await dbContext.Autores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            dbContext.Remove(new Autor() { Id = id });
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}

