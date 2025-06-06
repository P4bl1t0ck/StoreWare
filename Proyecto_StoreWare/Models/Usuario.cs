using System.ComponentModel.DataAnnotations;

namespace Proyecto_StoreWare.Models
{
    public class Usuario
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(40,ErrorMessage ="Only names of 40 of length, any name more of 40 caracters is no recomended.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Contraseña { get; set; }
        [Required(ErrorMessage ="Direcion es nesecaria")]
        [MaxLength(100, ErrorMessage = "La dirección no puede exceder los 100 caracteres.")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El teléfono es Obligatorio")]
        [MaxLength(15, ErrorMessage = "El teléfono no puede exceder los 15 caracteres.")]
        public string Telefono { get; set; }

        public ICollection<Transaccion> Transacciones { get; set; }
    }
}
