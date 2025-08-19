using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto.Models;
using Microsoft.EntityFrameworkCore;
using Humanizer;

namespace WebAPI.Controllers
{
    [Route("CambiarContraseñaController")]
    public class CtrlCambioContra : Controller
    {
        private readonly RopaIntimaOrozcoOficialnewContext _Dbcontext; // Reemplaza 'SmartnetpruebaContext' con el nombre de tu contexto

        public CtrlCambioContra(RopaIntimaOrozcoOficialnewContext Dbcontext)
        {
            _Dbcontext = Dbcontext;
        }

          [HttpPost("CambiarContra")]
        public async Task<IActionResult> CambiarContraseña(string usuario, string contraseñaActual, string nuevaContraseña)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseñaActual) || string.IsNullOrEmpty(nuevaContraseña))
            {
                return BadRequest("Por favor, completa todos los campos.");
            }

            try
            {
                // Buscar la entidad Persona y cargar la relación Usuario
                var user = await _Dbcontext.Usuarios
                    .Include(p => p.IdUsuarioNavigation) // Carga la relación Usuario junto con la entidad Persona
                    .FirstOrDefaultAsync(p => p.Usuario1 == usuario);

                if (user == null || user.Usuario1 == null)
                {
                    return NotFound("El usuario no fue encontrado.");
                }

                // Verificar la contraseña actual del usuario en la tabla 'Usuario'
                var usuarioDB = user.Usuario1;
                if (user.Contraseña != contraseñaActual)
                {
                    return BadRequest("La contraseña actual no es válida.");
                }

                // Actualizar la contraseña en la tabla 'Usuario'
                user.Contraseña = nuevaContraseña;

                // Guardar cambios en 'Usuario'
                await _Dbcontext.SaveChangesAsync();

                return Ok("Contraseña actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al actualizar la contraseña. Detalles: " + ex.Message);
            }
        }

    }
}