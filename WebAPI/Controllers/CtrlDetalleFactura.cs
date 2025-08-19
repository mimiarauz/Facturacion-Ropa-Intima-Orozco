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
    [Route("CtrlDetalleFactura")]
    public class CtrlDetalleFactura : Controller
    {
        private readonly RopaIntimaOrozcoOficialnewContext _DBcontext;
        public CtrlDetalleFactura(RopaIntimaOrozcoOficialnewContext dbContext)
        {
            this._DBcontext=dbContext;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var DetalleFac=this._DBcontext.DetalleFacturas.ToList();
            return Ok(DetalleFac);
        }
        
        [HttpDelete("Remove")]
        public IActionResult Remove(int code) 
        {
            var DetalleFac=this._DBcontext.DetalleFacturas.FirstOrDefault(o=>o.IdDetalleFactura==code);
            if (DetalleFac!=null)
            {
                this._DBcontext.Remove(DetalleFac);
                this._DBcontext.SaveChanges();
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] DetalleFactura _DetalleFac) 
        {
            var DetalleFac=this._DBcontext.DetalleFacturas.FirstOrDefault(o=>o.IdDetalleFactura==_DetalleFac.IdDetalleFactura);
            if (DetalleFac!=null)
            {
                return BadRequest("La entidad ya existe");
                
            }
            this._DBcontext.DetalleFacturas.Add(_DetalleFac);
            this._DBcontext.SaveChanges();
            
            return Ok(true);
        }

        [HttpPut("Update")]
        public IActionResult Update(int code, [FromBody] DetalleFactura _DetalleFac)
        {
            var DetalleFac = this._DBcontext.DetalleFacturas.FirstOrDefault(o => o.IdDetalleFactura == code);
            if (DetalleFac != null)
            {
                DetalleFac.IdDetalleFactura=_DetalleFac.IdDetalleFactura;
                DetalleFac.CantidadProductosF=_DetalleFac.CantidadProductosF;
                DetalleFac.IdProducto=_DetalleFac.IdProducto;
                DetalleFac.IdFactura=_DetalleFac.IdFactura;
                DetalleFac.Precio=_DetalleFac.Precio;
                this._DBcontext.SaveChanges();
                return Ok(true);
            }
            else
            {
                this._DBcontext.DetalleFacturas.Add(_DetalleFac);
                this._DBcontext.SaveChanges();
                return Ok(true);
            }
        }
    }
}