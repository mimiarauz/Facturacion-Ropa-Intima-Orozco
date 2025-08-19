using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string DireccionCliente { get; set; } = null!;

    public int NumCliente { get; set; }

    public virtual ICollection<Facturacion> Facturacions { get; set; } = new List<Facturacion>();

    public virtual Persona IdClienteNavigation { get; set; } = null!;
}
