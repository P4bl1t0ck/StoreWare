using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_StoreWare.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Nombre { get; set; } // Hacer nullable

        [MaxLength(500)]
        public string? Descripcion { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Precio { get; set; } // Hacer nullable

        public int? Stock { get; set; } // Hacer nullable

        [MaxLength(50)]
        public string? Categoria { get; set; }

        public int? ProveedorId { get; set; } // Hacer nullable

        [ForeignKey("ProveedorId")]
        public virtual Proveedor? Proveedor { get; set; } // Hacer nullable

        public int? AdminId { get; set; } // Hacer nullable

        [ForeignKey("AdminId")]
        public virtual Administrador? Admin { get; set; } // Hacer nullable

        public bool Activo { get; set; } = true; // Valor por defecto
        public virtual ICollection<Transaccion>? Transaccion { get; set; }
    }
}