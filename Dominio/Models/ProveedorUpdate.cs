using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class ProveedorUpdate
    {
        public int IdProveedor { get; set; }
        public int NumProveedor { get; set; }
        public string DireccionProveedor { get; set; } = null!;
        public string? CorreoProveedor { get; set; }
        public string Estado { get; set; } = null!;
        public string NombrePersona { get; set; } = null!;
        public string ApellidoPersona { get; set; } = null!;
        public string Cedula { get; set; } = null!;

    }
}