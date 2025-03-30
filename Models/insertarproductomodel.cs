using System.ComponentModel.DataAnnotations;

namespace BRIAMSHOP.Models
{
    public class insertarproductomodel
    {
        public string codigo {  get; set; }

        public string  nombre { get; set; }
        public string descripcion { get; set; }
         
        public string preciov { get; set; }

        public string unidades { get; set; }

        public string estado { get; set; }
         
        [DataType(DataType.Upload)]
        public IFormFile imagen { get; set; }

        public string urlimagen { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string color { get; set; }
        
        
        
    }
}
