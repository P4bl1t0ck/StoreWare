using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_StoreWare.Models;

namespace Proyecto_StoreWare.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorApiController : ControllerBase
    {
        private readonly StoreWare _context;

        public ProveedorApiController(StoreWare context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedors()
        {
            return await _context.Proveedor.Include(p => p.Administrador).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<Proveedor>> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedor.Include(p => p.Administrador).FirstOrDefaultAsync(m => m.Id == id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return proveedor;
        }
        //This controller doesn´t need those methods that are not necessary for the Proveedor entity.
        //As Transaccion, just needs Get ,search and delete methods.

        /*
        [HttpDelete]
        public async Task<ActionResult<Proveedor>> DeleteProveedor(int id)
        {
            var proveedor = await _context.Proveedor.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            _context.Proveedor.Remove(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }
        */
        //Bad idea if we delete a provider, we will lose all the products associated with it. 
        //I guess we can just disable the provider and not delete it.
        // GET: Proveedors
    }
}
