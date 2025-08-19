using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Aplicacion.Reportes;
using MediatR;
using Proyecto.Models;

namespace WebAPI.Controllers.ReportController
{
    [Route("ImprimirFactController")]
    public class ImprimirFactController : Controller
    {
        private readonly IMediator _mediator;
        public ImprimirFactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{idfac}")]
        public async Task<ActionResult<Stream>> factura(int idfac)
        {
            return await _mediator.Send(new ReporteImpriFact.Consult{IdFactura = idfac});
        }

        // [HttpGet]
        // public async Task<JsonResult> Get()
        // {
        //     var factura = await _mediator.Send(new ListaVentas.Ventas());
        //     //return Json(invoices);
        //     var data = factura.Select(fac => new object[]
        //     {
        //         fac.CodProducto,
        //         fac.Categoria,
        //         fac.Distintivo,
        //         fac.CantidadProductosF,
        //         fac.Precio,
        //         // Agrega los demás campos necesarios aquí
        //         fac.TotalFactura
        //     }).ToList();

        //     return Json(new
        //     {
        //         draw = 1, // Puedes ajustar este valor según tus necesidades
        //         recordsTotal = data.Count,
        //         recordsFiltered = data.Count,
        //         data = invoices
        //     });
        // }
    }
}