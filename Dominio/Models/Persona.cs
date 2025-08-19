using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Persona
{
    public int Id { get; set; }

    public string NombrePersona { get; set; } = null!;

    public string ApellidoPersona { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public virtual Cliente? Cliente { get; set; }

    public virtual Proveedor? Proveedor { get; set; }

    public virtual Usuario? Usuario { get; set; }
    
}
