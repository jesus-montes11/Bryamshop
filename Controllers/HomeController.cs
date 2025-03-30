using BRIAMSHOP.Models;
using BRIAMSHOP.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using System.Diagnostics;

namespace BRIAMSHOP.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositorioUsuario repousuario;
        private readonly IRepositorioHome repositorioHome;
        public HomeController(IRepositorioUsuario Repousuario, IRepositorioHome repoH)
        {
            this.repousuario = Repousuario;
            this.repositorioHome = repoH;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult login()
        {
            return View(Index);
        }
       /* public IActionResult carrusel()
        {
            return View();
        }*/

        public IActionResult Menu()
        {
            return View();
        }
        public IActionResult registrar(Registromodel usuario)
        {
            try
            {
                // if (!ModelState.IsValid)
                // {
                //   return View(usuario);
                // }

                encriptar encripto = new encriptar();
                usuario.rcontrasena= encripto.Encrypt(usuario.rcontrasena);
                repousuario.Registromodel(usuario);

                return View();
            }
            catch (Exception ex)
            {

            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult carrusel()
        {
            IEnumerable<insertarproductomodel> producto=repositorioHome.listarProductos();
            return View( producto);
        }
        public IActionResult compras()
        {
            return View("~/Views/Home/compras.cshtml");
        }

        

    }

   
}
