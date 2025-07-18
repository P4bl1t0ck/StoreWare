using Microsoft.EntityFrameworkCore;
using Proyecto_StoreWare.Interfaces;
using Proyecto_StoreWare.Models;
using Proyecto_StoreWare.Interfaces;
namespace Proyecto_StoreWare.Data.Repositories
{
    public class PagoRepository : IVentaService
    {
        private readonly StoreWareSQLite _context;

        public PagoRepository(StoreWareSQLite context)
        {
            _context = context;
        }

        public async Task<List<Pago>> GetAllPagosAsync()
            => await _context.Pagos.Include(p => p.Transacciones).ToListAsync();

        public async Task<Pago?> GetPagoByIdAsync(int id)
            => await _context.Pagos
                .Include(p => p.Transacciones)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<bool> CreatePagoAsync(Pago pago)
        {
            await _context.Pagos.AddAsync(pago);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePagoAsync(Pago pago)
        {
            _context.Pagos.Update(pago);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePagoAsync(int id)
        {
            var pago = await GetPagoByIdAsync(id);
            if (pago == null) return false;

            _context.Pagos.Remove(pago);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Pago>> GetPagosByUsuarioAsync(int usuarioId)
            => await _context.Pagos
                .Where(p => p.Transacciones.Any(t => t.UsuarioId == usuarioId))
                .ToListAsync();

        public Task<bool> AddToCartAsync(Transaccion item)
        {
            throw new NotImplementedException();
        }

        public Task<List<Transaccion>> GetCarritoAsync(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckoutAsync(int usuarioId, Pago metodoPago)
        {
            throw new NotImplementedException();
        }

        public Task<List<Transaccion>> GetHistorialComprasAsync(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Transaccion>> GetAllVentasAsync()
        {
            throw new NotImplementedException();
        }
    }
}