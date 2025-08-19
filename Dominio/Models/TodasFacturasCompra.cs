using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class TodasFacturasCompra
{
    public int IdCompras { get; set; }

    public DateTime FechaCompras { get; set; }

    public string Cedula { get; set; } = null!;

    public string NombrePersona { get; set; } = null!;

    public string ApellidoPersona { get; set; } = null!;

    public int NumProveedor { get; set; }

    public decimal? TotalGeneral { get; set; }
}
