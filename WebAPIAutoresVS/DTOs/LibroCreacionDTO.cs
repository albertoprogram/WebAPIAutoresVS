using System.ComponentModel.DataAnnotations;
using WebAPIAutoresVS.Validaciones;

namespace WebAPIAutoresVS.DTOs
{
    public class LibroCreacionDTO
    {
        [PrimeraLetraMayuscula]
        [StringLength(maximumLength: 250)]
        public string Titulo { get; set; }
        public List<int> AutoresIds { get; set; }
    }
}
