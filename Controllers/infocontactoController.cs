using BRIAMSHOP.Models;
using BRIAMSHOP.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BRIAMSHOP.Controllers
{
    public class infocontactoController : Controller
    {
        private readonly IRepositoriocontactanos repoContactanos;

        public infocontactoController(IRepositoriocontactanos repoContactanos)
        {
            this.repoContactanos = repoContactanos;
        }

        public IActionResult Contactanos(infocontactoModel contacto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(contacto);
                }
                repoContactanos.infocontactoModel(contacto);
            }
            catch (Exception ex)
            {

            }
            return View();

        }
        public IActionResult RecuperarContraseña()
        {
            return View();
        }
    }
}
