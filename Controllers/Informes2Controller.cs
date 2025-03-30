using BRIAMSHOP.Models;
using BRIAMSHOP.Repositorio.footer;
using BRIAMSHOP.Repositorio;
using iText.Kernel.Events;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;
using iText.Layout;

namespace BRIAMSHOP.Controllers
{
    public class Informes2Controller : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly Irepositorioproducto _RepoProducto;
        public Informes2Controller(IConfiguration configuration, Irepositorioproducto RepoProducto)
        {
            _configuration = configuration;
            _RepoProducto = RepoProducto;
        }

        public IActionResult index()
        {
            return View();
        }

        public IActionResult pdfv()
        {
            return View();
        }
        public IActionResult ListadoProductoPdf()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Consulta a la base de datos
            var query = "SELECT codigo, descripcion, preciov, unidades FROM productos";
            using var connection = new SqlConnection(connectionString);
            var productos = connection.Query<insertarproductomodel>(query).ToList();

            // Generar el PDF
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            //  manejador de eventos para el pie de página
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new footer(document));

            // Título del documento
            document.Add(new Paragraph("Listado de productos")
                .SetFontSize(18)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER));

            //   tabla con encabezados
            Table table = new Table(4, true); // 4 columnas
            table.AddHeaderCell("codigo");
            table.AddHeaderCell("descripcion");
            table.AddHeaderCell("precio");
            table.AddHeaderCell("unidades");

            insertarproductomodel hacer1 = new insertarproductomodel();
            var hacer2 = _RepoProducto.HacerPDF2(hacer1);

            // Llenar la tabla con datos
            foreach (var persona in productos)
            {
                table.AddCell(persona.codigo.ToString());
                table.AddCell(persona.descripcion);
                table.AddCell(persona.preciov);
                table.AddCell(persona.unidades.ToString());
            }

            // Agregar la tabla al documento
            document.Add(table);
            document.Close();

            // Retornar el archivo como respuesta
            byte[] pdfBytes = stream.ToArray();
            // Aplicar opción abrir pestaña navegador
            Response.Headers.Add("Content-Disposition", "inline; filename=ListadoProductos.pdf");
            return File(pdfBytes, "application/Informes");
        }
    }
}
