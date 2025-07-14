using Microsoft.EntityFrameworkCore;
using Proyecto_StoreWare.Interfaces;
using Proyecto_StoreWare.Models;

namespace Proyecto_StoreWare.Repositories
{
    public class VentaRepositorie : IVentaService
    {
        private readonly StoreWareSQLite _context;

        public VentaRepositorie(StoreWareSQLite context)
        {
            _context = context;
        }

        public async Task<bool> AddToCartAsync(Transaccion item)
        {
            item.Fecha = DateTime.Now;
            item.Estado = "Pendiente";
            await _context.Transacciones.AddAsync(item);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Transaccion>> GetCarritoAsync(int usuarioId)
            => await _context.Transacciones
                .Where(t => t.UsuarioId == usuarioId && t.Estado == "Pendiente")
                .Include(t => t.Producto)
                .Include(t => t.Pago)
                .ToListAsync();

        public async Task<bool> CheckoutAsync(int usuarioId, Pago metodoPago)
        {
            var carrito = await GetCarritoAsync(usuarioId);
            if (!carrito.Any()) return false;

            // Guardar el método de pago
            await _context.Pagos.AddAsync(metodoPago);
            await _context.SaveChangesAsync();

            // Actualizar transacciones
            foreach (var item in carrito)
            {
                item.PagoId = metodoPago.Id;
                item.Estado = "Completado";
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Transaccion>> GetHistorialComprasAsync(int usuarioId)
            => await _context.Transacciones
                .Where(t => t.UsuarioId == usuarioId && t.Estado == "Completado")
                .Include(t => t.Producto)
                .ToListAsync();

        public async Task<List<Transaccion>> GetAllVentasAsync()
            => await _context.Transacciones
                .Where(t => t.Estado == "Completado")
                .Include(t => t.Usuario)
                .Include(t => t.Producto)
                .ToListAsync();
    }
}
