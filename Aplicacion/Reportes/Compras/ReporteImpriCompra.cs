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
    public class ReporteImpriCompra
    {
        public class Consult: IRequest<Stream>{
            public int IdCompra;
        }
        public class Manejador: IRequestHandler<Consult, Stream>
        {
            private readonly RopaIntimaOrozcoOficialnewContext context;
            public Manejador(RopaIntimaOrozcoOficialnewContext _context)
            {
                this.context = _context;
            }

            public async Task<Stream> Handle(Consult request, CancellationToken cancellationToken)
            {
                int id = request.IdCompra;

                var compra = await context.ImprimirFacturaCompras.Where(compr => compr.IdCompras == id).ToListAsync();
                //var facturacion = await context.ImprimirFacturaVentas.ToListAsync();
                var datosCompra = context.ImprimirFacturaCompras.FirstOrDefault(compr => compr.IdCompras == id);
                //var datosFactura = await context.ImprimirFacturaVentas.ToListAsync();


                // Fuentes
                Font fuenteTitulo = new Font(Font.DEFAULTSIZE, 13f, Font.BOLD, BaseColor.Black);
                Font fuenteTitulo2 = new Font(Font.DEFAULTSIZE, 12f, Font.BOLD, BaseColor.Black);
                Font fuenteTitulo3 = new Font(Font.DEFAULTSIZE, 11f, Font.BOLD, BaseColor.Black);
                Font fuenteEncabezado = new Font(Font.DEFAULTSIZE,11f, Font.BOLD, BaseColor.White);
                Font fuenteDatos = new Font(Font.DEFAULTSIZE,10f, Font.NORMAL, BaseColor.DarkGray);
                Font fuenteDatos2 = new Font(Font.DEFAULTSIZE,10f, Font.BOLD, BaseColor.DarkGray);

                // Crear Documento
                MemoryStream workstream = new MemoryStream();
                Rectangle rectangle = new Rectangle(PageSize.A4);

                Document document = new Document(rectangle, 0,0,0,100);
                PdfWriter writer = PdfWriter.GetInstance(document, workstream);
                writer.CloseStream = false;

                document.Open();
                document.AddTitle("Impresión de Compras");

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
                //tableIz.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell onecell = new PdfPCell(new Phrase("Ropa Íntima Orozco", fuenteTitulo));
                onecell.Border = Rectangle.NO_BORDER; 
                onecell.HorizontalAlignment = Element.ALIGN_LEFT;
                tableIz.AddCell(onecell);
                tableIz.AddCell("");

                PdfPCell twocell = new PdfPCell(new Phrase("Factura de Compras", fuenteTitulo2));
                twocell.Border = Rectangle.NO_BORDER; 
                twocell.HorizontalAlignment = Element.ALIGN_LEFT;
                tableIz.AddCell(twocell);
                tableIz.AddCell("");

                PdfPTable intertableiz = new PdfPTable(2);
                intertableiz.SetTotalWidth(new float[] { 35, 65 });                 
                intertableiz.DefaultCell.Border = Rectangle.NO_BORDER;

                PdfPCell CeldaFactura = new PdfPCell(new Phrase("Compra Nº:", fuenteTitulo3));
                CeldaFactura.Border = Rectangle.NO_BORDER;
                CeldaFactura.HorizontalAlignment = Element.ALIGN_LEFT;
                intertableiz.AddCell(CeldaFactura);
                intertableiz.AddCell(new Phrase(datosCompra.IdCompras.ToString(), fuenteTitulo3));

                // PdfPCell CeldaNo = new PdfPCell(new Phrase("No:", fuenteDatos));
                // CeldaFactura.Border = Rectangle.NO_BORDER;
                // CeldaFactura.HorizontalAlignment = 10;
                // tableIz.AddCell(CeldaNo);

                tableIz.AddCell(intertableiz);


                PdfPCell espacio = new PdfPCell(tableIz);
                espacio.Border = Rectangle.NO_BORDER; 
                espacio.Padding = 20f;
                encabezado.AddCell(espacio);

                
                
            // CELDA DE EN MEDIO
                encabezado.AddCell("");


            // CELDA DERECHA
                
                PdfPCell cellimg = new PdfPCell();
                cellimg.Border = Rectangle.NO_BORDER; 
                // cellimg.BackgroundColor = new BaseColor(231, 206, 255);
                // Imagen
                Image imagen = Image.GetInstance(".../../../../../Aplicacion/Imágenes/Logo.jpg");// Crear un objeto Image con la ruta de la imagen
                imagen.ScaleAbsolute(130, 70); // Ancho: 200, Alto: 150
                imagen.Alignment = Element.ALIGN_CENTER;
                cellimg.AddElement(imagen); // Añadir la imagen a la celda

                PdfPCell padding = new PdfPCell(cellimg);
                padding.Rowspan = 80;
                padding.Padding = 15;

                encabezado.AddCell(padding);

                document.Add(encabezado);


            // CUERPO

                Chunk linea = new Chunk(new LineSeparator(3f,100f, BaseColor.Gray, Element.ALIGN_CENTER,0));
                document.Add(linea);

                PdfPTable tituloinfo2 = new PdfPTable(3);
                tituloinfo2.SetTotalWidth(new float[] { 10, 42, 48 }); 
                tituloinfo2.DefaultCell.Border = Rectangle.NO_BORDER;
                tituloinfo2.SpacingBefore = 40;
                tituloinfo2.SpacingAfter = 0;

                PdfPTable celda1 = new PdfPTable(1);
                celda1.DefaultCell.Border = Rectangle.NO_BORDER;

                PdfPTable celda2 = new PdfPTable(2);
                celda2.SetWidths(new float[] { 35f, 65f });
                celda2.DefaultCell.Border = Rectangle.NO_BORDER;

                PdfPTable celda3 = new PdfPTable(2);
                celda3.SetWidths(new float[] { 35f, 65f });
                celda3.DefaultCell.Border = Rectangle.NO_BORDER;

                // tituloinfo.SpacingBefore = 20;
                // tituloinfo.SpacingAfter = 0;

             
                PdfPCell CeldaProve = new PdfPCell(new Phrase("Proveedor:", fuenteDatos2));
                CeldaProve.HorizontalAlignment = 50;
                CeldaProve.Border = Rectangle.NO_BORDER;
                celda2.AddCell(CeldaProve);
                celda2.AddCell(new Phrase(datosCompra.NombrePersona + " " + datosCompra.ApellidoPersona, fuenteDatos));
                celda2.AddCell("");
                celda2.AddCell("");

                PdfPCell CeldaFecha = new PdfPCell(new Phrase("Dirección:", fuenteDatos2));
                CeldaFecha.HorizontalAlignment = 50;
                CeldaFecha.Border = Rectangle.NO_BORDER;
                celda2.AddCell(CeldaFecha);
                celda2.AddCell(new Phrase(datosCompra.DireccionProveedor.ToString(), fuenteDatos));
                

                celda1.AddCell("");

                PdfPCell CeldaCliente = new PdfPCell(new Phrase("Fecha:", fuenteDatos2));
                CeldaCliente.HorizontalAlignment = 50;
                CeldaCliente.Border = Rectangle.NO_BORDER;
                celda3.AddCell(CeldaCliente);
                celda3.AddCell(new Phrase(datosCompra.FechaCompras.ToString(), fuenteDatos));
                celda3.AddCell("");
                celda3.AddCell("");

                
                PdfPCell CeldaNum = new PdfPCell(new Phrase("Número:", fuenteDatos2));
                CeldaNum.HorizontalAlignment = 50;
                CeldaNum.Border = Rectangle.NO_BORDER;
                celda3.AddCell(CeldaNum);
                celda3.AddCell(new Phrase(datosCompra.NumProveedor.ToString(), fuenteDatos));

                tituloinfo2.AddCell(celda1);
                tituloinfo2.AddCell(celda2);
                tituloinfo2.AddCell(celda3);
                
                document.Add(tituloinfo2);


                PdfPTable tablaDatos = new PdfPTable(5);
                float[] width = new float[]{20, 20, 20, 20, 20};
                tablaDatos.SpacingBefore = 30;
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

                PdfPCell celdaMarca = new PdfPCell(new Phrase("Distintivo", fuenteEncabezado));
                celdaMarca.Border = Rectangle.NO_BORDER;
                celdaMarca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaMarca.BackgroundColor = new BaseColor(87, 73, 100);
                celdaMarca.Padding = 6;
                tablaDatos.AddCell(celdaMarca);

                PdfPCell celdaColor = new PdfPCell(new Phrase("Cantidad", fuenteEncabezado));
                celdaColor.Border = Rectangle.NO_BORDER;
                celdaColor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaColor.BackgroundColor = new BaseColor(87, 73, 100);
                celdaColor.Padding = 6;
                tablaDatos.AddCell(celdaColor);

                PdfPCell celdaTalla = new PdfPCell(new Phrase("Precio", fuenteEncabezado));
                celdaTalla.Border = Rectangle.NO_BORDER;
                celdaTalla.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaTalla.BackgroundColor = new BaseColor(87, 73, 100);
                celdaTalla.Padding = 6;
                tablaDatos.AddCell(celdaTalla);

                tablaDatos.WidthPercentage=90;


                foreach(var f in compra)
                {
                    PdfPCell celdaDatoCod = new PdfPCell(new Phrase(f.CodProducto, fuenteDatos));
                    celdaDatoCod.Border = Rectangle.BOX;
                    celdaDatoCod.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoCod.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoCod.BorderColor = BaseColor.White;
                    celdaDatoCod.Padding = 4;
                    tablaDatos.AddCell(celdaDatoCod);
                
                    PdfPCell celdaDatoCat = new PdfPCell(new Phrase(f.Categoria, fuenteDatos));
                    celdaDatoCat.Border = Rectangle.BOX;
                    celdaDatoCat.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoCat.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoCat.BorderColor = BaseColor.White;
                    celdaDatoCat.Padding = 4;
                    tablaDatos.AddCell(celdaDatoCat);

                    PdfPCell celdaDatoMarc = new PdfPCell(new Phrase(f.Distintivo, fuenteDatos));
                    celdaDatoMarc.Border = Rectangle.BOX;
                    celdaDatoMarc.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoMarc.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoMarc.BorderColor = BaseColor.White;
                    celdaDatoMarc.Padding = 4;
                    tablaDatos.AddCell(celdaDatoMarc);

                    PdfPCell celdaDatoColor = new PdfPCell(new Phrase(f.CantidadProductosC.ToString(), fuenteDatos));
                    celdaDatoColor.Border = Rectangle.BOX;
                    celdaDatoColor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoColor.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoColor.BorderColor = BaseColor.White;
                    celdaDatoColor.Padding = 4;
                    tablaDatos.AddCell(celdaDatoColor);

                    PdfPCell celdaDatoID = new PdfPCell(new Phrase("C$ " + f.PrecioC.ToString(), fuenteDatos));
                    celdaDatoID.Border = Rectangle.BOX;
                    celdaDatoID.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celdaDatoID.BackgroundColor = new BaseColor(214, 207, 203);
                    celdaDatoID.BorderColor = BaseColor.White;
                    celdaDatoID.Padding = 4;
                    tablaDatos.AddCell(celdaDatoID);
                }

                document.Add(tablaDatos);

                PdfPTable piepagina = new PdfPTable(3);
                piepagina.DefaultCell.Border = Rectangle.BOX;
                float[] tamaño = new float[] {60 ,20, 20};
                piepagina.SetWidths(tamaño);
                piepagina.WidthPercentage=90;
                piepagina.SpacingBefore = 30;

                PdfPCell celdaVacia = new PdfPCell(new Phrase(""));
                celdaVacia.Border = Rectangle.NO_BORDER;


                PdfPCell celdaTotal = new PdfPCell(new Phrase("Total General:", fuenteEncabezado));
                celdaTotal.Border = Rectangle.NO_BORDER;
                celdaTotal.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaTotal.BackgroundColor = new BaseColor(87, 73, 100);
                celdaTotal.Padding = 6;

                PdfPCell celdaDatoTotal = new PdfPCell(new Phrase("C$ " + datosCompra.TotalGeneral.ToString(), fuenteDatos));
                celdaDatoTotal.Border = Rectangle.BOX;
                celdaDatoTotal.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celdaDatoTotal.BackgroundColor = new BaseColor(214, 207, 203);
                celdaDatoTotal.BorderColor = BaseColor.White;
                celdaDatoTotal.Padding = 4;

                //celdatotal.BackgroundColor = new BaseColor(87, 73, 100);
                piepagina.AddCell(celdaVacia);
                piepagina.AddCell(celdaTotal);
                piepagina.AddCell(celdaDatoTotal);
                
                document.Add(piepagina);
                document.Close();

                byte[] byteData = workstream.ToArray();
                workstream.Write(byteData,0,byteData.Length);
                workstream.Position = 0;
                return workstream;

            }
        }
    }
}