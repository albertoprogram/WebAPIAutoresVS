using System.ComponentModel.DataAnnotations;

namespace WebAPIAutoresVS.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
