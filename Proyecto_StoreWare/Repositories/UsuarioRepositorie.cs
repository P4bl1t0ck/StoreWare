using Microsoft.EntityFrameworkCore;
using Proyecto_StoreWare.Interfaces;
using Proyecto_StoreWare.Models;

namespace Proyecto_StoreWare.Repositories
{
    public class UsuarioRepositorie : IUsuarioService
    {
        private readonly StoreWareSQLite _context;

        public UsuarioRepositorie(StoreWareSQLite context)
        {
            _context = context;
        }

        public async Task<bool> RegisterAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Usuario?> LoginAsync(string email, string contraseña)
            => await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Contraseña == contraseña);

        public async Task<Usuario?> GetUsuarioByIdAsync(int id)
        => await _context.Usuarios.FindAsync(id);

        public async Task<bool> UpdatePerfilAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            var usuario = await GetUsuarioByIdAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
