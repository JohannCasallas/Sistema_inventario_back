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
    public class ProductoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductoController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Producto/CrearProducto
        [HttpPost("CrearProducto")]
        public async Task<ActionResult<Respuesta<Producto>>> CrearProducto(Producto producto)
        {
            _context.Productos?.Add(producto);
            await _context.SaveChangesAsync();

            var respuesta = new Respuesta<Producto>
            {
                Mensaje = "Producto creado correctamente",
                Exitoso = true,
                Datos = producto
            };

            return Ok(respuesta);
        }

        // PUT: api/Producto/ActualizarProducto/5
        [HttpPut("ActualizarProducto/{id}")]
        public async Task<ActionResult<Respuesta<Producto>>> ActualizarProducto(int id, Producto producto)
        {
            if (id != producto.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var respuesta = new Respuesta<Producto>
            {
                Mensaje = "Producto actualizado correctamente",
                Exitoso = true,
                Datos = producto
            };

            return Ok(respuesta);
        }

        // DELETE: api/Producto/EliminarProducto/5
        [HttpDelete("EliminarProducto/{id}")]
        public async Task<ActionResult<Respuesta<bool>>> EliminarProducto(int id)
        {
            var producto = await _context.Productos!.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            var respuesta = new Respuesta<bool>
            {
                Mensaje = "Producto eliminado correctamente",
                Exitoso = true,
                Datos = true
            };

            return Ok(respuesta);
        }

        // GET: api/Producto/ObtenerProductoPorId/5
        [HttpGet("ObtenerProductoPorId/{id}")]
        public async Task<ActionResult<Respuesta<Producto>>> ObtenerProductoPorId(int id)
        {
            var producto = await _context.Productos!.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            var respuesta = new Respuesta<Producto>
            {
                Mensaje = "Producto obtenido correctamente",
                Exitoso = true,
                Datos = producto
            };

            return Ok(respuesta);
        }

        // GET: api/Producto/ListarProductos
        [HttpGet("ListarProductos")]
        public async Task<ActionResult<Respuesta<IEnumerable<Producto>>>> ListarProductos()
        {
            var productos = await _context.Productos!.ToListAsync();

            var respuesta = new Respuesta<IEnumerable<Producto>>
            {
                Mensaje = "Productos listados correctamente",
                Exitoso = true,
                Datos = productos
            };

            return Ok(respuesta);
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos!.Any(e => e.ProductId == id);
        }
    }
}
