using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Talla
{
    public string IdTalla { get; set; } = null!;

    public string Talla1 { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
