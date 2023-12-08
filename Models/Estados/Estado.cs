using System.ComponentModel.DataAnnotations;

namespace BecarioAPI.Models.Estados
{
    public class Estado
    {
        [Key]
        public int IdEstado { get; set; }
        public string Descripcion { get; set; }
    }
}
