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
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetAllProveedors()
        {
            return await _context.Proveedor.Include(p => p.Administrador).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> GetProveedorById(int id)
        {
            var proveedor = await _context.Proveedor.Include(p => p.Administrador).FirstOrDefaultAsync(m => m.Id == id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return proveedor;
        }
    }
}
