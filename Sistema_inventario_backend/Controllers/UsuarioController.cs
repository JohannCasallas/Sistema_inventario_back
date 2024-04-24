using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Sistema_inventario_backend.Models;
using Sistema_inventario_backend.Data;

namespace Sistema_inventario_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public UsuarioController(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // Método para registrar un nuevo usuario
        [HttpPost("RegistrarUsuario")]
        public async Task<ActionResult<Respuesta<Usuario>>> RegistrarUsuario(Usuario usuario)
        {
            try
            {
                _context.Usuario!.Add(usuario);
                await _context.SaveChangesAsync();
                return Ok(new Respuesta<Usuario> { Exitoso = true, Mensaje = "Usuario registrado exitosamente", Datos = usuario });
            }
            catch (Exception ex)
            {
                return BadRequest(new Respuesta<Usuario> { Exitoso = false, Mensaje = "Error al registrar el usuario", Datos = null });
            }
        }

        // Método para actualizar un usuario existente
        [HttpPut("ActualizarUsuario/{id}")]
        public async Task<ActionResult<Respuesta<Usuario>>> ActualizarUsuario(int id, Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return BadRequest(new Respuesta<Usuario> { Exitoso = false, Mensaje = "IDs de usuario no coinciden", Datos = null });
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new Respuesta<Usuario> { Exitoso = true, Mensaje = "Usuario actualizado exitosamente", Datos = usuario });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound(new Respuesta<Usuario> { Exitoso = false, Mensaje = "Usuario no encontrado", Datos = null });
                }
                else
                {
                    throw;
                }
            }
        }

        // Método para eliminar un usuario
        [HttpDelete("EliminarUsuario/{id}")]
        public async Task<ActionResult<Respuesta<Usuario>>> EliminarUsuario(int id)
        {
            var usuario = await _context.Usuario!.FindAsync(id);
            if (usuario == null)
            {
                return NotFound(new Respuesta<Usuario> { Exitoso = false, Mensaje = "Usuario no encontrado", Datos = null });
            }

            _context.Usuario!.Remove(usuario);
            await _context.SaveChangesAsync();

            return Ok(new Respuesta<Usuario> { Exitoso = true, Mensaje = "Usuario eliminado exitosamente", Datos = usuario });
        }

        // Método para autenticar un usuario y generar token JWT
        [HttpPost("usuarioAutenticacion")]
        public async Task<ActionResult<Respuesta<string>>> usuarioAutenticacion(Usuario model)
        {
            var usuario = await _context.Usuario!.SingleOrDefaultAsync(u => u.CorreoElectronico == model.CorreoElectronico && u.Contrasena == model.Contrasena);
            if (usuario == null)
            {
                return BadRequest(new Respuesta<string> { Exitoso = false, Mensaje = "Credenciales inválidas", Datos = null });
            }

            var token = GenerateJwtToken(usuario);

            return Ok(new Respuesta<string> { Exitoso = true, Mensaje = "Inicio de sesión exitoso", Datos = token });
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario!),
                    new Claim(ClaimTypes.Role, usuario.Rol!)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    // Método para listar todos los usuarios
    [HttpGet("ListarUsuarios")]
        public async Task<ActionResult<Respuesta<List<Usuario>>>> ListarUsuarios()
        {
            var usuarios = await _context.Usuario!.ToListAsync();
            return Ok(new Respuesta<List<Usuario>> { Exitoso = true, Mensaje = "Lista de usuarios obtenida correctamente", Datos = usuarios });
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario!.Any(e => e.UsuarioId == id);
        }
    }
}
