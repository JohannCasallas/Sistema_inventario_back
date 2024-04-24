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
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Categoria/CrearCategoria
        [HttpPost("CrearCategoria")]
        public async Task<ActionResult<Respuesta<Categoria>>> CrearCategoria(Categoria categoria)
        {
            _context.Categorias?.Add(categoria);
            await _context.SaveChangesAsync();

            var respuesta = new Respuesta<Categoria>
            {
                Mensaje = "Categoría creada correctamente",
                Exitoso = true,
                Datos = categoria
            };

            return Ok(respuesta);
        }

        // PUT: api/Categoria/ActualizarCategoria/5
        [HttpPut("ActualizarCategoria/{id}")]
        public async Task<ActionResult<Respuesta<Categoria>>> ActualizarCategoria(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var respuesta = new Respuesta<Categoria>
            {
                Mensaje = "Categoría actualizada correctamente",
                Exitoso = true,
                Datos = categoria
            };

            return Ok(respuesta);
        }

        // DELETE: api/Categoria/EliminarCategoria/5
        [HttpDelete("EliminarCategoria/{id}")]
        public async Task<ActionResult<Respuesta<bool>>> EliminarCategoria(int id)
        {
            var categoria = await _context.Categorias!.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            var respuesta = new Respuesta<bool>
            {
                Mensaje = "Categoría eliminada correctamente",
                Exitoso = true,
                Datos = true
            };

            return Ok(respuesta);
        }

        // GET: api/Categoria/ObtenerCategoriaPorId/5
        [HttpGet("ObtenerCategoriaPorId/{id}")]
        public async Task<ActionResult<Respuesta<Categoria>>> ObtenerCategoriaPorId(int id)
        {
            var categoria = await _context.Categorias!.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            var respuesta = new Respuesta<Categoria>
            {
                Mensaje = "Categoría obtenida correctamente",
                Exitoso = true,
                Datos = categoria
            };

            return Ok(respuesta);
        }

        // GET: api/Categoria/ListarCategorias
        [HttpGet("ListarCategorias")]
        public async Task<ActionResult<Respuesta<IEnumerable<Categoria>>>> ListarCategorias()
        {
            var categorias = await _context.Categorias!.ToListAsync();

            var respuesta = new Respuesta<IEnumerable<Categoria>>
            {
                Mensaje = "Categorías listadas correctamente",
                Exitoso = true,
                Datos = categorias
            };

            return Ok(respuesta);
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias!.Any(e => e.CategoriaId == id);
        }
    }
}
