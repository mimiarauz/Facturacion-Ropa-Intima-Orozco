using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto.Models;
using Microsoft.EntityFrameworkCore;
using Dominio.Models;

namespace WebAPI.Controllers
{
    [Route("CtrlCliente")]
    public class CtrlCliente : Controller
    {
        private readonly RopaIntimaOrozcoOficialnewContext _DBcontext;
        public CtrlCliente(RopaIntimaOrozcoOficialnewContext dbContext)
        {
            this._DBcontext=dbContext;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var cate=this._DBcontext.Clientes.ToList();
            return Ok(cate);
        }
        
        [HttpGet("GetClienteDetails")]
        public IActionResult GetClienteDetails(int id)
        {
            var cliente = this._DBcontext.Personas.FirstOrDefault(p => p.Id == id);

        if (cliente != null)
        {
            // Devolver solo los detalles necesarios (en este caso, solo el stock)
            var detalles = new { ApellidoPersona = cliente.ApellidoPersona };
            return Ok(detalles);
        }
        else
        {
            return NotFound(); // Producto no encontrado
        }
        }
        
        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            // Buscar la persona por su ID
            var persona = this._DBcontext.Personas.Include(p => p.Cliente).FirstOrDefault(p => p.Id == id);
            
            // Verificar si la persona existe
            if (persona == null)
            {
                return BadRequest(new { success = false, message = "No se encontró la persona para eliminar." });
            }
            
            // Eliminar el cliente asociado, si existe
            if (persona.Cliente != null)
            {
                this._DBcontext.Clientes.Remove(persona.Cliente);
            }
            
            // Eliminar la persona
            this._DBcontext.Personas.Remove(persona);
            this._DBcontext.SaveChanges();
            return Ok(new { success = true });  
        }


        [HttpPost("Create")]
        public IActionResult Create([FromBody] Persona _personas)
        {
            var persona = this._DBcontext.Personas.FirstOrDefault(o => o.Id == _personas.Id);
            if (persona != null)
            {
                return BadRequest("La entidad Componente ya existe. Utiliza la función de actualización en su lugar.");
            }
            
            this._DBcontext.Personas.Add(_personas);
            this._DBcontext.SaveChanges();
            return Ok(true);
        }
        
        [HttpPut("Update/{id}")]
        public IActionResult Update(int idCliente, [FromBody] ClienteUpdate _usuario)
        {
            var usuario = this._DBcontext.Clientes
            .Include(c => c.IdClienteNavigation)  // Asegúrate de cargar la propiedad de navegación
            .FirstOrDefault(o => o.IdCliente == idCliente);
            
            if (usuario == null)
            {
                return NotFound("La entidad Cliente no existe y no puede ser actualizada.");
            }
            
            // Actualiza los campos necesarios
            usuario.DireccionCliente = _usuario.DireccionCliente;
            usuario.NumCliente = _usuario.NumCliente;
            
            if (usuario.IdClienteNavigation != null)
            {
                usuario.IdClienteNavigation.NombrePersona = _usuario.NombrePersona;
                usuario.IdClienteNavigation.ApellidoPersona = _usuario.ApellidoPersona;
                usuario.IdClienteNavigation.Cedula = _usuario.Cedula;
            }
            this._DBcontext.SaveChanges();
            return Ok(new { success = true, message = "La entidad Cliente ha sido actualizada con éxito." });
        }
    }
}