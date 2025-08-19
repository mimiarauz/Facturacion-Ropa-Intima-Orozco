using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    [Route("CtrlFacturacion")]
    public class CtrlFacturacion : Controller
    {
        private readonly RopaIntimaOrozcoOficialnewContext _DBcontext;
        public CtrlFacturacion(RopaIntimaOrozcoOficialnewContext dbContext)
        {
            this._DBcontext=dbContext;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var facturacions=this._DBcontext.Facturacions.ToList();
            return Ok(facturacions);
        }
        
        [HttpDelete("Remove")]
        public IActionResult Remove(int code) 
        {
            var Facturacion=this._DBcontext.Facturacions.FirstOrDefault(o=>o.IdFactura==code);
            if (Facturacion!=null)
            {
                this._DBcontext.Remove(Facturacion);
                this._DBcontext.SaveChanges();
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Facturacion _facturacion) 
        {
            var facturacion=this._DBcontext.Facturacions.FirstOrDefault(o=>o.IdFactura==_facturacion.IdFactura);
            if (facturacion!=null)
            {
                return BadRequest("La entidad ya existe");
            }
            this._DBcontext.Facturacions.Add(_facturacion);
            this._DBcontext.SaveChanges();
            
            return Ok(true);
        }

        [HttpPut("Update")]
        public IActionResult Update(int code, [FromBody] Facturacion _facturacion)
        {
            var fact = this._DBcontext.Facturacions.FirstOrDefault(o => o.IdFactura == code);
            if (fact != null)
            {
                fact.IdFactura=_facturacion.IdFactura;
                fact.FechaFactura=_facturacion.FechaFactura;
                fact.DescuentoFactura=_facturacion.DescuentoFactura;
                fact.IdCliente=_facturacion.IdCliente;
                fact.IdUsuario=_facturacion.IdUsuario;
                fact.TotaFactura=_facturacion.TotaFactura;
                this._DBcontext.SaveChanges();
                return Ok(true);
            }
            else
            {
                this._DBcontext.Facturacions.Add(_facturacion);
                this._DBcontext.SaveChanges();
                return Ok(true);
            }
        }
    }
}