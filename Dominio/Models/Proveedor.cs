using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    public int NumProveedor { get; set; }

    public string DireccionProveedor { get; set; } = null!;

    public string? CorreoProveedor { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Persona IdProveedorNavigation { get; set; } = null!;
}
