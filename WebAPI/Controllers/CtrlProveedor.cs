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

namespace Proyecto.Controllers
{
    [Route("CtrlProveedor")]
    public class CtrlProveedor : Controller
    {
        private readonly RopaIntimaOrozcoOficialnewContext _DBcontext;
        public CtrlProveedor(RopaIntimaOrozcoOficialnewContext dbContext)
        {
            this._DBcontext=dbContext;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var Proveedor=this._DBcontext.Proveedors.ToList();
            return Ok(Proveedor);
        }
        
        [HttpGet("GetProveedorDetails")]
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
            var persona = this._DBcontext.Personas.Include(p => p.Proveedor).FirstOrDefault(p => p.Id == id);
            
            // Verificar si la persona existe
            if (persona == null)
            {
                return BadRequest(new { success = false, message = "No se encontró la persona para eliminar." });
                }
                // Eliminar el proveedor asociado, si existe
                
                if (persona.Proveedor != null)
                {
                    this._DBcontext.Proveedors.Remove(persona.Proveedor);
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
        public IActionResult Update(int idProveedor, [FromBody] ProveedorUpdate _proveedor)
        {
            var proveedor = this._DBcontext.Proveedors
            .Include(c => c.IdProveedorNavigation)  // Asegúrate de cargar la propiedad de navegación
            .FirstOrDefault(o => o.IdProveedor == idProveedor);
            
            if (proveedor == null)
            {
                return NotFound("La entidad Proveedor no existe y no puede ser actualizada.");
            }
            
            // Actualiza los campos necesarios
            proveedor.NumProveedor = _proveedor.NumProveedor;
            proveedor.DireccionProveedor = _proveedor.DireccionProveedor;
            proveedor.CorreoProveedor = _proveedor.CorreoProveedor;
            proveedor.Estado = _proveedor.Estado;
            
            if (proveedor.IdProveedorNavigation != null)
            {
                proveedor.IdProveedorNavigation.NombrePersona = _proveedor.NombrePersona;
                proveedor.IdProveedorNavigation.ApellidoPersona = _proveedor.ApellidoPersona;
                proveedor.IdProveedorNavigation.Cedula = _proveedor.Cedula;
            }
            this._DBcontext.SaveChanges();
            return Ok(new { success = true, message = "La entidad Proveedor ha sido actualizada con éxito." });
        }
    }
}