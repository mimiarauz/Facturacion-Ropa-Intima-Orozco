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
    [Route("CtrlDetalleCompra")]
    public class CtrlDetalleCompra : Controller
    {
        private readonly RopaIntimaOrozcoOficialnewContext _DBcontext;
        public CtrlDetalleCompra(RopaIntimaOrozcoOficialnewContext dbContext)
        {
            this._DBcontext=dbContext;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var DetalleCom=this._DBcontext.DetalleCompras.ToList();
            return Ok(DetalleCom);
        }
        
        [HttpDelete("Remove")]
        public IActionResult Remove(int code) 
        {
            var DetalleCom=this._DBcontext.DetalleCompras.FirstOrDefault(o=>o.IdDetalleCompras==code);
            if (DetalleCom!=null)
            {
                this._DBcontext.Remove(DetalleCom);
                this._DBcontext.SaveChanges();
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] DetalleCompra _DetalleCom) 
        {
            var DetalleCom=this._DBcontext.DetalleCompras.FirstOrDefault(o=>o.IdDetalleCompras==_DetalleCom.IdDetalleCompras);
            if (DetalleCom!=null)
            {
                return BadRequest("La entidad ya existe. Utiliza la función de actualización en su lugar.");
            }
            this._DBcontext.DetalleCompras.Add(_DetalleCom);
            this._DBcontext.SaveChanges();
            
            return Ok(true);
        }

        [HttpPut("Update")]
        public IActionResult Update(int code, [FromBody] DetalleCompra _DetalleCom)
        {
            var DetalleCom = this._DBcontext.DetalleCompras.FirstOrDefault(o => o.IdDetalleCompras == code);
            if (DetalleCom != null)
            {
                DetalleCom.IdDetalleCompras=_DetalleCom.IdDetalleCompras;
                DetalleCom.IdCompras=_DetalleCom.IdCompras;
                DetalleCom.CantidadProductosC=_DetalleCom.CantidadProductosC;
                DetalleCom.IdProducto=_DetalleCom.IdProducto;
                DetalleCom.PrecioC=_DetalleCom.PrecioC;
                DetalleCom.PrecioV=_DetalleCom.PrecioV;
                this._DBcontext.SaveChanges();
                return Ok(true);
            }
            else
            {
                this._DBcontext.DetalleCompras.Add(_DetalleCom);
                this._DBcontext.SaveChanges();
                return Ok(true);
            }
        }
    }
}