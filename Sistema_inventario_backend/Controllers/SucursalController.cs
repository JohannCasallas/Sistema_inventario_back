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
    public class SucursalController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SucursalController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Sucursal/CrearSucursal
        [HttpPost("CrearSucursal")]
        public async Task<ActionResult<Respuesta<Sucursal>>> CrearSucursal(Sucursal sucursal)
        {
            _context.Sucursales?.Add(sucursal);
            await _context.SaveChangesAsync();

            var respuesta = new Respuesta<Sucursal>
            {
                Mensaje = "Sucursal creada correctamente",
                Exitoso = true,
                Datos = sucursal
            };

            return Ok(respuesta);
        }

        // PUT: api/Sucursal/ActualizarSucursal/5
        [HttpPut("ActualizarSucursal/{id}")]
        public async Task<ActionResult<Respuesta<Sucursal>>> ActualizarSucursal(int id, Sucursal sucursal)
        {
            if (id != sucursal.SucursalId)
            {
                return BadRequest();
            }

            _context.Entry(sucursal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SucursalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var respuesta = new Respuesta<Sucursal>
            {
                Mensaje = "Sucursal actualizada correctamente",
                Exitoso = true,
                Datos = sucursal
            };

            return Ok(respuesta);
        }

        // DELETE: api/Sucursal/EliminarSucursal/5
        [HttpDelete("EliminarSucursal/{id}")]
        public async Task<ActionResult<Respuesta<bool>>> EliminarSucursal(int id)
        {
            var sucursal = await _context.Sucursales!.FindAsync(id);
            if (sucursal == null)
            {
                return NotFound();
            }

            _context.Sucursales.Remove(sucursal);
            await _context.SaveChangesAsync();

            var respuesta = new Respuesta<bool>
            {
                Mensaje = "Sucursal eliminada correctamente",
                Exitoso = true,
                Datos = true
            };

            return Ok(respuesta);
        }

        // GET: api/Sucursal/ObtenerSucursalPorId/5
        [HttpGet("ObtenerSucursalPorId/{id}")]
        public async Task<ActionResult<Respuesta<Sucursal>>> ObtenerSucursalPorId(int id)
        {
            var sucursal = await _context.Sucursales!.FindAsync(id);

            if (sucursal == null)
            {
                return NotFound();
            }

            var respuesta = new Respuesta<Sucursal>
            {
                Mensaje = "Sucursal obtenida correctamente",
                Exitoso = true,
                Datos = sucursal
            };

            return Ok(respuesta);
        }

        // GET: api/Sucursal/ListarSucursales
        [HttpGet("ListarSucursales")]
        public async Task<ActionResult<Respuesta<IEnumerable<Sucursal>>>> ListarSucursales()
        {
            var sucursales = await _context.Sucursales!.ToListAsync();

            var respuesta = new Respuesta<IEnumerable<Sucursal>>
            {
                Mensaje = "Sucursales listadas correctamente",
                Exitoso = true,
                Datos = sucursales
            };

            return Ok(respuesta);
        }

        private bool SucursalExists(int id)
        {
            return _context.Sucursales!.Any(e => e.SucursalId == id);
        }
    }
}
