using System.ComponentModel.DataAnnotations;


namespace Proyecto_StoreWare.Models
{
    public class Proveedor
    {
        [Key]
        public int Id { get; set; } // Primary Key

        //Foreign Key to Administrador
        public int AdministradorId { get; set; } // Foreign Key de Administrador
        public Administrador Administrador { get; set; } // Porpiedad de navegación

        [Required(ErrorMessage = "El nombre del proveedor es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre del proveedor no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "Debe ingresar un número de teléfono válido.")]
        public string Telefono { get; set; }
         public ICollection<Producto> Productos { get; set; } = new List<Producto>();
        //COnsigue la lsita de prouctos que tiene el proveedor.
    }
}
