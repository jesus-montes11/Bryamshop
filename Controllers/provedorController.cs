using BRIAMSHOP.Models;
using BRIAMSHOP.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BRIAMSHOP.Controllers
{
    public class provedorController : Controller
    {
        private readonly IRepositorioprovedor repoProvedor;
         
        public provedorController(IRepositorioprovedor repoProvedor)
        {
            this.repoProvedor = repoProvedor;
        }
        public IActionResult proveedor(provedorModel provedor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("~/Views/Home/proveedor.cshtml");
                }
                repoProvedor.provedorModel(provedor);


            }
            catch (Exception ex)
            {
                
            }
            return View("~/Views/Home/proveedor.cshtml");
        }
		public IActionResult provedor()
		{
			return View("~/Views/Home/proveedor.cshtml");



		}
	}
}
