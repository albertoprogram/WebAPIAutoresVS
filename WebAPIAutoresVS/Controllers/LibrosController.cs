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

        //[HttpGet("onlybook/{id:int}")]
        //public async Task<ActionResult<LibroDTOOnly>> GetSoloLibro(int id)
        //{
        //    var libro = await context.Libros
        //        .FirstOrDefaultAsync(x => x.Id == id);

        //    return mapper.Map<LibroDTOOnly>(libro);
        //}

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {
            //var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);

            //if (!existeAutor)
            //{
            //    return BadRequest($"No existe el autor de Id: {libro.AutorId}");
            //}

            var libro = mapper.Map<Libro>(libroCreacionDTO);

            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
