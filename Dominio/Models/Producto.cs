using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string IdCategoria { get; set; } = null!;

    public string IdTalla { get; set; } = null!;

    public string IdColor { get; set; } = null!;

    public string IdMarca { get; set; } = null!;

    public string Distintivo { get; set; } = null!;

    public decimal PrecioVenta { get; set; }

    public int Stock { get; set; }

    public string CodProducto { get; set; } = null!;

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;

    public virtual Color IdColorNavigation { get; set; } = null!;

    public virtual Marca IdMarcaNavigation { get; set; } = null!;

    public virtual Talla IdTallaNavigation { get; set; } = null!;
}
