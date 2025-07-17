using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_StoreWare.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(40)]
        public string? Nombre { get; set; } // Hacer nullable

        [EmailAddress]
        public string? Email { get; set; } // Hacer nullable

        [MaxLength(100)]
        public string? Contraseña { get; set; } // Hacer nullable

        [MaxLength(100)]
        public string? Direccion { get; set; } // Hacer nullable

        [MaxLength(15)]
        public string? Telefono { get; set; } // Hacer nullable
        public ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();

    }
}