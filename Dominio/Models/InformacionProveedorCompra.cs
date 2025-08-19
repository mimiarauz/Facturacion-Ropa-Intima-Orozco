using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class InformacionProveedorCompra
{
    public int Id { get; set; }

    public string NombrePersona { get; set; } = null!;

    public string ApellidoPersona { get; set; } = null!;

    public int NumProveedor { get; set; }

    public string DireccionProveedor { get; set; } = null!;

    public string? CorreoProveedor { get; set; }

    public string Estado { get; set; } = null!;

    public int IdCompras { get; set; }

    public DateTime FechaCompras { get; set; }

    public int IdProducto { get; set; }

    public int CantidadProductosC { get; set; }

    public decimal? PrecioC { get; set; }

    public decimal PrecioV { get; set; }
}
