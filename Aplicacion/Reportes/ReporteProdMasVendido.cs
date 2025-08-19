using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Validators;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Proyecto.Models;
using Rectangle = iTextSharp.text.Rectangle;

namespace Aplicacion.Reportes
{
    public class ReporteProdMasVendido
    {
        public class Consulta: IRequest<Stream>{}
        public class Manejador: IRequestHandler<Consulta, Stream>
        {
            private readonly RopaIntimaOrozcoOficialnewContext context;
            public Manejador(RopaIntimaOrozcoOficialnewContext _context)
            {
                this.context = _context;
            }

            public async Task<Stream> Handle(Consulta request, CancellationToken cancellationToken)
            {
                var producto = await context.ProductosMasVendidos.ToListAsync();
                var productos = producto.OrderByDescending(p => p.TotalCantidad).ToList();

                // Fuentes
                Font fuenteTitulo = new Font(Font.DEFAULTSIZE, 13f, Font.BOLD, BaseColor.Black);
                Font fuenteTitulo2 = new Font(Font.DEFAULTSIZE, 13f, Font.NORMAL, BaseColor.Black);
                Font fuenteEncabezado = new Font(Font.DEFAULTSIZE,12f, Font.BOLD, BaseColor.White);
                Font fuenteDatos = new Font(Font.DEFAULTSIZE,11f, Font.NORMAL, BaseColor.DarkGray);

                // Crear Documento
                MemoryStream workstream = new MemoryStream();
                Rectangle rectangle = new Rectangle(PageSize.A4);

                Document document = new Document(rectangle, 0,0,0,100);
                PdfWriter writer = PdfWriter.GetInstance(document, workstream);
                writer.CloseStream = false;

                document.Open();
                document.AddTitle("Productos más vendidos");

                PdfPTable miniencabezado = new PdfPTable(5);
                miniencabezado.WidthPercentage=100;
                miniencabezado.DefaultCell.Border = Rectangle.NO_BORDER; 
                miniencabezado.SpacingAfter = 5;

                PdfPCell c1 = new PdfPCell( new Phrase(" "));
                c1.BackgroundColor = new BaseColor(87, 73, 100);
                c1.Border = Rectangle.NO_BORDER; 
                miniencabezado.AddCell(c1);

                PdfPCell c2 = new PdfPCell( new Phrase(" "));
                c2.BackgroundColor = new BaseColor(113, 102, 123);
                c2.Border = Rectangle.NO_BORDER; 
                miniencabezado.AddCell(c2);

                PdfPCell c3 = new PdfPCell( new Phrase(" "));
                c3.BackgroundColor = new BaseColor(181, 142, 158);
                c3.Border = Rectangle.NO_BORDER; 
                miniencabezado.AddCell(c3);

                PdfPCell c4 = new PdfPCell( new Phrase(" "));
                c4.BackgroundColor = new BaseColor(204, 183, 174);
                c4.Border = Rectangle.NO_BORDER; 
                miniencabezado.AddCell(c4);

                PdfPCell c5 = new PdfPCell( new Phrase(" "));
                c5.BackgroundColor = new BaseColor(214, 207, 203);
                c5.Border = Rectangle.NO_BORDER; 
                miniencabezado.AddCell(c5);

                document.Add(miniencabezado);


            //     // ENCABEZADO
                PdfPTable encabezado = new PdfPTable(3);
                encabezado.WidthPercentage=100;
                encabezado.SetTotalWidth(new float[] { 5, 3, 4 }); 
                encabezado.DefaultCell.Border = Rectangle.NO_BORDER;     
              

            // CELDA IZQUIERDA
                PdfPTable tableIz = new PdfPTable(1);
                tableIz.DefaultCell.Border = Rectangle.NO_BORDER; 

                PdfPCell onecell = new PdfPCell(new Phrase("Ropa Íntima Orozco", fuenteTitulo));
                onecell.Border = Rectangle.NO_BORDER; 
                onecell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableIz.AddCell(onecell);
                PdfPCell twocell = new PdfPCell(new Phrase("Productos más vendidos", fuenteTitulo2));
                twocell.Border = Rectangle.NO_BORDER; 
                twocell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableIz.AddCell(twocell);

                PdfPCell espacio = new PdfPCell(tableIz);
                espacio.Border = Rectangle.NO_BORDER; 
                espacio.Padding = 30f;
                encabezado.AddCell(espacio);

                
            // CELDA DE EN MEDIO
                encabezado.AddCell("");


            // CELDA DERECHA
                
                PdfPCell cellimg = new PdfPCell();
                cellimg.Border = Rectangle.NO_BORDER; 
                // cellimg.BackgroundColor = new BaseColor(231, 206, 255);
                // Imagen
                Image imagen = Image.GetInstance("C:/WebFactura/Aplicacion/Imágenes/Logo.jpg");// Crear un objeto Image con la ruta de la imagen
                imagen.ScaleAbsolute(130, 70); // Ancho: 200, Alto: 150
                imagen.Alignment = Element.ALIGN_CENTER; // 
                cellimg.AddElement(imagen); // Añadir la imagen a la celda

                PdfPCell padding = new PdfPCell(cellimg);
                padding.Padding = 10f;

                encabezado.AddCell(padding);

                document.Add(encabezado);


            // CUERPO

                Chunk linea = new Chunk(new LineSeparator(3f,100f, BaseColor.Gray, Element.ALIGN_CENTER,0));
                document.Add(linea);

                PdfPTable tablaDatos = new PdfPTable(6);
                float[] width = new float[]{16, 16, 16, 16, 16, 20};
                tablaDatos.SpacingBefore = 60;
                tablaDatos.SetWidthPercentage(width, rectangle);

                PdfPCell celdaCod = new PdfPCell(new Phrase("Código", fuenteEncabezado));
                celdaCod.Border = Rectangle.NO_BORDER;
                celdaCod.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaCod.BackgroundColor = new BaseColor(87, 73, 100);
                celdaCod.Padding = 6;
                tablaDatos.AddCell(celdaCod);

                PdfPCell celdaCate = new PdfPCell(new Phrase("Categoría", fuenteEncabezado));
                celdaCate.Border = Rectangle.NO_BORDER;
                celdaCate.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaCate.BackgroundColor = new BaseColor(87, 73, 100);
                celdaCate.Padding = 6;
                tablaDatos.AddCell(celdaCate);

                PdfPCell celdaMarc = new PdfPCell(new Phrase("Marca", fuenteEncabezado));
                celdaMarc.Border = Rectangle.NO_BORDER;
                celdaMarc.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaMarc.BackgroundColor = new BaseColor(87, 73, 100);
                celdaMarc.Padding = 6;
                tablaDatos.AddCell(celdaMarc);
                
                PdfPCell celdaColor = new PdfPCell(new Phrase("Color", fuenteEncabezado));
                celdaColor.Border = Rectangle.NO_BORDER;
                celdaColor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaColor.BackgroundColor = new BaseColor(87, 73, 100);
                celdaColor.Padding = 6;
                tablaDatos.AddCell(celdaColor);

                PdfPCell celdaTalla = new PdfPCell(new Phrase("Talla", fuenteEncabezado));
                celdaTalla.Border = Rectangle.NO_BORDER;
                celdaTalla.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaTalla.BackgroundColor = new BaseColor(87, 73, 100);
                celdaTalla.Padding = 6;
                tablaDatos.AddCell(celdaTalla);

                PdfPCell celdaCant = new PdfPCell(new Phrase("Cantidad Vendida", fuenteEncabezado));
                celdaCant.Border = Rectangle.NO_BORDER;
                celdaCant.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaCant.BackgroundColor = new BaseColor(87, 73, 100);
                celdaCant.Padding = 6;
                tablaDatos.AddCell(celdaCant);

                tablaDatos.WidthPercentage=90;

                foreach(var p in productos)
                {
                    PdfPCell celdaDatoCod = new PdfPCell(new Phrase("" + p.CodProducto, fuenteDatos));
                    celdaDatoCod.Border = Rectangle.BOX;
                    celdaDatoCod.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoCod.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoCod.BorderColor = BaseColor.White;
                    celdaDatoCod.Padding = 4;
                    tablaDatos.AddCell(celdaDatoCod);

                    PdfPCell celdaDatoCate = new PdfPCell(new Phrase("" + p.Categoria, fuenteDatos));
                    celdaDatoCate.Border = Rectangle.BOX;
                    celdaDatoCate.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoCate.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoCate.BorderColor = BaseColor.White;
                    celdaDatoCate.Padding = 4;
                    tablaDatos.AddCell(celdaDatoCate);

                    PdfPCell celdaDatoMarc = new PdfPCell(new Phrase("" + p.Marca, fuenteDatos));
                    celdaDatoMarc.Border = Rectangle.BOX;
                    celdaDatoMarc.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoMarc.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoMarc.BorderColor = BaseColor.White;
                    celdaDatoMarc.Padding = 4;
                    tablaDatos.AddCell(celdaDatoMarc);

                    PdfPCell celdaDatoColor = new PdfPCell(new Phrase("" + p.Color, fuenteDatos));
                    celdaDatoColor.Border = Rectangle.BOX;
                    celdaDatoColor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoColor.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoColor.BorderColor = BaseColor.White;
                    celdaDatoColor.Padding = 4;
                    tablaDatos.AddCell(celdaDatoColor);

                    PdfPCell celdaDatoTalla = new PdfPCell(new Phrase("" + p.Talla, fuenteDatos));
                    celdaDatoTalla.Border = Rectangle.BOX;
                    celdaDatoTalla.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoTalla.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoTalla.BorderColor = BaseColor.White;
                    celdaDatoTalla.Padding = 4;
                    tablaDatos.AddCell(celdaDatoTalla);
                    
                    PdfPCell celdaDatoCant = new PdfPCell(new Phrase("" + p.TotalCantidad, fuenteDatos));
                    celdaDatoCant.Border = Rectangle.BOX;
                    celdaDatoCant.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoCant.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoCant.BorderColor = BaseColor.White;
                    celdaDatoCant.Padding = 4;
                    tablaDatos.AddCell(celdaDatoCant);
                }

                document.Add(tablaDatos);

                document.Close();

                byte[] byteData = workstream.ToArray();
                workstream.Write(byteData,0,byteData.Length);
                workstream.Position = 0;
                return workstream;

            }
        }
    }
}