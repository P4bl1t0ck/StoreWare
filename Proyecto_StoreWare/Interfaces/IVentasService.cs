using Proyecto_StoreWare.Models;

namespace Proyecto_StoreWare.Interfaces
{
    public interface IVentaService
    {
        // Carrito y Transacciones
        Task<bool> AddToCartAsync(Transaccion item); // Guarda en SQLite
        Task<List<Transaccion>> GetCarritoAsync(int usuarioId);
        Task<bool> CheckoutAsync(int usuarioId, Pago metodoPago); // Confirma compra

        // Historial
        Task<List<Transaccion>> GetHistorialComprasAsync(int usuarioId);

        // Admin
        Task<List<Transaccion>> GetAllVentasAsync();
    }
}