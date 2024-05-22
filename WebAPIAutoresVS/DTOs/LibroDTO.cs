using System.ComponentModel.DataAnnotations;
using WebAPIAutoresVS.Validaciones;

namespace WebAPIAutoresVS.DTOs
{
    public class LibroDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public List<ComentarioDTO> Comentarios { get; set; }
    }
}
