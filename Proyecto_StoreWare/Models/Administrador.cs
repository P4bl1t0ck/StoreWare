using System.ComponentModel.DataAnnotations;

namespace Proyecto_StoreWare.Models
{
    public class Administrador
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(40)]
        public string Nombre { get; set; }
        [MaxLength(40)]
        //Nuevo que encontre en internet
        [Required(ErrorMessage ="Access Denied, Email Address Required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]//Copi Help.
        public string Email { get; set; }
        [Required(ErrorMessage = "Access Denied, Password Required.")]
        [StringLength(100,ErrorMessage ="Something a little hard the next time,")]
        public string Contraseña { get; set; }
        

    }
}
