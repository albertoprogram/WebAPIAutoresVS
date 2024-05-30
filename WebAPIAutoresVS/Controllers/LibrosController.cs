using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAutoresVS.DTOs;
using WebAPIAutoresVS.Entidades;

namespace WebAPIAutoresVS.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LibrosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDTO>> Get(int id)
        {
            var libro = await context.Libros
                .Include(libroBD => libroBD.Comentarios)
                .FirstOrDefaultAsync(x => x.Id == id);

            return mapper.Map<LibroDTO>(libro);
        }

        [HttpGet("onlybook/{id:int}", Name = "ObtenerLibro")]
        public async Task<ActionResult<LibroDTOConAutores>> GetSoloLibro(int id)
        {
            var libro = await context.Libros
                .Include(libroDB => libroDB.AutoresLibros)
                .ThenInclude(autorLibroDB => autorLibroDB.Autor)
                .FirstOrDefaultAsync(x => x.Id == id);

            libro.AutoresLibros = libro.AutoresLibros.OrderBy(x => x.Orden).ToList();

            return mapper.Map<LibroDTOConAutores>(libro);
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {
            if (libroCreacionDTO.AutoresIds == null)
            {
                return BadRequest("No se puede crear un libro sin autores");
            }

            var autoresIds = await context.Autores
                .Where(autorBD => libroCreacionDTO.AutoresIds
                .Contains(autorBD.Id))
                .ToListAsync();

            if (libroCreacionDTO.AutoresIds.Count != autoresIds.Count)
            {
                return BadRequest("No exite alguno de los autores enviados");
            }

            var libro = mapper.Map<Libro>(libroCreacionDTO);

            if (libro.AutoresLibros != null)
            {
                for (int i = 0; i < libro.AutoresLibros.Count; i++)
                {
                    libro.AutoresLibros[i].Orden = i;
                }
            }

            context.Add(libro);
            await context.SaveChangesAsync();

            var libroDTOOnly = mapper.Map<LibroDTOOnly>(libro);

            return CreatedAtRoute("ObtenerLibro", new { id = libro.Id }, libroDTOOnly);
        }
    }
}
