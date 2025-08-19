using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public partial class ProductosMasVendido
    {
        public int IdProducto { get; set; }
        public string CodProducto { get; set; } = null!;
        public string Categoria { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string Talla { get; set; } = null!;
        public int? TotalCantidad { get; set; }
    }
}