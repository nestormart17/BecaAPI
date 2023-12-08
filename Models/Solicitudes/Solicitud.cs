using System.ComponentModel.DataAnnotations;

namespace BecarioAPI.Models.Solicitudes
{
    public class Solicitud
    {
        [Key]
        public int IdSolicitud { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "El nombre no es valido ")]
        public string Nombre { get; set; }
        [Required]
        public int IdSolicitante { get; set; }
        [Required]
        public DateTime FechadeCreacion { get; set; }
        [Required]
        public int IdEstado { get; set; }
    }
}
