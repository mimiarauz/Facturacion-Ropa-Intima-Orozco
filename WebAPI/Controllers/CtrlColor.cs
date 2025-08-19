using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto.Models;

namespace WebAPI.Controllers
{
    [Route("CtrlColor")]
    public class CtrlColor : Controller
    {
        private readonly RopaIntimaOrozcoOficialnewContext _DBcontext;
        public CtrlColor(RopaIntimaOrozcoOficialnewContext dbContext)
        {
            this._DBcontext=dbContext;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var color=this._DBcontext.Colors.ToList();
            return Ok(color);
        }
        
        [HttpDelete("Remove")]
        public IActionResult Remove(string code) 
        {
            var color=this._DBcontext.Colors.FirstOrDefault(o=>o.IdColor==code);
            if (color!=null)
            {
                this._DBcontext.Remove(color);
                this._DBcontext.SaveChanges();
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Color _color) 
        {
            var color=this._DBcontext.Colors.FirstOrDefault(o=>o.IdColor==_color.IdColor);
            if (color!=null)
            {
                color.IdColor=_color.IdColor;
                color.Color1=_color.Color1;
                
                this._DBcontext.SaveChanges();
            }
            else
            {
                this._DBcontext.Colors.Add(_color);
                this._DBcontext.SaveChanges();
            }
            return Ok(true);
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(string id, [FromBody] Color _color)
        {
            var color = this._DBcontext.Colors.FirstOrDefault(o => o.IdColor == id);
            if (color == null)
            {
                return NotFound("La entidad no existe y no puede ser actualizada.");
            }
            // Actualiza los campos necesarios
            color.Color1 = _color.Color1;
            this._DBcontext.SaveChanges();
            return Ok(true);
        }
    }
}