using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_StoreWare.Models
{
    public class Transaccion
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        [Required]
        [ForeignKey("Producto")]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        [Required]
        [ForeignKey("Pago")]
        public int PagoId { get; set; }    
        public Pago Pago { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1,999, ErrorMessage = "La cantidad debe ser al menos 1.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage ="Ingrese la fecha del mismo dia de la transaccion.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El estado no puede tener más de 20 caracteres.")]
        public string Estado { get; set; }
    }
}
