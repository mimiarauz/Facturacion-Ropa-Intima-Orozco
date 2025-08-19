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
    [Route("CtrlTalla")]
    public class CtrlTalla : Controller
    {
        private readonly RopaIntimaOrozcoOficialnewContext _DBcontext;
        public CtrlTalla(RopaIntimaOrozcoOficialnewContext dbContext)
        {
            this._DBcontext=dbContext;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var talla=this._DBcontext.Tallas.ToList();
            return Ok(talla);
        }
        
        [HttpDelete("Remove")]
        public IActionResult Remove(string code) 
        {
            var talla=this._DBcontext.Tallas.FirstOrDefault(o=>o.IdTalla==code);
            if (talla!=null)
            {
                this._DBcontext.Remove(talla);
                this._DBcontext.SaveChanges();
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Talla _talla) 
        {
            var talla=this._DBcontext.Tallas.FirstOrDefault(o=>o.IdTalla==_talla.IdTalla);
            if (talla!=null)
            {
                talla.IdTalla=_talla.IdTalla;
                talla.Talla1=_talla.Talla1;

                this._DBcontext.SaveChanges();
            }
            else
            {
                this._DBcontext.Tallas.Add(_talla);
                this._DBcontext.SaveChanges();
            }
            return Ok(true);
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(string id, [FromBody] Talla _talla)
        {
            var talla = this._DBcontext.Tallas.FirstOrDefault(o => o.IdTalla == id);
            if (talla == null)
            {
                return NotFound("La entidad no existe y no puede ser actualizada.");
            }
            talla.Talla1=_talla.Talla1;
            this._DBcontext.SaveChanges();
            return Ok(true);
        }
    }
}