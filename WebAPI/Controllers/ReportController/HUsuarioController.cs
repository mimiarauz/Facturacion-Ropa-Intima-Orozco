using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Aplicacion.Reportes;
using MediatR;
using Aplicacion.Reportes.Ventas;
using Aplicacion.Reportes.Compras;

namespace WebAPI.Controllers.ReportController
{
    [Route("HUsuarioController")]
    public class HUsuarioController : Controller
    {
        private readonly IMediator _mediator;
        public HUsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Stream>> GetTask()
        {
            return await _mediator.Send(new ReporteHUsuarios.Consulta());
        }
    }
}