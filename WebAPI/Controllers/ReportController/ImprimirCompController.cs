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
    [Route("ImprimirCompController")]
    public class ImprimirCompController : Controller
    {
        private readonly IMediator _mediator;
        public ImprimirCompController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{idfac}")]
        public async Task<ActionResult<Stream>> factura(int idfac)
        {
            return await _mediator.Send(new ReporteImpriCompra.Consult{IdCompra = idfac});
        }
    }
}