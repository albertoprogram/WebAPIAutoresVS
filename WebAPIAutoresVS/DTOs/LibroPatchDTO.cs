using System.ComponentModel.DataAnnotations;
using WebAPIAutoresVS.Validaciones;

namespace WebAPIAutoresVS.DTOs
{
    public class LibroPatchDTO
    {
        [Required]
        [PrimeraLetraMayuscula]
        [StringLength(maximumLength: 250)]
        public string Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}
