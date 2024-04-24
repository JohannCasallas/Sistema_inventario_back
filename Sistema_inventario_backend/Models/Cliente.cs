using System.ComponentModel.DataAnnotations;

namespace Sistema_inventario_backend.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nombre { get; set; }

        public string? Direccion { get; set; }

        [StringLength(100)]
        public string? Contacto { get; set; }
    }
}
