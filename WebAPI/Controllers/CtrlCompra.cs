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
    [Route("CtrlCompra")]
    public class CtrlCompra : Controller
    {
        private readonly RopaIntimaOrozcoOficialnewContext _DBcontext;
        public CtrlCompra(RopaIntimaOrozcoOficialnewContext dbContext)
        {
            this._DBcontext=dbContext;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var color=this._DBcontext.Compras.ToList();
            return Ok(color);
        }
        
        [HttpDelete("Remove")]
        public IActionResult Remove(int code) 
        {
            var color=this._DBcontext.Compras.FirstOrDefault(o=>o.IdCompras==code);
            if (color!=null)
            {
                this._DBcontext.Remove(color);
                this._DBcontext.SaveChanges();
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Compra _compra) 
        {
            var compra=this._DBcontext.Compras.FirstOrDefault(o=>o.IdCompras==_compra.IdCompras);
            if (compra!=null)
            {
                return BadRequest("La entidad ya existe. Utiliza la función de actualización en su lugar.");
               
            }
            this._DBcontext.Compras.Add(_compra);
            this._DBcontext.SaveChanges();
            
            return Ok(true);
        }

        [HttpPut("Update")]
        public IActionResult Update(int code, [FromBody] Compra _compra)
        {
            var compra = this._DBcontext.Compras.FirstOrDefault(o => o.IdCompras == code);
            if (compra != null)
            {
                compra.IdCompras=_compra.IdCompras;
                compra.FechaCompras=_compra.FechaCompras;
                compra.TotalGeneral=_compra.TotalGeneral;
                this._DBcontext.SaveChanges();
                return Ok(true);
            }
            else
            {
                this._DBcontext.Compras.Add(_compra);
                this._DBcontext.SaveChanges();
                return Ok(true);
            }
        }
    }
}