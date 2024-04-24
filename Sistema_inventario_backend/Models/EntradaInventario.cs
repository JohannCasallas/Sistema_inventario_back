using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sistema_inventario_backend.Models
{
    public class EntradaInventario
    {
        [Key]
        public int EntradaId { get; set; }

        public int ProductoId { get; set; }

        public int ProveedorId { get; set; }

        public int Cantidad { get; set; }

        public DateTime Fecha { get; set; }

        [ForeignKey("ProductoId")]
        public Producto? Producto { get; set; }

        [ForeignKey("ProveedorId")]
        public Proveedor? Proveedor { get; set; }
    }
}
