using Microsoft.AspNetCore.Mvc;
using Proyecto_StoreWare.Interfaces;
using Proyecto_StoreWare.Models;

namespace Proyecto_StoreWare.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoApiController : ControllerBase
    {
        private readonly IProductoService _productoService;

        
        public ProductoApiController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _productoService.GetAllProductosAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _productoService.GetProductoByIdAsync(id);
            return producto != null ? Ok(producto) : NotFound();
        }

        [HttpGet("categoria/{categoria}")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetByCategoria(string categoria)
        {
            var productos = await _productoService.GetProductosByCategoriaAsync(categoria);
            return Ok(productos);
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productoService.AddProductoAsync(producto);
            return result ? CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto) : BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id || !ModelState.IsValid)
                return BadRequest();

            var existe = await _productoService.GetProductoByIdAsync(id);
            if (existe == null)
                return NotFound();

            var result = await _productoService.UpdateProductoAsync(producto);
            return result ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductoById(int id)
        {
            var result = await _productoService.DeleteProductoAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Producto>>> SearchProductos([FromQuery] string keyword)
        {
            var productos = await _productoService.SearchProductosAsync(keyword);
            return Ok(productos);
        }
    }
}