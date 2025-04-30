using System.ComponentModel.DataAnnotations;

namespace Proyecto_StoreWare.Models
{
    public class Proveedor
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Required(ErrorMessage = "El nombre del proveedor es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre del proveedor no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "Debe ingresar un número de teléfono válido.")]
        public string Telefono { get; set; }
  
    }
}
