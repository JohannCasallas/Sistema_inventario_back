using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_inventario_backend.Data;
using Sistema_inventario_backend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_inventario_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Cliente/CrearCliente
        [HttpPost("CrearCliente")]
        public async Task<ActionResult<Respuesta<Cliente>>> CrearCliente(Cliente cliente)
        {
            _context.Clientes?.Add(cliente);
            await _context.SaveChangesAsync();

            var respuesta = new Respuesta<Cliente>
            {
                Mensaje = "Cliente creado correctamente",
                Exitoso = true,
                Datos = cliente
            };

            return Ok(respuesta);
        }

        // PUT: api/Cliente/ActualizarCliente/5
        [HttpPut("ActualizarCliente/{id}")]
        public async Task<ActionResult<Respuesta<Cliente>>> ActualizarCliente(int id, Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var respuesta = new Respuesta<Cliente>
            {
                Mensaje = "Cliente actualizado correctamente",
                Exitoso = true,
                Datos = cliente
            };

            return Ok(respuesta);
        }

        // DELETE: api/Cliente/EliminarCliente/5
        [HttpDelete("EliminarCliente/{id}")]
        public async Task<ActionResult<Respuesta<bool>>> EliminarCliente(int id)
        {
            var cliente = await _context.Clientes!.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            var respuesta = new Respuesta<bool>
            {
                Mensaje = "Cliente eliminado correctamente",
                Exitoso = true,
                Datos = true
            };

            return Ok(respuesta);
        }

        // GET: api/Cliente/ObtenerClientePorId/5
        [HttpGet("ObtenerClientePorId/{id}")]
        public async Task<ActionResult<Respuesta<Cliente>>> ObtenerClientePorId(int id)
        {
            var cliente = await _context.Clientes!.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            var respuesta = new Respuesta<Cliente>
            {
                Mensaje = "Cliente obtenido correctamente",
                Exitoso = true,
                Datos = cliente
            };

            return Ok(respuesta);
        }

        // GET: api/Cliente/ListarClientes
        [HttpGet("ListarClientes")]
        public async Task<ActionResult<Respuesta<IEnumerable<Cliente>>>> ListarClientes()
        {
            var clientes = await _context.Clientes!.ToListAsync();

            var respuesta = new Respuesta<IEnumerable<Cliente>>
            {
                Mensaje = "Clientes listados correctamente",
                Exitoso = true,
                Datos = clientes
            };

            return Ok(respuesta);
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes!.Any(e => e.ClienteId == id);
        }
    }
}
