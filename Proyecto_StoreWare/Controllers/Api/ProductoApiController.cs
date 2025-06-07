using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_StoreWare.Models;

namespace Proyecto_StoreWare.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoApiController : ControllerBase
    {
        private readonly StoreWare _context;

        public ProductoApiController(StoreWare context)
        {
            _context = context;
        }


        //Starts again.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetandSearchProductos(int id)
        {
            if (id == 0)
            {
                return await _context.Producto.Include(p => p.Proveedor).ToListAsync();
            }
            else
            {
                var producto = await _context.Producto.Include(p => p.Proveedor).FirstOrDefaultAsync(m => m.Id == id);
                if (producto == null)
                {
                    return NotFound();
                }
                return new List<Producto> { producto };//Retorna un listado con un solo producto, el que se busca por id.
            }
        }
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Producto.Add(producto);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetandSearchProductos), new { id = producto.Id }, producto);
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                _context.Entry(producto).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            return BadRequest(ModelState);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool ProductoExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
    }
}
