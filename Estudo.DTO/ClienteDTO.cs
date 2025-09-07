using System.ComponentModel.DataAnnotations;

namespace Estudo.DTO
{
    public class ClienteDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 11)]
        public string Documento { get; set; }
    }
}