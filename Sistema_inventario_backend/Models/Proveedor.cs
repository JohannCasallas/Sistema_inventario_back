using System.ComponentModel.DataAnnotations;

namespace Sistema_inventario_backend.Models
{
    public class Proveedor
    {
        [Key]
        public int ProveedorId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nombre { get; set; }

        public string? Direccion { get; set; }

        [StringLength(100)]
        public string? Contacto { get; set; }
    }
}
