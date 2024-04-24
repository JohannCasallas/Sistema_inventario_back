using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sistema_inventario_backend.Models
{
    public class SalidaInventario
    {
        [Key]
        public int SalidaId { get; set; }

        public int ProductoId { get; set; }

        public int ClienteId { get; set; }

        public int Cantidad { get; set; }

        public DateTime Fecha { get; set; }

        [ForeignKey("ProductoId")]
        public Producto? Producto { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }
    }
}
