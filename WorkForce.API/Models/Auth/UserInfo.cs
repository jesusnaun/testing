namespace WorkForce.API.Models.Auth
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; // Agregamos esto para la verificación
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}
