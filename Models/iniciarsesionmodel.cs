using System.ComponentModel.DataAnnotations;

namespace BRIAMSHOP.Models
{
    public class iniciarsesionmodel
    {
        [Required(ErrorMessage ="el campo usuario es requerido")]
        public string firstName { get;   set; }

        [Required(ErrorMessage ="el campo contraseña es requerido")]
        public string rcontrasena { get; set; }
    }
}
    