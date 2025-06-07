using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_StoreWare.Models;

namespace Proyecto_StoreWare.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosApiController : ControllerBase
    {
        private readonly StoreWare _context;
        //Nuestra base de datos se llama StoreWare y es la que contiene la entidad Usuario.

        public UsuariosApiController(StoreWare context)
        {
            _context = context;
        }
        // GET: api/Usuarios
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            //var usuarios = await _context.Usuario.ToListAsync();
            return await _context.Usuario.ToListAsync();
        }
        // GET: api/Usuarios/5
        [HttpGet]
        [Route("Buscar")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }
        // POST: api/Usuarios
        [HttpPost]
        [Route("Agregar")]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }
        // PUT: api/Usuarios/5
        [HttpPut]
        [Route("Agregar")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }
            _context.Entry(usuario).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
        // DELETE: api/Usuarios/5
        [HttpDelete]
        [Route("Borrar")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
