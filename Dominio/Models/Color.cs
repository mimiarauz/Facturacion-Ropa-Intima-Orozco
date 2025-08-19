using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Color
{
    public string IdColor { get; set; } = null!;

    public string Color1 { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
