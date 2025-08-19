using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    [Route("CtrlProducto")]
    public class CtrlProducto : Controller
    {
        private readonly RopaIntimaOrozcoOficialnewContext _DBcontext;
        public CtrlProducto(RopaIntimaOrozcoOficialnewContext dbContext)
        {
            this._DBcontext = dbContext;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var produc = this._DBcontext.Productos.ToList();
            return Ok(produc);
        }


        [HttpGet("GetDetails")]
        public IActionResult GetDetails(int id)
        {
            var producto = this._DBcontext.Productos
            .Include(p => p.IdCategoriaNavigation)
            .Include(p => p.IdMarcaNavigation)
            .Include(p => p.IdColorNavigation)
            .Include(p => p.IdTallaNavigation)
            .FirstOrDefault(p => p.IdProducto == id);


            if (producto != null)
            {
                // Devolver detalles necesarios
                var detalles = new
                {
                    Stock = producto.Stock,
                    Distintivo = producto.Distintivo,
                    PrecioVenta = producto.PrecioVenta,
                    Categoria = producto.IdCategoriaNavigation?.Categoria ?? "",
                    Marca1 = producto.IdMarcaNavigation?.Marca1 ?? "",
                    Color1 = producto.IdColorNavigation?.Color1 ?? "",
                    Talla1 = producto.IdTallaNavigation?.Talla1 ?? "",
                    Codigo = producto.CodProducto
                };
                return Ok(detalles);
            }
            else
            {
                return NotFound(); // Producto no encontrado
            }
        }



        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            try
            {
                var produc = this._DBcontext.Productos.FirstOrDefault(o => o.IdProducto == id);
                if (produc != null)
                {
                    this._DBcontext.Remove(produc);
                    this._DBcontext.SaveChanges();
                    return Ok(true);
                }
                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Producto _prod)
        {
            var prod = this._DBcontext.Productos.FirstOrDefault(o => o.IdProducto == _prod.IdProducto);
            if (prod != null)
            {
                return BadRequest("La entidad Componente ya existe. Utiliza la función de actualización en su lugar.");
            }
            this._DBcontext.Productos.Add(_prod);
            this._DBcontext.SaveChanges();
            return Ok(true);
        }

        //   
        [HttpPut("Update")]
        public IActionResult Update(int code, [FromBody] Producto _prod)
        {
            var prod = this._DBcontext.Productos.FirstOrDefault(o => o.IdProducto == code);
            if (prod != null)
            {
                prod.IdProducto = _prod.IdProducto;
                prod.IdCategoria = _prod.IdCategoria;
                prod.IdTalla = _prod.IdTalla;
                prod.IdColor = _prod.IdColor;
                prod.IdMarca = _prod.IdMarca;
                prod.Distintivo = _prod.Distintivo;
                prod.PrecioVenta = _prod.PrecioVenta;
                prod.Stock = _prod.Stock;
                prod.CodProducto = _prod.CodProducto;
                this._DBcontext.SaveChanges();
                return Ok(true);
            }
            else
            {
                this._DBcontext.Productos.Add(_prod);
                this._DBcontext.SaveChanges();
                return Ok(true);
            }
        }
    }
}