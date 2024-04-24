using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sistema_inventario_backend.Models
{
    public class Producto
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        [Column(TypeName = "numeric(10, 2)")]
        public decimal Precio { get; set; }

        public int CantidadStock { get; set; }

        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria? Categoria { get; set; }
    }
}
