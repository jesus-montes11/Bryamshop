using BRIAMSHOP.Models;
using BRIAMSHOP.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BRIAMSHOP.Controllers
{
    public class ProductoController : Controller
    {
        private readonly Irepositorioproducto repoProducto;

        public ProductoController(Irepositorioproducto repoProducto)
        {
            this.repoProducto = repoProducto;
        }
        public async Task<IActionResult> RegistroproductosAsync(insertarproductomodel productos)
        {
            try
            {
                if (productos.imagen != null && productos.imagen.Length > 0)
                {
                    var extension = Path.GetExtension(productos.imagen.FileName);
                    var nuevonombre = Guid.NewGuid().ToString() + extension;
                    var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/almacen", nuevonombre);
                    using (var stream = new FileStream(FilePath, FileMode.Create))
                    {
                        await productos.imagen.CopyToAsync(stream);

                    }
                    productos.urlimagen = "./almacen/" + nuevonombre;
                }
                repoProducto.insertarproductomodel(productos);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return View("~/Views/Home/Registroproductos.cshtml");



        }
        [HttpGet]
        public IActionResult Registroproductos()
        {
            return View("~/Views/Home/Registroproductos.cshtml");
        }
        [HttpGet]
        public string Mensaje()
        {
            return "Mensaje Back sino crees";
        }
       [HttpGet]
        public JsonResult infoproducto(int id)
        {
            insertarproductomodel detalle = repoProducto.infoproducto(id);
            return Json(detalle);
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerProductoPorCodigo(string codigo)
        {
            var producto = await repoProducto.ObtenerProductoPorCodigo(codigo);
            if (producto == null)
            { return NotFound(); }
            return Json(producto);
        }

    }
}
