using System.ComponentModel.DataAnnotations;


namespace Proyecto_StoreWare.Models
{
    public class Pago
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Required(ErrorMessage = "El nombre del método de pago es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre del método de pago no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El tipo de pago es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El tipo de pago no puede exceder los 50 caracteres.")]
        public string Tipo { get; set; }
        public ICollection<Transaccion> Transacciones { get; set; }
    }
}
