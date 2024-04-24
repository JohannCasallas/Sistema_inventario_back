using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sistema_inventario_backend.Models
{
    public class Transaccion
    {
        [Key]
        public int TransaccionId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Tipo { get; set; }

        public DateTime Fecha { get; set; }

        [Column(TypeName = "numeric(10, 2)")]
        public decimal MontoTotal { get; set; }
    }
}
