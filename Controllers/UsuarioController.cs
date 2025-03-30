using BRIAMSHOP.Models;
using BRIAMSHOP.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BRIAMSHOP.Controllers
{
    public class UsuarioController : Controller
    {
		private readonly IRepositorioUsuario repoUsuario;
		private readonly IRepositorioHome repositorioHome;
		public UsuarioController(IRepositorioUsuario repoUsuario , IRepositorioHome repositorioHome)
		{
			this.repoUsuario = repoUsuario;
			this.repositorioHome = repositorioHome;
		}
		[HttpPost]
		public async Task<IActionResult> ingresar(iniciarsesionmodel login)
		{
			ErrorViewModel viewModel = new ErrorViewModel();
			
			try
			{
				encriptar encripto = new encriptar();
				login.rcontrasena = encripto.Encrypt(login.rcontrasena);
				bool rsp = await repoUsuario.ValidarUsuario(login);

				if (rsp)
				{
					IEnumerable<insertarproductomodel> producto = repositorioHome.listarProductos();
					return View("~/Views/Home/carrusel.cshtml", producto);

				}
				return View("Index");
				
			}
			catch (Exception Error)
			{
				viewModel.RequestId=Error.HResult.ToString();
				viewModel.message = Error.HResult.ToString();
			}
			return View("Error",viewModel);



		}
		public IActionResult Index ()
		{
			return View();
		}


	}
}

