using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using Aplicacion.Reportes;

namespace WebAPI.Controllers.ReportController
{
    [Route("StockAgotController")]
    public class StockAgotController : Controller
    {
        private readonly IMediator _mediator;
        public StockAgotController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Stream>> GetTask()
        {
            return await _mediator.Send(new ReportStockAgot.Consulta());
        }
    }
}