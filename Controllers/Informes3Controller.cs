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
    public class Informes3Controller : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IRepositorioprovedor _RepoProvedores;
        public Informes3Controller(IConfiguration configuration, IRepositorioprovedor RepoProvedores)
        {
            _configuration = configuration;
            _RepoProvedores = RepoProvedores;
        }

        public IActionResult index()
        {
            return View();
        }

        public IActionResult pdfv()
        {
            return View();
        }
        public IActionResult ListadoProvedoresPdf()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Consulta a la base de datos
            var query = "SELECT Cedula, Nombre, Apellido, Ntelefono,Correo,Empresa FROM provedores";
            using var connection = new SqlConnection(connectionString);
            var personas = connection.Query<provedorModel>(query).ToList();

            // Generar el PDF
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            //  manejador de eventos para el pie de página
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new footer(document));

            // Título del documento
            document.Add(new Paragraph("Listado de provedores")
                .SetFontSize(18)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER));

            //   tabla con encabezados
            Table table = new Table(6, true); // 4 columnas
            table.AddHeaderCell("Cedula");
            table.AddHeaderCell("Nombre");
            table.AddHeaderCell("Apellido");
            table.AddHeaderCell("Telefono");
            table.AddHeaderCell("Correo");
            table.AddHeaderCell("Empresa");

            provedorModel hacer1 = new provedorModel();
            var hacer2 = _RepoProvedores.HacerPDF3(hacer1);

            // Llenar la tabla con datos
            foreach (var persona in personas)
            {
                table.AddCell(persona.Cedula.ToString());
                table.AddCell(persona.Nombre);
                table.AddCell(persona.Apellido);
                table.AddCell(persona.Ntelefono.ToString());
                table.AddCell(persona.Correo);
                table.AddCell(persona.Empresa);
            }

            // Agregar la tabla al documento
            document.Add(table);
            document.Close();

            // Retornar el archivo como respuesta
            byte[] pdfBytes = stream.ToArray();
            // Aplicar opción abrir pestaña navegador
            Response.Headers.Add("Content-Disposition", "inline; filename=ListadoProvedores.pdf");
            return File(pdfBytes, "application/Informes");
        }
    }
}
