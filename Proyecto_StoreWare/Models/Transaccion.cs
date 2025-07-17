using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_StoreWare.Models
{
    [Table("Transacciones")]
    public class Transaccion
    {
        [Key]
        public int Id { get; set; }

        public int? UsuarioId { get; set; } // Hacer nullable
        public int? ProductoId { get; set; } // Hacer nullable
        public int? PagoId { get; set; } // Hacer nullable

        [ForeignKey("UsuarioId")]
        public virtual Usuario? Usuario { get; set; }

        [ForeignKey("ProductoId")]
        public virtual Producto? Producto { get; set; }

        [ForeignKey("PagoId")]
        public virtual Pago? Pago { get; set; }

        public int? Cantidad { get; set; } // Hacer nullable

        public DateTime? Fecha { get; set; } // Hacer nullable

        [MaxLength(20)]
        public string? Estado { get; set; } // Hacer nullable
    }
}