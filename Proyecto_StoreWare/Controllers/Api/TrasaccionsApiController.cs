using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_StoreWare.Models;

namespace Proyecto_StoreWare.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrasaccionsApiController : ControllerBase
    {
        private readonly StoreWare _context;

        public TrasaccionsApiController(StoreWare context)
        {
            _context = context;
        }
        
        //Here we should create a method to search for transactions by user, product, or date.
        //We just need to show our transactions and delete them if necessary.
        //So we will not create a search method for now, but we can add it later if needed.
        //Method to show all transactions.
        [HttpGet("usuario/{usaarioId}")]//consiue el usuario por id. desde la url.
        public async Task<ActionResult<IEnumerable<Transaccion>>> GetTransaccionsByUsuario(int usuarioId)
        {
            var transacciones = await _context.Transaccion
                .Where(t => t.UsuarioId == usuarioId)
                .Include(t => t.Pago)
                .Include(t => t.Producto)
                .ToListAsync();
            return Ok(transacciones);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaccion>>> GetTransaccions()
        {
            return await _context.Transaccion.ToListAsync();
        }
        //Method to get a specific transaction by id.
        [HttpGet]
        public async Task<ActionResult<Transaccion>> GetTransaccion(int id)
        {
            var transaccion = await _context.Transaccion.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }
            return transaccion;
        }
        //Method for delete transaction by id.
        [HttpDelete]
        public async Task<ActionResult<Transaccion>> DeleteTransaccion(int id)
        {
            var transaccion = await _context.Transaccion.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }
            else
            {
                _context.Transaccion.Remove(transaccion);
                await _context.SaveChangesAsync();
                return transaccion;
            }

            //return null;
        }
    }
}
