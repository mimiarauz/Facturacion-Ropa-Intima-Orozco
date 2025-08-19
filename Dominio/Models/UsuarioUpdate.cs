using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class UsuarioUpdate
    {
        public int IdUsuario { get; set; }
        public string Usuario1 { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string TipoUsuario { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string NombrePersona { get; set; } = null!;
        public string ApellidoPersona { get; set; } = null!;
        public string Cedula { get; set; } = null!;
    }
}