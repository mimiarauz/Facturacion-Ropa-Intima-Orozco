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
    [Route("CtrlPersona")]
    public class CtrlPersona : Controller
    {

        private readonly RopaIntimaOrozcoOficialnewContext _DBcontext;
        public CtrlPersona(RopaIntimaOrozcoOficialnewContext dbContext)
        {
            this._DBcontext=dbContext;
        }
        

        [HttpGet("GetWithUsers")]
        public IActionResult GetWithUsers()
        {
            var personasConUsuarios = this._DBcontext.Personas
            .Where(p => p.Usuario != null)
            .ToList();
            //return Ok(personasConUsuarios);
            return Json(personasConUsuarios);
        }

        [HttpGet("GetWithClientes")]
        public IActionResult GetWithClientes()
        {
            var personasConClientes = this._DBcontext.Personas
            .Where(p => p.Cliente != null)
            .ToList();
            return Ok(personasConClientes);
        }

         [HttpGet("GetWithProveedores")]
        public IActionResult GetWithProveedores()
        {
            var personasConProveedores = this._DBcontext.Personas
            .Where(p => p.Proveedor != null)
            .ToList();
            return Ok(personasConProveedores);
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var personas=this._DBcontext.Personas.ToList();
            return Ok(personas);
        }
        

        [HttpDelete("Remove")]
        public IActionResult Remove(int code) 
        {
            var personas=this._DBcontext.Personas.FirstOrDefault(o=>o.Id==code);
            if (personas!=null)
            {
                this._DBcontext.Remove(personas);
                this._DBcontext.SaveChanges();
                return Ok(true);
            }
            return Ok(false);
        }


        [HttpPost("Create")]
        public IActionResult Create([FromBody] Persona _personas) 
        {
            var personas=this._DBcontext.Personas.FirstOrDefault(o=>o.Id==_personas.Id);
            if (personas!=null)
            {
                personas.Id=_personas.Id;
                personas.NombrePersona=_personas.NombrePersona;
                personas.ApellidoPersona=_personas.ApellidoPersona;
                personas.Cedula=_personas.Cedula;
                this._DBcontext.SaveChanges();
            }
            else
            {
                this._DBcontext.Personas.Add(_personas);
                this._DBcontext.SaveChanges();
            }
            return Ok(true);
        }

        
        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] Persona _persona)
        {
            var persona = this._DBcontext.Personas.FirstOrDefault(o => o.Id == id);
            if (persona == null)
            {
                return NotFound("La entidad Componente no existe y no puede ser actualizada.");
            }
            // Actualiza los campos necesarios
            persona.Id = _persona.Id;
            
            this._DBcontext.SaveChanges();
            return Ok(true);
        }
    }
}