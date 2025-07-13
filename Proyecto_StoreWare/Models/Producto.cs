using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_StoreWare.Models
{
    [Table("Productos")] // Explicitamos el nombre de la tabla
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Para autoincrement en SQLite
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(500)]
        public string Descripcion { get; set; }

        // SQLite no soporta "Precision(18,2)" directamente. Alternativas:
        [Required]
        [Range(1.00, 100000.00)]
        [Column(TypeName = "decimal(18,2)")] // Conversión implícita en SQLite
        public decimal Precio { get; set; }

        [Required]
        [Range(0, 999)]
        public int Stock { get; set; }

        [Required]
        [MaxLength(50)]
        public string Categoria { get; set; }

        // Relación con Proveedor (ajustada para SQLite)
        [Required]
        [ForeignKey("ProveedorId")]
        public int ProveedorId { get; set; }

        public Proveedor Proveedor { get; set; }

        // Colección de Transacciones (relación inversa)
        public ICollection<Transaccion> Transacciones { get; set; } = new HashSet<Transaccion>();
    }
}