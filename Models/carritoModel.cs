namespace BRIAMSHOP.Models
{
	public class carritoModel
	{
		public int codigo { get; set; }
		public string Nombre { get; set; }
		public decimal preciov {  get; set; }
		public string Descripcion {  get; set; }
		public string urlimagen { get; set; }
	}
	public class carroitem
	{
		public carritoModel Producto { get; set; }
		public int cantidad { get; set; }
	}
	public class productoSeleccionados
	{
		public List<carroitem> items { get; set; } = new List<carroitem>();
		public decimal Totalprecio => items.Sum(item => item.Producto.preciov * item.cantidad);
	}
}
