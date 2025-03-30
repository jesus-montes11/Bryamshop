using BRIAMSHOP.Repositorio;
using Microsoft.AspNetCore.Mvc;
using BRIAMSHOP.Models;
using Microsoft.AspNetCore.Http; 

namespace BRIAMSHOP.Controllers
{
	public class CarritoController : Controller
	{
		private readonly IRepositoriocarrito _Repositoriocarrito;
		private readonly Irepositorioproducto _repositorioproducto;
		public CarritoController(IRepositoriocarrito repocarrito, Irepositorioproducto repoproducto)
		{
			_Repositoriocarrito = repocarrito;
			_repositorioproducto = repoproducto;
		}

		public IActionResult agregar(carritoModel productoId, int Cantidad)
		{
			
			if (productoId == null)
			{
				_Repositoriocarrito.agregar(productoId, Cantidad);
			}

			var carritoItems = _Repositoriocarrito.ListarItemscarro();
			return View("", carritoItems);
		}

		public IActionResult eliminar(int productoId)
		{
			_Repositoriocarrito.eliminarItemcarro(productoId);

			var carritoItems = _Repositoriocarrito.ListarItemscarro();
			return View("~/Views/carrito/carrito.cshtml", carritoItems);
		}

		public IActionResult actualizarItem(int productoId, int Cantidad)
		{
			if (Cantidad < 1)
			{
				return BadRequest("La cantidad debe ser al menos 1.");
			}
			_Repositoriocarrito.actualizarItemcarro(productoId, Cantidad);
			var carritoItems = _Repositoriocarrito.ListarItemscarro();
			return View("~/Views/carrito/carrito.cshtml", carritoItems);



		}
		public IActionResult carrito (int productoId,int cantidad)
		{
			var conectar= _repositorioproducto.agregar(productoId,cantidad);
			if (conectar != null)
			{
				_Repositoriocarrito.agregar(conectar, cantidad);
			}
			var carritoitems = _Repositoriocarrito.ListarItemscarro();
			return View(carritoitems);
		}

	}
}
