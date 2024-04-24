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
    public class TransaccionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransaccionController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método para registrar una nueva transacción en el sistema.
        /// </summary>
        /// <param name="transaccion">Datos de la transacción a registrar.</param>
        /// <returns>Respuesta de la solicitud con información sobre la operación realizada.</returns>
        [HttpPost("RegistrarTransaccion")]
        public async Task<ActionResult<Respuesta<Transaccion>>> RegistrarTransaccion(Transaccion transaccion)
        {
            try
            {
                // Agregar la transacción a la base de datos
                _context.Transacciones?.Add(transaccion);
                await _context.SaveChangesAsync();

                // Crear una respuesta exitosa
                var respuesta = new Respuesta<Transaccion>
                {
                    Mensaje = "Transacción registrada correctamente",
                    Exitoso = true,
                    Datos = transaccion
                };

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error en caso de excepción
                var respuestaError = new Respuesta<Transaccion>
                {
                    Mensaje = $"Error al registrar la transacción: {ex.Message}",
                    Exitoso = false,
                    Datos = null
                };

                return StatusCode(500, respuestaError);
            }
        }
    }
}
