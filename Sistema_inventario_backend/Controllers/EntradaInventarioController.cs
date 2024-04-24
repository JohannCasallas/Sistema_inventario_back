using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_inventario_backend.Data;
using Sistema_inventario_backend.Models;
using System;
using System.Threading.Tasks;

namespace Sistema_inventario_backend.Controllers
{
    /// <summary>
    /// Controlador para manejar las operaciones relacionadas con las entradas de inventario.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EntradaInventarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor del controlador de entradas de inventario.
        /// </summary>
        /// <param name="context">Contexto de la base de datos de la aplicación.</param>
        public EntradaInventarioController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método para registrar una entrada de inventario.
        /// </summary>
        /// <param name="entrada">Datos de la entrada de inventario a registrar.</param>
        /// <returns>Respuesta de la solicitud con información sobre la operación realizada.</returns>
        [HttpPost("RegistrarEntradaInventario")]
        public async Task<ActionResult<Respuesta<EntradaInventario>>> RegistrarEntradaInventario(EntradaInventario entrada)
        {
            // Verificar si el producto y el proveedor existen
            var producto = await _context.Productos!.FindAsync(entrada.ProductoId);
            if (producto == null)
            {
                var respuestaProductoNoEncontrado = new Respuesta<EntradaInventario>
                {
                    Mensaje = "Error: El producto especificado no fue encontrado.",
                    Exitoso = false,
                    Datos = null
                };
                return BadRequest(respuestaProductoNoEncontrado);
            }

            var proveedor = await _context.Proveedores!.FindAsync(entrada.ProveedorId);
            if (proveedor == null)
            {
                var respuestaProveedorNoEncontrado = new Respuesta<EntradaInventario>
                {
                    Mensaje = "Error: El proveedor especificado no fue encontrado.",
                    Exitoso = false,
                    Datos = null
                };
                return BadRequest(respuestaProveedorNoEncontrado);
            }

            // Guardar la entrada de inventario
            _context.EntradasInventario?.Add(entrada);
            await _context.SaveChangesAsync();

            var respuesta = new Respuesta<EntradaInventario>
            {
                Mensaje = "Entrada de inventario registrada correctamente",
                Exitoso = true,
                Datos = entrada
            };

            return Ok(respuesta);
        }
    }
}
