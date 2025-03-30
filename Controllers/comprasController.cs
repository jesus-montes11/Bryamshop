using BRIAMSHOP.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BRIAMSHOP.Controllers
{
	public class comprasController : Controller
	{
		private readonly IRepositoriocompras repoCompras;
		public comprasController(IRepositoriocompras repositoriocompras)
		{
			this.repoCompras= repositoriocompras;
		}
		[HttpGet]
		public async Task<IActionResult> obtenercompra(string codigo)
		{
			var producto=await repoCompras.obtenercompra(codigo);
			if (producto == null)
			{
				return NotFound();
			}
			return Json(producto);
		}
		public IActionResult compras ()
		{
			return View("~/Views/Home/compras.cshtml");
		}
	}
}
