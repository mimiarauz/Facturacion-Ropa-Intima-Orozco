using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class ClienteUpdate
    {
        public int IdCliente { get; set; }
        public string DireccionCliente { get; set; } = null!;
        public int NumCliente { get; set; }
        public string NombrePersona { get; set; } = null!;
        public string ApellidoPersona { get; set; } = null!;
        public string Cedula { get; set; } = null!;

    }
}