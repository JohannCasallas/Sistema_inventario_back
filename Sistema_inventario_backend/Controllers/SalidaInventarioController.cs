using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_inventario_backend.Data;
using Sistema_inventario_backend.Models;
using System;
using System.Threading.Tasks;

namespace Sistema_inventario_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidaInventarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalidaInventarioController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método para registrar una salida de inventario.
        /// </summary>
        /// <param name="salida">Datos de la salida de inventario a registrar.</param>
        /// <returns>Respuesta de la solicitud con información sobre la operación realizada.</returns>
        [HttpPost("RegistrarSalidaInventario")]
        public async Task<ActionResult<Respuesta<SalidaInventario>>> RegistrarSalidaInventario(SalidaInventario salida)
        {
            // Verificar si el producto y el cliente existen
            var producto = await _context.Productos!.FindAsync(salida.ProductoId);
            if (producto == null)
            {
                var respuestaProductoNoEncontrado = new Respuesta<SalidaInventario>
                {
                    Mensaje = "Error: El producto especificado no fue encontrado.",
                    Exitoso = false,
                    Datos = null
                };
                return BadRequest(respuestaProductoNoEncontrado);
            }

            var cliente = await _context.Clientes!.FindAsync(salida.ClienteId);
            if (cliente == null)
            {
                var respuestaClienteNoEncontrado = new Respuesta<SalidaInventario>
                {
                    Mensaje = "Error: El cliente especificado no fue encontrado.",
                    Exitoso = false,
                    Datos = null
                };
                return BadRequest(respuestaClienteNoEncontrado);
            }

            // Guardar la salida de inventario
            _context.SalidasInventario!.Add(salida);
            await _context.SaveChangesAsync();

            var respuesta = new Respuesta<SalidaInventario>
            {
                Mensaje = "Salida de inventario registrada correctamente",
                Exitoso = true,
                Datos = salida
            };

            return Ok(respuesta);
        }
    }
}
