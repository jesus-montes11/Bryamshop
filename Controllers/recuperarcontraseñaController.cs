using BRIAMSHOP.Models;
using BRIAMSHOP.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BRIAMSHOP.Controllers
{
    public class recuperarcontraseñaController : Controller
    {
        private readonly IRepositoriorecuperarcontraseña reporecuperar;
        public recuperarcontraseñaController(IRepositoriorecuperarcontraseña repocuperar)
        {
            this.reporecuperar = repocuperar;
        }
        public IActionResult nuevacontra(Registromodel nuevo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(nuevo);
                }
                reporecuperar.nuevacontra(nuevo);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return View();
        }
        public IActionResult recuperar()
        {
            return View("~/Views/recuperar/recuperar.cshtml");
        }
    }
}
