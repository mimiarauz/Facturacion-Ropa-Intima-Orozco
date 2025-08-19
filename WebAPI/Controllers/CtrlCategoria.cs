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
    [Route("CtrlCategoria")]
    public class CtrlCategoria : Controller
    {
        private readonly RopaIntimaOrozcoOficialnewContext _DBcontext;
        public CtrlCategoria(RopaIntimaOrozcoOficialnewContext dbContext)
        {
            this._DBcontext=dbContext;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var categoria=this._DBcontext.Categoria.ToList();
            return Ok(categoria);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Categorium _categoria) 
        {
            var categoria=this._DBcontext.Categoria.FirstOrDefault(o=>o.IdCategoria==_categoria.IdCategoria);
            if (categoria!=null)
            {
                categoria.IdCategoria=_categoria.IdCategoria;
                categoria.Categoria=_categoria.Categoria;

                this._DBcontext.SaveChanges();
            }
            else
            {
                this._DBcontext.Categoria.Add(_categoria);
                this._DBcontext.SaveChanges();
            }
            return Ok(true);
        }


        [HttpPut("Update/{id}")]
        public IActionResult Update(string id, [FromBody] Categorium _categoria)
        {
            var categoria = this._DBcontext.Categoria.FirstOrDefault(o => o.IdCategoria == id);
            if (categoria == null)
            {
                return NotFound("La entidad no existe y no puede ser actualizada.");
            }
             categoria.Categoria = _categoria.Categoria;
             this._DBcontext.SaveChanges();
             return Ok(true);
        }

        [HttpDelete("Remove")]
        public IActionResult Remove(string code) 
        {
            var cate=this._DBcontext.Categoria.FirstOrDefault(o=>o.IdCategoria==code);
            if (cate!=null)
            {
                this._DBcontext.Remove(cate);
                this._DBcontext.SaveChanges();
                return Ok(true);
            }
            return Ok(false);
        }

        // private readonly RopaIntimaOrozcoOficialnewContext context;

        // public CtrlCategoria(RopaIntimaOrozcoOficialnewContext _context)
        // {
        //     this.context = _context;
        // }
        
        //  [HttpGet]
        // public IEnumerable<Categorium> Get()
        // {          
        //     return context.Categoria.ToList();
        // }
    }
}