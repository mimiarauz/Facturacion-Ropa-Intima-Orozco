using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Aplicacion.Reportes;
using MediatR;
using Aplicacion.Reportes.Compras;
namespace WebAPI.Controllers.ReportController
{
    [Route("AllComprasController")]
    public class AllComprasController : Controller
    {
        private readonly IMediator _mediator;
        public AllComprasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var compra = await _mediator.Send(new ListasCompras.Compras());
            // return Json(compra);
            var data = compra.Select(com => new object[]
            {
                com.IdCompras,
                com.FechaCompras,
                com.Cedula,
                com.NombrePersona,
                // Agrega los demás campos necesarios aquí
                com.TotalGeneral
            }).ToList();

            return Json(new
            {
                draw = 1, // Puedes ajustar este valor según tus necesidades
                recordsTotal = data.Count,
                recordsFiltered = data.Count,
                data = compra
            });
        }
    }
}