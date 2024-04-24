using System;
using System.ComponentModel.DataAnnotations;

namespace Sistema_inventario_backend.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(100)]
        public string? NombreUsuario { get; set; }

        [Required]
        [StringLength(255)]
        public string? Contrasena { get; set; }

        [StringLength(100)]
        public string? Nombre { get; set; }

        [StringLength(100)]
        public string? Apellido { get; set; }

        [Required]
        public bool Activo { get; set; } 

        [Required]
        public string? Rol { get; set; } 

        public DateTime FechaRegistro { get; set; } 

        [StringLength(100)]
        public string? CorreoElectronico { get; set; } 

        [StringLength(20)]
        public string? Telefono { get; set; } 

    }
}
