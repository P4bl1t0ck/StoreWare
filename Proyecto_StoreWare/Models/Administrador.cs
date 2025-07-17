using System.ComponentModel.DataAnnotations;

namespace Proyecto_StoreWare.Models
{
    public class Administrador
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(40)]
        public string? Nombre { get; set; } // Hacer nullable

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; } // Hacer nullable

        [MaxLength(100)]
        public string? Contraseña { get; set; } // Hacer nullable

        public bool Activo { get; set; } = true; // Valor por defecto
    }
}