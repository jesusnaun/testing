using System.ComponentModel.DataAnnotations;

namespace WorkForce.API.Models.User
{
    public class UserCreateDto
    {
        [Required, StringLength(50)]
        public string Username { get; set; } = null!; // No puede ser nulo, longitud máxima de 50 caracteres

        [Required, StringLength(100, MinimumLength = 8)]
        public string Password { get; set; } = null!; // Contraseña en texto plano antes de hashear

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int RoleId { get; set; }

        [Range(1, int.MaxValue)]
        public int CompanyId { get; set; }
        
        public bool Active { get; set; } = true;
    }
}
