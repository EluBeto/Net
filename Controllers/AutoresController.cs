using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutor.Entidades;
using WebApiAutor.Filtros;
using WebApiAutor.Services;

namespace WebApiAutor.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IServicio servicio;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<AutoresController> logger;

        public AutoresController(ApplicationDbContext context, IServicio servicio,
            ServiceTransient serviceTransient, ServiceScoped serviceScoped,
            ServiceSingleton serviceSingleton, ILogger<AutoresController> logger)
        {
            dbContext = context;
            this.servicio = servicio;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
        }

        [HttpGet("GUID")]
        [ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(MiFiltroDeAccion))]
        public ActionResult ObtenerGuid()
        {
            return Ok(new
            {
                AutoresControllerTransiet = serviceTransient.Guid,
                ServicioA_Transiet = servicio.ObtenerTransient(),

                AutoresControllerScoped = serviceScoped.Guid,
                ServicioA_Scoped = servicio.ObtenerScoped(),

                AutoresControllerSingleton = serviceSingleton.Guid,
                ServicioA_Singleton = servicio.ObtenerSingleton()
            });
        }

        [HttpGet] // api/autores
        [HttpGet("listado")] // api/autores/listado
        [HttpGet("/listado")] // listado
        //[Authorize]
        [ServiceFilter(typeof(MiFiltroDeAccion))]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            logger.LogInformation("Estamos obteniendo lista de autores");
            logger.LogWarning("Warnining");
            return await dbContext.Autores.Include(x => x.Libros).ToListAsync();
        }

        [HttpGet("primero")]
        public async Task<ActionResult<Autor>> Primero()
        {
            servicio.RealizaTarea();
            return await dbContext.Autores.FirstOrDefaultAsync();
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

