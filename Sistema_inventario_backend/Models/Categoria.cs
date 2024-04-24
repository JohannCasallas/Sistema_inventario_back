using System.ComponentModel.DataAnnotations;

namespace Sistema_inventario_backend.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }
    }
}
