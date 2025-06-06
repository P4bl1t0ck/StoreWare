using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_StoreWare.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(500)]
        public string Descripcion { get; set; }

        [Required]
        [Range(1.00, 100000.00)]
        [Precision(18, 2)]
        public decimal Precio { get; set; }

        [Required]
        [Range(0, 999)]
        public int Stock { get; set; }

        [Required]
        [MaxLength(50)]
        public string Categoria { get; set; }

        [Required]
        [ForeignKey("Proveedor")]
        public int ProveedorId { get; set; }

        public Proveedor Proveedor { get; set; }

        public ICollection<Transaccion> Transacciones { get; set; }

        public Producto()
        {
            Transacciones = new HashSet<Transaccion>();
        }
    }
}
