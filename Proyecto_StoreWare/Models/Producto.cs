using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_StoreWare.Models
{
    public class Producto
    {

        [Key]
        public int Id { get; set; } // Primary Key

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre del producto no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [MaxLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(1.00, 100000.00, ErrorMessage = "El precio debe ser mayor a 0.")]
        [Precision(18, 2)] // Este codigo me ayudo Copi tuve unos problemas al restablecer las migraciones
        //Este atributi de data annottation funciona dando la precision decimal a los numeros desde 18 digitos y hasta 2 puntos decimales.
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Range(0, 999, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        [MaxLength(50, ErrorMessage = "La categoría no puede exceder los 50 caracteres.")]
        public string Categoria { get; set; }

        [Required]
        [ForeignKey("Proveedor")]
        public int ProveedorId { get; set; } // Foreign Key

        // Relación de navegación
        public virtual Proveedor Proveedor { get; set; }
    }
}
