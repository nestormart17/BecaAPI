using System.ComponentModel.DataAnnotations;

namespace BecarioAPI.Models.Usuarios
{
    public class Usuario
    {
        [Key]
        [EmailAddress(ErrorMessage = "El email no es valido")]
        public string Email { get; set; }
        //agregamos la contraseña con tipo de datos varbinary
        [Required]
        [MinLength(8)] //minimo 8 caracteres
        public byte[] Contrasena { get; set; }
        [Required]
        //agregamos la restricion de tipo de usuario 1: administrador, 2: Becario
        [StringLength(1)]
        public int TipodeUsuario { get; set; }
    }
}
