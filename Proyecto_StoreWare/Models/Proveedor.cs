using System.ComponentModel.DataAnnotations;

namespace Proyecto_StoreWare.Models
{
    public class Proveedor
    {
        [Key]
        public int Id { get; set; }

        // Optional relationship to Administrador
        public int? AdministradorId { get; set; }
        public Administrador? Administrador { get; set; }

        // Optional descriptive fields
        [MaxLength(100, ErrorMessage = "El nombre del proveedor no puede exceder los 100 caracteres.")]
        public string? Nombre { get; set; }

        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Debe ingresar un número de teléfono válido.")]
        public string? Telefono { get; set; }

        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}