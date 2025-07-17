using System.ComponentModel.DataAnnotations;

namespace Proyecto_StoreWare.Models
{
    public class Pago
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Nombre { get; set; } // Hacer nullable

        [MaxLength(50)]
        public string? Tipo { get; set; } // Hacer nullable

        public bool Activo { get; set; } = true; // Valor por defecto
        public ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();

    }
}