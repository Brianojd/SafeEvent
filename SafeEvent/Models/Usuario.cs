using SafeEvent.Enum;

namespace SafeEvent.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;

        
        public RolUsuarioEnum Rol { get; set; }
    }
}