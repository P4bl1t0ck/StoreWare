using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_StoreWare.Models;

namespace Proyecto_StoreWare.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionesApiController : ControllerBase
    {
        private readonly StoreWare _context;

        public TransaccionesApiController(StoreWare context)
        {
            _context = context;
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Transaccion>>> GetTransaccionesByUsuario(int usuarioId)
        {
            var transacciones = await _context.Transaccion
                .Where(t => t.UsuarioId == usuarioId)
                .Include(t => t.Pago)
                .Include(t => t.Producto)
                .ToListAsync();

            return Ok(transacciones);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaccion>>> GetAllTransacciones()
        {
            return await _context.Transaccion
                .Include(t => t.Pago)
                .Include(t => t.Producto)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaccion>> GetTransaccion(int id)
        {
            var transaccion = await _context.Transaccion
                .Include(t => t.Pago)
                .Include(t => t.Producto)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaccion == null)
            {
                return NotFound();
            }

            return transaccion;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaccionById(int id)
        {
            var transaccion = await _context.Transaccion.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }

            _context.Transaccion.Remove(transaccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
