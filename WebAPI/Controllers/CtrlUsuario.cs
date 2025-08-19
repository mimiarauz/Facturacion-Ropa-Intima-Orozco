using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto.Models;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Controllers
{
    [Route("[controller]")]
    public class CtrlUsuario : Controller
    {
        private readonly RopaIntimaOrozcoOficialnewContext _DBcontext;
        public CtrlUsuario(RopaIntimaOrozcoOficialnewContext dbContext)
        {
            this._DBcontext=dbContext;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var usuario=this._DBcontext.Usuarios.ToList();
            return Ok(usuario);
        }


        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            // Buscar la persona por su ID
            var persona = this._DBcontext.Personas.Include(p => p.Usuario).FirstOrDefault(p => p.Id == id);
            
            // Verificar si la persona existe
            if (persona == null)
            {
                return BadRequest(new { success = false, message = "No se encontró la persona para eliminar." });
            }
            
            // Eliminar el usuario asociado, si existe
            if (persona.Usuario != null)
            {
                this._DBcontext.Usuarios.Remove(persona.Usuario);
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
        public IActionResult Update(int IdUsuario, [FromBody] UsuarioUpdate _usuario)
        {
            var usuario = this._DBcontext.Usuarios
            .Include(c => c.IdUsuarioNavigation)  // Asegúrate de cargar la propiedad de navegación
            .FirstOrDefault(o => o.IdUsuario == IdUsuario);
            
            if (usuario == null)
            {
                return NotFound("La entidad Cliente no existe y no puede ser actualizada.");
            }
            
            // Actualiza los campos necesarios
            usuario.Usuario1 = _usuario.Usuario1;
            usuario.TipoUsuario = _usuario.TipoUsuario;
            usuario.Estado = _usuario.Estado;
            usuario.Email = _usuario.Email;
            
            if (usuario.IdUsuario != null)
            {
                usuario.IdUsuarioNavigation.NombrePersona = _usuario.NombrePersona;
                usuario.IdUsuarioNavigation.ApellidoPersona = _usuario.ApellidoPersona;
                usuario.IdUsuarioNavigation.Cedula = _usuario.Cedula;
            }
            this._DBcontext.SaveChanges();
            return Ok(new { success = true, message = "La entidad Usuario ha sido actualizada con éxito." });
        }


        [HttpGet("Autenticar/{usuario}/{contrasena}")]
        public IActionResult Autenticar(string usuario, string contrasena)
        {
            // Utiliza Include para cargar ansiosamente la información de la tabla Persona
            var usuarioInfo = this._DBcontext.Usuarios
            .Include(u => u.IdUsuarioNavigation) // IdUsuarioNavigation es la referencia a la tabla Persona en la clase Usuario
            .FirstOrDefault(x => x.Usuario1 == usuario && x.Contraseña == contrasena);

    if (usuarioInfo == null)
    {
        return BadRequest("Usuario o contraseña incorrecta");
    }
    else
    {
        return Json(new
        {
            id = usuarioInfo.IdUsuario,
            usuario1 = usuarioInfo.Usuario1,
            tipoUsuario = usuarioInfo.TipoUsuario,
            nombrePersona = usuarioInfo.IdUsuarioNavigation.NombrePersona,
            estado = usuarioInfo.Estado,
            cedula = usuarioInfo.IdUsuarioNavigation.Cedula
        });
    }
}


        //Metodo de recupercion de contrasena
        [HttpGet("VerificarCorreo/{correo}")]
        public IActionResult VerificarCorreo(string correo)
        {
            var Correos = this._DBcontext.Usuarios.FirstOrDefault(x => x.Email == correo);

            if (Correos ==null)
            {
                return BadRequest("El correo no tiene un registro de cuenta");
            }else{
                return Json(Correos);
            }
        }
    }
}