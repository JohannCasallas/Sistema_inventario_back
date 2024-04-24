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
    public class ProveedorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProveedorController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Proveedor/CrearProveedor
        [HttpPost("CrearProveedor")]
        public async Task<ActionResult<Respuesta<Proveedor>>> CrearProveedor(Proveedor proveedor)
        {
            _context.Proveedores?.Add(proveedor);
            await _context.SaveChangesAsync();

            var respuesta = new Respuesta<Proveedor>
            {
                Mensaje = "Proveedor creado correctamente",
                Exitoso = true,
                Datos = proveedor
            };

            return Ok(respuesta);
        }

        // PUT: api/Proveedor/ActualizarProveedor/5
        [HttpPut("ActualizarProveedor/{id}")]
        public async Task<ActionResult<Respuesta<Proveedor>>> ActualizarProveedor(int id, Proveedor proveedor)
        {
            if (id != proveedor.ProveedorId)
            {
                return BadRequest();
            }

            _context.Entry(proveedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var respuesta = new Respuesta<Proveedor>
            {
                Mensaje = "Proveedor actualizado correctamente",
                Exitoso = true,
                Datos = proveedor
            };

            return Ok(respuesta);
        }

        // DELETE: api/Proveedor/EliminarProveedor/5
        [HttpDelete("EliminarProveedor/{id}")]
        public async Task<ActionResult<Respuesta<bool>>> EliminarProveedor(int id)
        {
            var proveedor = await _context.Proveedores!.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();

            var respuesta = new Respuesta<bool>
            {
                Mensaje = "Proveedor eliminado correctamente",
                Exitoso = true,
                Datos = true
            };

            return Ok(respuesta);
        }

        // GET: api/Proveedor/ObtenerProveedorPorId/5
        [HttpGet("ObtenerProveedorPorId/{id}")]
        public async Task<ActionResult<Respuesta<Proveedor>>> ObtenerProveedorPorId(int id)
        {
            var proveedor = await _context.Proveedores!.FindAsync(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            var respuesta = new Respuesta<Proveedor>
            {
                Mensaje = "Proveedor obtenido correctamente",
                Exitoso = true,
                Datos = proveedor
            };

            return Ok(respuesta);
        }

        // GET: api/Proveedor/ListarProveedores
        [HttpGet("ListarProveedores")]
        public async Task<ActionResult<Respuesta<IEnumerable<Proveedor>>>> ListarProveedores()
        {
            var proveedores = await _context.Proveedores!.ToListAsync();

            var respuesta = new Respuesta<IEnumerable<Proveedor>>
            {
                Mensaje = "Proveedores listados correctamente",
                Exitoso = true,
                Datos = proveedores
            };

            return Ok(respuesta);
        }

        private bool ProveedorExists(int id)
        {
            return _context.Proveedores!.Any(e => e.ProveedorId == id);
        }
    }
}
