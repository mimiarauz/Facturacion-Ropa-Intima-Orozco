using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aplicacion.Reportes;
using MediatR;

namespace WebAPI.Controllers.ReportController
{
    [Route("ReporteColorController")]
    public class ReporteColorController : Controller
    {
        private readonly IMediator _mediator;
        public ReporteColorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Stream>> GetTask()
        {
            return await _mediator.Send(new ReporteColor.Consulta());
        }
    }
}