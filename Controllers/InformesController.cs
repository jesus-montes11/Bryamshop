using Dapper;
using BRIAMSHOP.Models;
using BRIAMSHOP.Repositorio.footer;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Events;
using iText.Layout.Properties;
using BRIAMSHOP.Repositorio;


namespace BRIAMSHOP.Controllers
{
    public class InformesController : Controller
    {
        
        
            private readonly IConfiguration _configuration;
            private readonly IRepositoriopersonas _RepoPersonas;
            public InformesController(IConfiguration configuration, IRepositoriopersonas RepoPersonas)
            {
                _configuration = configuration;
                _RepoPersonas = RepoPersonas; 
            }

            public IActionResult index()
            {
                return View();
            }

             public IActionResult pdfv()
             {
                return View();
             }
            public IActionResult ListadoPersonasPdf()
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                // Consulta a la base de datos
                var query = "SELECT Id, firstName, rapellidos, rfecha FROM tregistro";
                using var connection = new SqlConnection(connectionString);
                var personas = connection.Query<Registromodel>(query).ToList();

                // Generar el PDF
                MemoryStream stream = new MemoryStream();
                PdfWriter writer = new PdfWriter(stream);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                //  manejador de eventos para el pie de página
                pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new footer(document));

                // Título del documento
                document.Add(new Paragraph("Listado de Personas")
                    .SetFontSize(18)
                    .SetBold()
                    .SetTextAlignment(TextAlignment.CENTER));

                //   tabla con encabezados
                Table table = new Table(4, true); // 4 columnas
                table.AddHeaderCell("ID");
                table.AddHeaderCell("Nombre");
                table.AddHeaderCell("Apellido");
                table.AddHeaderCell("Fecha");

                Registromodel hacer1 = new Registromodel();
                var hacer2 = _RepoPersonas.HacerPDF1(hacer1);

               // Llenar la tabla con datos
                foreach (var persona in personas)
                {
                    table.AddCell(persona.Id.ToString());
                    table.AddCell(persona.firstName);
                    table.AddCell(persona.rapellidos);
                    table.AddCell(persona.rfecha.ToString());
                }

                // Agregar la tabla al documento
                document.Add(table);
                document.Close();

                // Retornar el archivo como respuesta
                byte[] pdfBytes = stream.ToArray();
                // Aplicar opción abrir pestaña navegador
                Response.Headers.Add("Content-Disposition", "inline; filename=ListadoPersonas.pdf");
                return File(pdfBytes, "application/Informes");
            }
        }
    }