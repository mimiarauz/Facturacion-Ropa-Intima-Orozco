using System;
using System.Collections.Generic;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Models;

public partial class RopaIntimaOrozcoOficialnewContext : DbContext
{
    public RopaIntimaOrozcoOficialnewContext()
    {
    }

    public RopaIntimaOrozcoOficialnewContext(DbContextOptions<RopaIntimaOrozcoOficialnewContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<ComprasAño> ComprasAños { get; set; }

    public virtual DbSet<ComprasMe> ComprasMes { get; set; }

    public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }

    public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }

    public virtual DbSet<Facturacion> Facturacions { get; set; }

    public virtual DbSet<ImprimirFacturaCompra> ImprimirFacturaCompras { get; set; }

    public virtual DbSet<ImprimirFacturaVenta> ImprimirFacturaVentas { get; set; }

    public virtual DbSet<InformacionClienteFactura> InformacionClienteFacturas { get; set; }

    public virtual DbSet<InformacionProveedorCompra> InformacionProveedorCompras { get; set; }

    public virtual DbSet<InformacionUsuarioFactura> InformacionUsuarioFacturas { get; set; }

    public virtual DbSet<InventarioProducto> InventarioProductos { get; set; }

    public virtual DbSet<InventarioProductosP> InventarioProductosPs { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductosCategorium> ProductosCategoria { get; set; }

    public virtual DbSet<ProductosColor> ProductosColors { get; set; }

    public virtual DbSet<ProductosMarca> ProductosMarcas { get; set; }
    public virtual DbSet<ProductosMasVendido> ProductosMasVendidos { get; set; }

    public virtual DbSet<ProductosTalla> ProductosTallas { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<StockProximoAgotarse> StockProximoAgotarses { get; set; }

    public virtual DbSet<Talla> Tallas { get; set; }

    public virtual DbSet<TodasFacturasCompra> TodasFacturasCompras { get; set; }

    public virtual DbSet<TodasFacturasVenta> TodasFacturasVentas { get; set; }

    public virtual DbSet<TotalFacturaVenta> TotalFacturaVentas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VentasAño> VentasAños { get; set; }

    public virtual DbSet<VentasMe> VentasMes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Usuario>()
        // .HasOne(u => u.IdUsuarioNavigation)
        // .WithOne(p => p.Usuario)
        // .HasForeignKey<Usuario>(u => u.IdUsuario);

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria)
                .HasName("PK12")
                .IsClustered(false);

            entity.Property(e => e.IdCategoria).HasMaxLength(10);
            entity.Property(e => e.Categoria).HasMaxLength(20);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente)
                .HasName("PKCedulaCliente")
                .IsClustered(false);

            entity.ToTable("Cliente");

            entity.Property(e => e.IdCliente).ValueGeneratedNever();
            entity.Property(e => e.DireccionCliente).HasMaxLength(50);

            entity.HasOne(d => d.IdClienteNavigation).WithOne(p => p.Cliente)
                .HasForeignKey<Cliente>(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clientes_Persona");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.IdColor)
                .HasName("PK14")
                .IsClustered(false);

            entity.ToTable("Color");

            entity.Property(e => e.IdColor).HasMaxLength(10);
            entity.Property(e => e.Color1)
                .HasMaxLength(15)
                .HasColumnName("Color");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompras)
                .HasName("PKIdCompras")
                .IsClustered(false);

            entity.ToTable("Compra");

            entity.Property(e => e.FechaCompras).HasColumnType("date");
            entity.Property(e => e.TotalGeneral).HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compras_Proveedor");
        });

        modelBuilder.Entity<ComprasAño>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ComprasAÑO");

            entity.Property(e => e.Subtotal).HasColumnType("decimal(38, 2)");
        });

        modelBuilder.Entity<ComprasMe>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ComprasMES");

            entity.Property(e => e.Subtotal).HasColumnType("decimal(38, 2)");
        });

        modelBuilder.Entity<DetalleCompra>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCompras);

            entity.ToTable("DetalleCompra", tb => tb.HasTrigger("TR_Stock_Compras"));

            entity.Property(e => e.PrecioC).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.PrecioV).HasColumnType("decimal(6, 2)");

            entity.HasOne(d => d.IdComprasNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdCompras)
                .HasConstraintName("FK_DetalleCompra_Compra");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleCompra_Producto");
        });

        modelBuilder.Entity<DetalleFactura>(entity =>
        {
            entity.HasKey(e => e.IdDetalleFactura)
                .HasName("PKIdDetalleVentas")
                .IsClustered(false);

            entity.ToTable("DetalleFactura", tb => tb.HasTrigger("TR_StockFactura"));

            entity.Property(e => e.Precio).HasColumnType("decimal(6, 2)");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleFactura_Facturacion");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleFactura_Producto1");
        });

        modelBuilder.Entity<Facturacion>(entity =>
        {
            entity.HasKey(e => e.IdFactura)
                .HasName("PKIdFactura")
                .IsClustered(false);

            entity.ToTable("Facturacion");

            entity.Property(e => e.DescuentoFactura).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.FechaFactura).HasColumnType("date");
            entity.Property(e => e.TotaFactura).HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Facturacions)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Facturacion_Cliente1");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Facturacions)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Facturacion_Usuario1");
        });

        modelBuilder.Entity<ImprimirFacturaCompra>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ImprimirFactura_Compras");

            entity.Property(e => e.ApellidoPersona).HasMaxLength(25);
            entity.Property(e => e.Categoria).HasMaxLength(20);
            entity.Property(e => e.CodProducto).HasMaxLength(30);
            entity.Property(e => e.CorreoProveedor).HasMaxLength(25);
            entity.Property(e => e.DireccionProveedor).HasMaxLength(50);
            entity.Property(e => e.Distintivo).HasMaxLength(50);
            entity.Property(e => e.Estado).HasMaxLength(10);
            entity.Property(e => e.FechaCompras).HasColumnType("date");
            entity.Property(e => e.NombrePersona).HasMaxLength(25);
            entity.Property(e => e.PrecioC).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.TotalGeneral).HasColumnType("decimal(12, 2)");
        });

        modelBuilder.Entity<ImprimirFacturaVenta>().HasNoKey();

        modelBuilder.Entity<ImprimirFacturaVenta>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ImprimirFactura_Ventas");

            entity.Property(e => e.ApellidoPersona).HasMaxLength(25);
            entity.Property(e => e.Categoria).HasMaxLength(20);
            entity.Property(e => e.CodProducto).HasMaxLength(30);
            entity.Property(e => e.DireccionCliente).HasMaxLength(50);
            entity.Property(e => e.Distintivo).HasMaxLength(50);
            entity.Property(e => e.FechaFactura).HasColumnType("date");
            entity.Property(e => e.NombrePersona).HasMaxLength(25);
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.TotaFactura).HasColumnType("decimal(12, 2)");
        });

        modelBuilder.Entity<InformacionClienteFactura>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Informacion_Cliente_Facturas");
            entity.Property(e => e.Cedula).HasMaxLength(16);
            entity.Property(e => e.ApellidoPersona).HasMaxLength(25);
            entity.Property(e => e.DescuentoFactura).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.DireccionCliente).HasMaxLength(50);
            entity.Property(e => e.FechaFactura).HasColumnType("date");
            entity.Property(e => e.NombrePersona).HasMaxLength(25);
            entity.Property(e => e.TotaFactura).HasColumnType("decimal(12, 2)");
        });

        modelBuilder.Entity<InformacionProveedorCompra>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Informacion_Proveedor_Compras");

            entity.Property(e => e.ApellidoPersona).HasMaxLength(25);
            entity.Property(e => e.CorreoProveedor).HasMaxLength(25);
            entity.Property(e => e.DireccionProveedor).HasMaxLength(50);
            entity.Property(e => e.Estado).HasMaxLength(10);
            entity.Property(e => e.FechaCompras).HasColumnType("date");
            entity.Property(e => e.NombrePersona).HasMaxLength(25);
            entity.Property(e => e.PrecioC).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.PrecioV).HasColumnType("decimal(6, 2)");
        });

        modelBuilder.Entity<InformacionUsuarioFactura>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Informacion_Usuario_Facturas");
            entity.Property(e => e.Cedula).HasMaxLength(16);
            entity.Property(e => e.ApellidoPersona).HasMaxLength(25);
            entity.Property(e => e.DescuentoFactura).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Email).HasMaxLength(25);
            entity.Property(e => e.Estado).HasMaxLength(10);
            entity.Property(e => e.FechaFactura).HasColumnType("date");
            entity.Property(e => e.NombrePersona).HasMaxLength(25);
            entity.Property(e => e.Precio).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.TipoUsuario).HasMaxLength(25);
            entity.Property(e => e.TotaFactura).HasColumnType("decimal(12, 2)");
        });

        modelBuilder.Entity<InventarioProducto>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("InventarioProductos");

            entity.Property(e => e.Categoria).HasMaxLength(20);
            entity.Property(e => e.CodProducto).HasMaxLength(30);
            entity.Property(e => e.Color).HasMaxLength(15);
            entity.Property(e => e.Distintivo).HasMaxLength(50);
            entity.Property(e => e.Marca).HasMaxLength(15);
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Talla)
                .HasMaxLength(5)
                .IsUnicode(false);
        });

        modelBuilder.Entity<InventarioProductosP>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("InventarioProductosP");

            entity.Property(e => e.CodProducto).HasMaxLength(30);
            entity.Property(e => e.Distintivo).HasMaxLength(50);
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(6, 2)");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca)
                .HasName("PK15")
                .IsClustered(false);

            entity.ToTable("Marca");

            entity.Property(e => e.IdMarca).HasMaxLength(10);
            entity.Property(e => e.Marca1)
                .HasMaxLength(15)
                .HasColumnName("Marca");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PKCedulaPersona")
                .IsClustered(false);

            entity.ToTable("Persona", tb => tb.HasTrigger("VerificarNulos"));

            entity.Property(e => e.ApellidoPersona).HasMaxLength(25);
            entity.Property(e => e.Cedula).HasMaxLength(16);
            entity.Property(e => e.NombrePersona).HasMaxLength(25);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto)
                .HasName("PKCodProducto")
                .IsClustered(false);

            entity.ToTable("Producto", tb => tb.HasTrigger("VerificarPrecioPositivo"));

            entity.Property(e => e.CodProducto).HasMaxLength(30);
            entity.Property(e => e.Distintivo).HasMaxLength(50);
            entity.Property(e => e.IdCategoria).HasMaxLength(10);
            entity.Property(e => e.IdColor).HasMaxLength(10);
            entity.Property(e => e.IdMarca).HasMaxLength(10);
            entity.Property(e => e.IdTalla).HasMaxLength(10);
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(6, 2)");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Categoria");

            entity.HasOne(d => d.IdColorNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdColor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Color");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Marca");

            entity.HasOne(d => d.IdTallaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdTalla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Talla");
        });

        modelBuilder.Entity<ProductosCategorium>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Productos_Categoria");

            entity.Property(e => e.Categoria).HasMaxLength(20);
            entity.Property(e => e.CodProducto).HasMaxLength(30);
            entity.Property(e => e.Distintivo).HasMaxLength(50);
            entity.Property(e => e.IdCategoria).HasMaxLength(10);
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(6, 2)");
        });

        modelBuilder.Entity<ProductosColor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Productos_Color");

            entity.Property(e => e.CodProducto).HasMaxLength(30);
            entity.Property(e => e.Color).HasMaxLength(15);
            entity.Property(e => e.Distintivo).HasMaxLength(50);
            entity.Property(e => e.IdColor).HasMaxLength(10);
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(6, 2)");
        });

        modelBuilder.Entity<ProductosMarca>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Productos_Marca");

            entity.Property(e => e.CodProducto).HasMaxLength(30);
            entity.Property(e => e.Distintivo).HasMaxLength(50);
            entity.Property(e => e.IdMarca).HasMaxLength(10);
            entity.Property(e => e.Marca).HasMaxLength(15);
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(6, 2)");
        });
        
        modelBuilder.Entity<ProductosMasVendido>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ProductosMasVendidos");

            entity.Property(e => e.Categoria).HasMaxLength(20);
            entity.Property(e => e.CodProducto).HasMaxLength(30);
            entity.Property(e => e.Color).HasMaxLength(15);
            entity.Property(e => e.Marca).HasMaxLength(15);
            entity.Property(e => e.Talla).HasMaxLength(5);

        });

        modelBuilder.Entity<ProductosTalla>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Productos_Talla");

            entity.Property(e => e.CodProducto).HasMaxLength(30);
            entity.Property(e => e.Distintivo).HasMaxLength(50);
            entity.Property(e => e.IdTalla).HasMaxLength(10);
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Talla)
                .HasMaxLength(5)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor)
                .HasName("PKCedulaProveedor")
                .IsClustered(false);

            entity.ToTable("Proveedor");

            entity.Property(e => e.IdProveedor).ValueGeneratedNever();
            entity.Property(e => e.CorreoProveedor).HasMaxLength(25);
            entity.Property(e => e.DireccionProveedor).HasMaxLength(50);
            entity.Property(e => e.Estado).HasMaxLength(10);

            entity.HasOne(d => d.IdProveedorNavigation).WithOne(p => p.Proveedor)
                .HasForeignKey<Proveedor>(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proveedor_Persona");
        });

        modelBuilder.Entity<StockProximoAgotarse>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("StockProximoAgotarse");

            entity.Property(e => e.CodProducto).HasMaxLength(30);
            entity.Property(e => e.Distintivo).HasMaxLength(50);
        });

        modelBuilder.Entity<Talla>(entity =>
        {
            entity.HasKey(e => e.IdTalla)
                .HasName("PK13")
                .IsClustered(false);

            entity.ToTable("Talla");

            entity.Property(e => e.IdTalla).HasMaxLength(10);
            entity.Property(e => e.Talla1)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("Talla");
        });

        modelBuilder.Entity<TodasFacturasCompra>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TodasFacturas_Compras");

            entity.Property(e => e.ApellidoPersona).HasMaxLength(25);
            entity.Property(e => e.Cedula).HasMaxLength(16);
            entity.Property(e => e.FechaCompras).HasColumnType("date");
            entity.Property(e => e.NombrePersona).HasMaxLength(25);
            entity.Property(e => e.TotalGeneral).HasColumnType("decimal(12, 2)");
        });

        modelBuilder.Entity<TodasFacturasVenta>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TodasFacturas_Ventas");

            entity.Property(e => e.ApellidoPersona).HasMaxLength(25);
            entity.Property(e => e.Categoria).HasMaxLength(20);
            entity.Property(e => e.Cedula).HasMaxLength(16);
            entity.Property(e => e.CodProducto).HasMaxLength(30);
            entity.Property(e => e.DireccionCliente).HasMaxLength(50);
            entity.Property(e => e.Distintivo).HasMaxLength(50);
            entity.Property(e => e.FechaFactura).HasColumnType("date");
            entity.Property(e => e.NombrePersona).HasMaxLength(25);
            entity.Property(e => e.Precio).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.TotaFactura).HasColumnType("decimal(12, 2)");
        });

        modelBuilder.Entity<TotalFacturaVenta>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TotalFactura_Ventas");

            entity.Property(e => e.TotalFactura).HasColumnType("numeric(38, 2)");
        });

        


        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario)
                .HasName("PKCedulaUsuario")
                .IsClustered(false);

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).ValueGeneratedNever();
            entity.Property(e => e.Contraseña).HasMaxLength(25);
            entity.Property(e => e.Email).HasMaxLength(25);
            entity.Property(e => e.Estado).HasMaxLength(10);
            entity.Property(e => e.TipoUsuario).HasMaxLength(25);
            entity.Property(e => e.Usuario1)
                .HasMaxLength(20)
                .HasColumnName("Usuario");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Persona");
        });

        modelBuilder.Entity<VentasAño>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VentasAÑO");

            entity.Property(e => e.Subtotal).HasColumnType("decimal(38, 2)");
        });

        modelBuilder.Entity<VentasMe>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VentasMES");

            entity.Property(e => e.Subtotal).HasColumnType("decimal(38, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
