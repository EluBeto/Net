using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutor.DTOs;
using WebApiAutor.Entidades;
using WebApiAutor.Filtros;

namespace WebApiAutor.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper Amapper;

        public AutoresController(ApplicationDbContext context, IMapper mapper)
        {
            dbContext = context;
            Amapper = mapper;
        }

        [HttpGet] // api/autores
        public async Task<List<AutorDto>> Get()
        {
            var autores = await dbContext.Autores.ToListAsync();
            return Amapper.Map<List<AutorDto>>(autores);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AutorDto>> Get(int id)
        {
            var autor = await dbContext.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }
            return Amapper.Map<AutorDto>(autor);
        }

        [HttpGet("nombre{nom}")]
        public async Task<ActionResult<List<AutorDto>>> Nombre(string nom)
        {
            var autores = await dbContext.Autores.Where(x => x.Nombre.Contains(nom)).ToListAsync();

            return Amapper.Map<List<AutorDto>>(autores);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AutorCreacionDto autorCreacionDto)
        {
            var autorExiste = await dbContext.Autores.AnyAsync(x => x.Nombre == autorCreacionDto.Nombre);

            if (autorExiste)
            {
                return BadRequest($"El autor ya existe con el nombre {autorCreacionDto.Nombre}");
            }

            var autor = Amapper.Map<Autor>(autorCreacionDto);

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

