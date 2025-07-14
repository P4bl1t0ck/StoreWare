using Microsoft.EntityFrameworkCore;
using Proyecto_StoreWare.Interfaces;
using Proyecto_StoreWare.Models;

namespace Proyecto_StoreWare.Data.Repositories
{
    public class ProductoRepository : IProductoService
    {
        private readonly StoreWareSQLite _context;

        public ProductoRepository(StoreWareSQLite context)
        {
            _context = context;
        }

        public void Init() => _context.Database.EnsureCreated();

        public async Task<List<Producto>> GetAllProductosAsync()
            => await _context.Productos.ToListAsync();

        public async Task<List<Producto>> GetProductosByCategoriaAsync(string categoria)
            => await _context.Productos.Where(p => p.Categoria == categoria).ToListAsync();

        public async Task<bool> AddProductoAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<Producto?> GetProductoByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateProductoAsync(Producto producto)
        {
            //Actualiza un producto existente, y retorna true si se actualizó correctamente
            _context.Productos.Update(producto);
            //De forma asíncrona, guarda los cambios en la base de datos
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductoAsync(int id)
        {
            var producto = _context.Productos.Find(id);
            //Dentro de var encuentra el producto por su id
            //Si no se encuentra el producto, retorna null la funcion de Find
            if (producto == null)
            {
                //Si es null, este producto no existe, por lo que no se puede eliminar
                return false;
                //No existe por lo tno no se puede eliminar
            }
            _context.Productos.Remove(producto);
            //En caso contrario, si el producto existe, lo elimina de la base de datos
            //Actualiza los cambios en la base de datos
            //Y con el >0, verifica si SaveChangesAsync() dio 1, en caso de si True
            //Porque (_context.SaveChangesAsync() = 1 ó 0) > 0 
            // Si SaveChangesAsync() devuelve 1, significa que se eliminó correctamente
            // Si devuelve 0, significa que no se eliminó nada (posiblemente porque el producto ya no existía)
            //Entonces retorna false si no se eliminó nada, o true si se eliminó correctamente
            return await _context.SaveChangesAsync() > 0; 
 
        }

        //Este metodo de lamda , obtiene todos los productos que están en oferta
        public async Task<List<Producto>> GetProductosEnOfertaAsync() => await _context.Productos.Where(p => p.Precio < 100).ToListAsync();
        //COmo ?, sabra dios, y el que escribio el codigo jajajaj

        //Creamos un metodo que permita el buscar n producto por su nombre
        public async Task<List<Producto>> BuscarProductosAsync(string nombre) => await _context.Productos
                .Where(p => p.Nombre.Contains(nombre) || p.Descripcion.Contains(nombre))
                .ToListAsync();
    }
}