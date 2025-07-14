using Proyecto_StoreWare.Models;

namespace Proyecto_StoreWare.Interfaces
{
    public interface IUsuarioService
    {
        Task<bool> RegisterAsync(Usuario usuario);
        Task<Usuario?> LoginAsync(string email, string contraseña);
        Task<Usuario?> GetUsuarioByIdAsync(int id);
        Task<bool> UpdatePerfilAsync(Usuario usuario);
        Task<bool> DeleteUsuarioAsync(int id);
    }
}