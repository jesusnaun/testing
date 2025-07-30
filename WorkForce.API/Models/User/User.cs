namespace WorkForce.API.Models.User
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; // Aquí almacenarás el hash de la contraseña
        public string Email { get; set; } = string.Empty;
        public int RoleId { get; set; }  // Foreign Key a Role
        public int CompanyId { get; set; } // Foreign Key a Company
        public int Active { get; set; }

    }
}
