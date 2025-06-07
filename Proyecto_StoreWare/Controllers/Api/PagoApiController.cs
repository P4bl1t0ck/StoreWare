using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_StoreWare.Models;

namespace Proyecto_StoreWare.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoApiController : ControllerBase
    {
        private readonly StoreWare _context;

        public PagoApiController(StoreWare context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Agregar")]
        public async Task<ActionResult<IEnumerable<Pago>>> GetPago()
        {
            return await _context.Pago.ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<Pago>> GetPago(int id)
        {
            var pago = await _context.Pago.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }
            return pago;
        }
        // POST: Administradors
        [HttpPost]
        [Route("Ver")]
        public async Task<ActionResult<Pago>> PostPago(Pago pago)
        {
            _context.Pago.Add(pago);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPago), new { id = pago.Id }, pago);
        }
        // PUT: Administradors/5
        [HttpPut]
        [Route("Agregar")]
        public async Task<IActionResult> PutPago(int id, Pago pago)
        {
            if (id != pago.Id)
            {
                return BadRequest();
            }
            _context.Entry(pago).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(pago.Id))
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
        // DELETE: Administradors/5
        private bool PagoExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
        [HttpDelete]
        [Route("Borrar")]
        public async Task<IActionResult> DeletePago(int id)
        {
            var pago = await _context.Pago.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }
            _context.Pago.Remove(pago);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
