using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_StoreWare.Models;

namespace Proyecto_StoreWare.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorsApiController : ControllerBase
    {
        private readonly StoreWare _context;

        public AdministradorsApiController(StoreWare context)
        {
            _context = context;
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Administrador>>> GetAdministradors()
        {
            return await _context.Administrador.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Administrador>> GetUsuario(int id)
        {
            var administrador = await _context.Administrador.FindAsync(id);
            if (administrador == null)
            {
                return NotFound();
            }
            return administrador;
        }
        // POST: Administradors
        [HttpPost]
        public async Task<ActionResult<Administrador>> PostAdministrador(Administrador administrador)
        {
            _context.Administrador.Add(administrador);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsuario), new { id = administrador.Id }, administrador);
        }
        // PUT: Administradors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministrador(int id, Administrador administrador)
        {
            if (id != administrador.Id)
            {
                return BadRequest();
            }
            _context.Entry(administrador).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministradorExists(administrador.Id))
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
        private bool AdministradorExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
        // DELETE: Administradors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrador(int id)
        {
            var administrador = await _context.Administrador.FindAsync(id);
            if (administrador == null)
            {
                return NotFound();
            }
            _context.Administrador.Remove(administrador);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
