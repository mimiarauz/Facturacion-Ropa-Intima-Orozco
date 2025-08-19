using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Proyecto.Models;
using Rectangle = iTextSharp.text.Rectangle;

namespace Aplicacion.Reportes
{
    public class ReporteInventario
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
                var stocks = await context.InventarioProductos.ToListAsync();

                // Fuentes
                Font fuenteTitulo = new Font(Font.DEFAULTSIZE, 13f, Font.BOLD, BaseColor.Black);
                Font fuenteTitulo2 = new Font(Font.DEFAULTSIZE, 13f, Font.NORMAL, BaseColor.Black);
                Font fuenteEncabezado = new Font(Font.DEFAULTSIZE,11f, Font.BOLD, BaseColor.White);
                Font fuenteDatos = new Font(Font.DEFAULTSIZE,10f, Font.NORMAL, BaseColor.DarkGray);

                // Crear Documento
                MemoryStream workstream = new MemoryStream();
                Rectangle rectangle = new Rectangle(PageSize.A4);

                Document document = new Document(rectangle, 0,0,0,100);
                PdfWriter writer = PdfWriter.GetInstance(document, workstream);
                writer.CloseStream = false;

                document.Open();
                document.AddTitle("Inventario");

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
                onecell.HorizontalAlignment = Element.ALIGN_LEFT;
                tableIz.AddCell(onecell);
                PdfPCell twocell = new PdfPCell(new Phrase("Reporte de Inventario", fuenteTitulo2));
                twocell.Border = Rectangle.NO_BORDER; 
                twocell.HorizontalAlignment = Element.ALIGN_LEFT;
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

                PdfPTable tablaDatos = new PdfPTable(8);
                float[] width = new float[]{12, 12, 12, 12, 9, 15, 13, 11};
                tablaDatos.SpacingBefore = 60;
                tablaDatos.SetWidthPercentage(width, rectangle);

                PdfPCell celdaCod = new PdfPCell(new Phrase("Código", fuenteEncabezado));
                celdaCod.Border = Rectangle.NO_BORDER;
                celdaCod.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaCod.BackgroundColor = new BaseColor(87, 73, 100);
                celdaCod.Padding = 6;
                tablaDatos.AddCell(celdaCod);

                PdfPCell celdaCateg = new PdfPCell(new Phrase("Categoría", fuenteEncabezado));
                celdaCateg.Border = Rectangle.NO_BORDER;
                celdaCateg.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaCateg.BackgroundColor = new BaseColor(87, 73, 100);
                celdaCateg.Padding = 6;
                tablaDatos.AddCell(celdaCateg);

                PdfPCell celdaMarca = new PdfPCell(new Phrase("Marca", fuenteEncabezado));
                celdaMarca.Border = Rectangle.NO_BORDER;
                celdaMarca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaMarca.BackgroundColor = new BaseColor(87, 73, 100);
                celdaMarca.Padding = 6;
                tablaDatos.AddCell(celdaMarca);

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

                PdfPCell celdaDisti = new PdfPCell(new Phrase("Distintivo", fuenteEncabezado));
                celdaDisti.Border = Rectangle.NO_BORDER;
                celdaDisti.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaDisti.BackgroundColor = new BaseColor(87, 73, 100);
                celdaDisti.Padding = 6;
                tablaDatos.AddCell(celdaDisti);

                PdfPCell celdaPrecioV = new PdfPCell(new Phrase("PrecioVenta", fuenteEncabezado));
                celdaPrecioV.Border = Rectangle.NO_BORDER;
                celdaPrecioV.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaPrecioV.BackgroundColor = new BaseColor(87, 73, 100);
                celdaPrecioV.Padding = 6;
                tablaDatos.AddCell(celdaPrecioV);

                PdfPCell celdaStock = new PdfPCell(new Phrase("Stock", fuenteEncabezado));
                celdaStock.Border = Rectangle.NO_BORDER;
                celdaStock.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaStock.BackgroundColor = new BaseColor(87, 73, 100);
                celdaStock.Padding = 6;
                tablaDatos.AddCell(celdaStock);

                tablaDatos.WidthPercentage=90;

                foreach(var stock in stocks)
                {
                    PdfPCell celdaDatoCod = new PdfPCell(new Phrase("" + stock.CodProducto, fuenteDatos));
                    celdaDatoCod.Border = Rectangle.BOX;
                    celdaDatoCod.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoCod.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoCod.BorderColor = BaseColor.White;
                    celdaDatoCod.Padding = 4;
                    tablaDatos.AddCell(celdaDatoCod);
                    
                    PdfPCell celdaDatoCat = new PdfPCell(new Phrase("" + stock.Categoria, fuenteDatos));
                    celdaDatoCat.Border = Rectangle.BOX;
                    celdaDatoCat.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoCat.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoCat.BorderColor = BaseColor.White;
                    celdaDatoCat.Padding = 4;
                    tablaDatos.AddCell(celdaDatoCat);

                    PdfPCell celdaDatoMarc = new PdfPCell(new Phrase("" + stock.Marca, fuenteDatos));
                    celdaDatoMarc.Border = Rectangle.BOX;
                    celdaDatoMarc.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoMarc.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoMarc.BorderColor = BaseColor.White;
                    celdaDatoMarc.Padding = 4;
                    tablaDatos.AddCell(celdaDatoMarc);

                    PdfPCell celdaDatoColor = new PdfPCell(new Phrase("" + stock.Color, fuenteDatos));
                    celdaDatoColor.Border = Rectangle.BOX;
                    celdaDatoColor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoColor.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoColor.BorderColor = BaseColor.White;
                    celdaDatoColor.Padding = 4;
                    tablaDatos.AddCell(celdaDatoColor);

                    PdfPCell celdaDatoTalla = new PdfPCell(new Phrase("" + stock.Talla, fuenteDatos));
                    celdaDatoTalla.Border = Rectangle.BOX;
                    celdaDatoTalla.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoTalla.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoTalla.BorderColor = BaseColor.White;
                    celdaDatoTalla.Padding = 4;
                    tablaDatos.AddCell(celdaDatoTalla);

                    PdfPCell celdaDatoDist = new PdfPCell(new Phrase("" + stock.Distintivo, fuenteDatos));
                    celdaDatoDist.Border = Rectangle.BOX;
                    celdaDatoDist.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoDist.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoDist.BorderColor = BaseColor.White;
                    celdaDatoDist.Padding = 4;
                    tablaDatos.AddCell(celdaDatoDist);

                    PdfPCell celdaDatoPreV = new PdfPCell(new Phrase("C$ " + stock.PrecioVenta, fuenteDatos));
                    celdaDatoPreV.Border = Rectangle.BOX;
                    celdaDatoPreV.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoPreV.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoPreV.BorderColor = BaseColor.White;
                    celdaDatoPreV.Padding = 4;
                    tablaDatos.AddCell(celdaDatoPreV);

                    PdfPCell celdaDatoStock = new PdfPCell(new Phrase("" + stock.Stock, fuenteDatos));
                    celdaDatoStock.Border = Rectangle.BOX;
                    celdaDatoStock.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoStock.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoStock.BorderColor = BaseColor.White;
                    celdaDatoStock.Padding = 4;
                    tablaDatos.AddCell(celdaDatoStock);
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