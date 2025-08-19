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
    [Route("CtrlMarca")]
    public class CtrlMarca : Controller
    {
        private readonly RopaIntimaOrozcoOficialnewContext _DBcontext;
        public CtrlMarca(RopaIntimaOrozcoOficialnewContext dbContext)
        {
            this._DBcontext=dbContext;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var marca=this._DBcontext.Marcas.ToList();
            return Ok(marca);
        }
        
        [HttpDelete("Remove")]
        public IActionResult Remove(string code) 
        {
            var marca=this._DBcontext.Marcas.FirstOrDefault(o=>o.IdMarca==code);
            if (marca!=null)
            {
                this._DBcontext.Remove(marca);
                this._DBcontext.SaveChanges();
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Marca _marca) 
        {
            var marca=this._DBcontext.Marcas.FirstOrDefault(o=>o.IdMarca==_marca.IdMarca);
            if (marca!=null)
            {
                marca.IdMarca=_marca.IdMarca;
                marca.Marca1=_marca.Marca1;
                this._DBcontext.SaveChanges();
            }
            else
            {
                this._DBcontext.Marcas.Add(_marca);
                this._DBcontext.SaveChanges();
            }
            return Ok(true);
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(string id, [FromBody] Marca _marca)
        {
            var marca = this._DBcontext.Marcas.FirstOrDefault(o => o.IdMarca == id);
            if (marca == null)
            {
               return NotFound("La entidad no existe y no puede ser actualizada.");
                
            }
            //actualiza los campos
             marca.Marca1=_marca.Marca1;
             this._DBcontext.SaveChanges();
             return Ok(true);
             
        }
    }
}