using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_StoreWare.Models
{
    [Table("Transacciones")]
    [Index(nameof(UsuarioId))]
    [Index(nameof(ProductoId))]
    public class Transaccion
    {
        [Key]
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public int ProductoId { get; set; }
        public int PagoId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }

        [ForeignKey("PagoId")]
        public Pago Pago { get; set; }

        [Required]
        [Range(1, 999)]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "TEXT")]
        public DateTime Fecha { get; set; }

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; }
    }
}
