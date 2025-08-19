using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Proyecto.Models;

namespace Aplicacion.Reportes.Compras
{
    public class ListasCompras
    {
        public class Compras: IRequest<List<TodasFacturasCompra>>{
            public int IdCompras;
        }
        public class Manejador: IRequestHandler<Compras, List<TodasFacturasCompra>>
        {
            private readonly RopaIntimaOrozcoOficialnewContext context;
            public Manejador(RopaIntimaOrozcoOficialnewContext _context)
            {
                this.context = _context;
            }

            // Este es para TODAS las facturas
            public async Task<List<TodasFacturasCompra>> Handle(Compras request, CancellationToken cancellationToken)
            {
                var todascompras = await context.TodasFacturasCompras.ToListAsync();
                
                return todascompras;
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