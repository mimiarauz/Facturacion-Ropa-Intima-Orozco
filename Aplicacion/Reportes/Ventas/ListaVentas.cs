using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Proyecto.Models;

namespace Aplicacion.Reportes
{
    public class ListaVentas
    {
        public class Ventas: IRequest<List<TodasFacturasVenta>>{
            public int IdFactura;
        }
        public class Manejador: IRequestHandler<Ventas, List<TodasFacturasVenta>>
        {
            private readonly RopaIntimaOrozcoOficialnewContext context;
            public Manejador(RopaIntimaOrozcoOficialnewContext _context)
            {
                this.context = _context;
            }

            // Este es para TODAS las facturas
            public async Task<List<TodasFacturasVenta>> Handle(Ventas request, CancellationToken cancellationToken)
            {
                var todasventas = await context.TodasFacturasVentas.ToListAsync();
                
                return todasventas;
            }

            // Este es para imprimir una solo factura con consulta para optimizar tiempo
            // public async Task<List<ImprimirFacturaVenta>> Handle(Ventas request, CancellationToken cancellationToken)
            // {
            //     var imprimirFacturaVenta = await context.ImprimirFacturaVentas
            //     .("")
            //     .ToListAsync();

            //     return imprimirFacturaVenta;
            // }
        }
    }
}