using Proyecto_StoreWare.Models;

namespace Proyecto_StoreWare.Interfaces
{
    public interface IProductoService
    {
        void Init(); // Inicializa la BD local (SQLite)

        // CRUD Productos
        Task<List<Producto>> GetAllProductosAsync();
        Task<List<Producto>> GetProductosByCategoriaAsync(string categoria); // Licencias/Hardware
        Task<Producto?> GetProductoByIdAsync(int id);
        /*Administrrador*/
        Task<bool> AddProductoAsync(Producto producto);
        Task<bool> UpdateProductoAsync(Producto producto);
        Task<bool> DeleteProductoAsync(int id);

        // Métodos específicos para tienda
        Task<List<Producto>> GetProductosEnOfertaAsync();
        Task<List<Producto>> SearchProductosAsync(string keyword);
    }
}