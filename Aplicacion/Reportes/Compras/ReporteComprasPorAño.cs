using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Proyecto.Models;
using Rectangle = iTextSharp.text.Rectangle;

namespace Aplicacion.Reportes.Compras
{
    public class ReporteComprasPorAño
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
                // var producto = await context.TodasFacturasVentas.OrderBy(p => p.IdProducto).ToListAsync();
                var compras = await context.ComprasAños.ToListAsync();

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
                document.AddTitle("Compras por Año");

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
                PdfPCell twocell = new PdfPCell(new Phrase("Reporte Compras por Año", fuenteTitulo2));
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

                PdfPTable tablaDatos = new PdfPTable(2);
                float[] width = new float[]{50, 50};
                tablaDatos.SpacingBefore = 60;
                tablaDatos.SetWidthPercentage(width, rectangle);

                PdfPCell celdaID = new PdfPCell(new Phrase("Año", fuenteEncabezado));
                celdaID.Border = Rectangle.NO_BORDER;
                celdaID.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaID.BackgroundColor = new BaseColor(87, 73, 100);
                celdaID.Padding = 6;
                tablaDatos.AddCell(celdaID);

                PdfPCell celdaColor = new PdfPCell(new Phrase("Subtotal", fuenteEncabezado));
                celdaColor.Border = Rectangle.NO_BORDER;
                celdaColor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaColor.BackgroundColor = new BaseColor(87, 73, 100);
                celdaColor.Padding = 6;
                tablaDatos.AddCell(celdaColor);

                tablaDatos.WidthPercentage=90;

                decimal totalgeneral = 0;
                foreach(var compr in compras)
                {
                    PdfPCell celdaDatoID = new PdfPCell(new Phrase("" + compr.Año, fuenteDatos));
                    celdaDatoID.Border = Rectangle.BOX;
                    celdaDatoID.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoID.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoID.BorderColor = BaseColor.White;
                    celdaDatoID.Padding = 4;
                    tablaDatos.AddCell(celdaDatoID);
                    
                    PdfPCell celdaDatoColor = new PdfPCell(new Phrase("C$ " + compr.Subtotal, fuenteDatos));
                    celdaDatoColor.Border = Rectangle.BOX;
                    celdaDatoColor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoColor.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoColor.BorderColor = BaseColor.White;
                    celdaDatoColor.Padding = 4;
                    tablaDatos.AddCell(celdaDatoColor);

                    totalgeneral += compr.Subtotal;

                }

                document.Add(tablaDatos);

                PdfPTable TablaTota = new PdfPTable(4);
                TablaTota.DefaultCell.Border = Rectangle.BOX;
                float[] tamaño = new float[] {30 ,20, 20, 30};
                TablaTota.SetWidths(tamaño);
                TablaTota.WidthPercentage=90;
                TablaTota.SpacingBefore = 30;

                PdfPCell celdaVacia = new PdfPCell(new Phrase(""));
                celdaVacia.Border = Rectangle.NO_BORDER;

                PdfPCell celdaTotal = new PdfPCell(new Phrase("Total General:", fuenteEncabezado));
                celdaTotal.Border = Rectangle.NO_BORDER;
                celdaTotal.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaTotal.BackgroundColor = new BaseColor(87, 73, 100);
                celdaTotal.Padding = 6;

                PdfPCell celdaDatoTotal = new PdfPCell(new Phrase("C$ " + totalgeneral.ToString("#,###.00"), fuenteDatos));
                celdaDatoTotal.Border = Rectangle.BOX;
                celdaDatoTotal.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaDatoTotal.BackgroundColor = new BaseColor(214, 207, 203);
                celdaDatoTotal.BorderColor = BaseColor.White;
                celdaDatoTotal.Padding = 4;

                PdfPCell celdaVacia2 = new PdfPCell(new Phrase(""));
                celdaVacia2.Border = Rectangle.NO_BORDER;

                TablaTota.AddCell(celdaVacia);
                TablaTota.AddCell(celdaTotal);
                TablaTota.AddCell(celdaDatoTotal);
                TablaTota.AddCell(celdaVacia2);
                
                document.Add(TablaTota);

                document.Close();

                byte[] byteData = workstream.ToArray();
                workstream.Write(byteData,0,byteData.Length);
                workstream.Position = 0;
                return workstream;

            }
        }
    }
}