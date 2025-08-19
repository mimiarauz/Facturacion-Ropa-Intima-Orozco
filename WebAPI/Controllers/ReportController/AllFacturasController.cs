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
    [Route("AllFacturasController")]
    public class AllFacturasController : Controller
    {
        private readonly IMediator _mediator;
        public AllFacturasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var factura = await _mediator.Send(new ListaVentas.Ventas());
            //return Json(factura);
            var data = factura.Select(fac => new object[]
            {
                fac.IdFactura,
                fac.FechaFactura,
                fac.NombrePersona,
                // Agrega los demás campos necesarios aquí
                fac.ApellidoPersona,
                fac.TotaFactura
            }).ToList();

            return Json(new
            {
                draw = 1, // Puedes ajustar este valor según tus necesidades
                recordsTotal = data.Count,
                recordsFiltered = data.Count,
                data = factura
            });
        }
    }
}