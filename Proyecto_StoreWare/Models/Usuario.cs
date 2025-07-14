using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Proyecto_StoreWare.Models
{
    [Table("Usuarios")] // Personaliza nombre de tabla
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment en SQLite
        public int Id { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "Solo se permiten nombres de hasta 40 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mínimo 6 caracteres")]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "Dirección requerida")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Teléfono obligatorio")]
        [MaxLength(15, ErrorMessage = "Máximo 15 caracteres")]
        [Column(TypeName = "varchar(15)")] // Optimizado para SQLite
        public string Telefono { get; set; }

        // Relación con Transacciones (CORREGIDO)
        public ICollection<Transaccion> Transacciones { get; set; } = new HashSet<Transaccion>();
    }
}