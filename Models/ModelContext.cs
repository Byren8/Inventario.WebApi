using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Inventario.DataAcces_.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<InvActivo> InvActivos { get; set; }

    public virtual DbSet<InvAlmacen> InvAlmacens { get; set; }

    public virtual DbSet<InvAsignacionActivo> InvAsignacionActivos { get; set; }

    public virtual DbSet<InvCategoria> InvCategorias { get; set; }

    public virtual DbSet<InvDepartamento> InvDepartamentos { get; set; }

    public virtual DbSet<InvDetalleRecepcion> InvDetalleRecepcions { get; set; }

    public virtual DbSet<InvEmpleado> InvEmpleados { get; set; }

    public virtual DbSet<InvInventarioKardex> InvInventarioKardices { get; set; }

    public virtual DbSet<InvMunicipio> InvMunicipios { get; set; }

    public virtual DbSet<InvProducto> InvProductos { get; set; }

    public virtual DbSet<InvProveedore> InvProveedores { get; set; }

    public virtual DbSet<InvPuesto> InvPuestos { get; set; }

    public virtual DbSet<InvRecepcion> InvRecepcions { get; set; }

    public virtual DbSet<InvTipoDocumentoSalida> InvTipoDocumentoSalida { get; set; }

    public virtual DbSet<InvTrasladoAlmacen> InvTrasladoAlmacens { get; set; }

    public virtual DbSet<InvUnidadDeMedidum> InvUnidadDeMedidas { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
      //  => optionsBuilder.UseOracle("User Id=inventario;password=inv123;Data Source=localhost:1521/orcl;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("INVENTARIO")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<InvActivo>(entity =>
        {
            entity.HasKey(e => e.ActIdActivos).HasName("SYS_C008869");

            entity.ToTable("INV_ACTIVOS");

            entity.Property(e => e.ActIdActivos)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ACT_ID_ACTIVOS");
            entity.Property(e => e.ActCantidadActivo)
                .HasColumnType("NUMBER")
                .HasColumnName("ACT_CANTIDAD_ACTIVO");
            entity.Property(e => e.ActCostoActivo)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("ACT_COSTO_ACTIVO");
            entity.Property(e => e.ActDescActivo)
                .HasMaxLength(275)
                .IsUnicode(false)
                .HasColumnName("ACT_DESC_ACTIVO");
            entity.Property(e => e.ActFechaAdquisicion)
                .HasColumnType("DATE")
                .HasColumnName("ACT_FECHA_ADQUISICION");
            entity.Property(e => e.ActNombreActivo)
                .HasMaxLength(175)
                .IsUnicode(false)
                .HasColumnName("ACT_NOMBRE_ACTIVO");
            entity.Property(e => e.AsiIdAsignacion)
                .HasColumnType("NUMBER")
                .HasColumnName("ASI_ID_ASIGNACION");

            entity.HasOne(d => d.AsiIdAsignacionNavigation).WithMany(p => p.InvActivos)
                .HasForeignKey(d => d.AsiIdAsignacion)
                .HasConstraintName("FK_ASIGNACION_ACTIVOS");
        });

        modelBuilder.Entity<InvAlmacen>(entity =>
        {
            entity.HasKey(e => e.AlmIdAlmacen).HasName("SYS_C008882");

            entity.ToTable("INV_ALMACEN");

            entity.Property(e => e.AlmIdAlmacen)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ALM_ID_ALMACEN");
            entity.Property(e => e.AlmDireccion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("ALM_DIRECCION");
            entity.Property(e => e.AlmNombreAlmacen)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("ALM_NOMBRE_ALMACEN");
        });

        modelBuilder.Entity<InvAsignacionActivo>(entity =>
        {
            entity.HasKey(e => e.AsiIdAsignacion).HasName("SYS_C008867");

            entity.ToTable("INV_ASIGNACION_ACTIVOS");

            entity.Property(e => e.AsiIdAsignacion)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ASI_ID_ASIGNACION");
            entity.Property(e => e.AsiFechaAsignacion)
                .HasColumnType("DATE")
                .HasColumnName("ASI_FECHA_ASIGNACION");
            entity.Property(e => e.EmpIdEmpleado)
                .HasColumnType("NUMBER")
                .HasColumnName("EMP_ID_EMPLEADO");

            entity.HasOne(d => d.EmpIdEmpleadoNavigation).WithMany(p => p.InvAsignacionActivos)
                .HasForeignKey(d => d.EmpIdEmpleado)
                .HasConstraintName("FK_EMPLEADO_ASIGNACION");
        });

        modelBuilder.Entity<InvCategoria>(entity =>
        {
            entity.HasKey(e => e.CatIdCategoria).HasName("SYS_C008862");

            entity.ToTable("INV_CATEGORIAS");

            entity.Property(e => e.CatIdCategoria)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CAT_ID_CATEGORIA");
            entity.Property(e => e.CatDescCategoria)
                .HasMaxLength(175)
                .IsUnicode(false)
                .HasColumnName("CAT_DESC_CATEGORIA");
            entity.Property(e => e.CatNombreCategoria)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("CAT_NOMBRE_CATEGORIA");
        });

        modelBuilder.Entity<InvDepartamento>(entity =>
        {
            entity.HasKey(e => e.DepIdDepartamento).HasName("SYS_C008880");

            entity.ToTable("INV_DEPARTAMENTO");

            entity.Property(e => e.DepIdDepartamento)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("DEP_ID_DEPARTAMENTO");
            entity.Property(e => e.DepCodigoPostal)
                .HasColumnType("NUMBER")
                .HasColumnName("DEP_CODIGO_POSTAL");
            entity.Property(e => e.DepNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DEP_NOMBRE");
            entity.Property(e => e.MunIdMunicipio)
                .HasColumnType("NUMBER")
                .HasColumnName("MUN_ID_MUNICIPIO");

            entity.HasOne(d => d.MunIdMunicipioNavigation).WithMany(p => p.InvDepartamentos)
                .HasForeignKey(d => d.MunIdMunicipio)
                .HasConstraintName("FK_MUNICIPIO_DEPARTAMENTO");
        });

        modelBuilder.Entity<InvDetalleRecepcion>(entity =>
        {
            entity.HasKey(e => e.DetIdDetalle).HasName("SYS_C008878");

            entity.ToTable("INV_DETALLE_RECEPCION");

            entity.Property(e => e.DetIdDetalle)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("DET_ID_DETALLE");
            entity.Property(e => e.DetCantidadRecibida)
                .HasColumnType("NUMBER")
                .HasColumnName("DET_CANTIDAD_RECIBIDA");
            entity.Property(e => e.DetPrecioUnitarioCompra)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("DET_PRECIO_UNITARIO_COMPRA");
            entity.Property(e => e.DetSubtotal)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("DET_SUBTOTAL");
            entity.Property(e => e.RecIdRecepcion)
                .HasColumnType("NUMBER")
                .HasColumnName("REC_ID_RECEPCION");

            entity.HasOne(d => d.RecIdRecepcionNavigation).WithMany(p => p.InvDetalleRecepcions)
                .HasForeignKey(d => d.RecIdRecepcion)
                .HasConstraintName("FK_RECEPCION_DETALLE");
        });

        modelBuilder.Entity<InvEmpleado>(entity =>
        {
            entity.HasKey(e => e.EmpIdEmpleado).HasName("SYS_C008865");

            entity.ToTable("INV_EMPLEADO");

            entity.Property(e => e.EmpIdEmpleado)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("EMP_ID_EMPLEADO");
            entity.Property(e => e.EmpApellido)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("EMP_APELLIDO");
            entity.Property(e => e.EmpCodigoEmpleado)
                .HasColumnType("NUMBER")
                .HasColumnName("EMP_CODIGO_EMPLEADO");
            entity.Property(e => e.EmpNombre)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("EMP_NOMBRE");
            entity.Property(e => e.EmpTelefono)
                .HasColumnType("NUMBER")
                .HasColumnName("EMP_TELEFONO");
            entity.Property(e => e.PueIdPuesto)
                .HasColumnType("NUMBER")
                .HasColumnName("PUE_ID_PUESTO");

            entity.HasOne(d => d.PueIdPuestoNavigation).WithMany(p => p.InvEmpleados)
                .HasForeignKey(d => d.PueIdPuesto)
                .HasConstraintName("FK_PUESTO_EMPLEADO");
        });

        modelBuilder.Entity<InvInventarioKardex>(entity =>
        {
            entity.HasKey(e => e.IneIdInventario).HasName("SYS_C008886");

            entity.ToTable("INV_INVENTARIO_KARDEX");

            entity.Property(e => e.IneIdInventario)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("INE_ID_INVENTARIO");
            entity.Property(e => e.AlmIdAlmacen)
                .HasColumnType("NUMBER")
                .HasColumnName("ALM_ID_ALMACEN");
            entity.Property(e => e.IneCantidadStock)
                .HasColumnType("NUMBER")
                .HasColumnName("INE_CANTIDAD_STOCK");
            entity.Property(e => e.IneFechaIngreso)
                .HasColumnType("DATE")
                .HasColumnName("INE_FECHA_INGRESO");
            entity.Property(e => e.IneFechaSalida)
                .HasColumnType("DATE")
                .HasColumnName("INE_FECHA_SALIDA");
            entity.Property(e => e.InvDetalleRecepcion)
                .HasColumnType("NUMBER")
                .HasColumnName("INV_DETALLE_RECEPCION");
            entity.Property(e => e.InvTipoDocumentoSalida)
                .HasColumnType("NUMBER")
                .HasColumnName("INV_TIPO_DOCUMENTO_SALIDA");
            entity.Property(e => e.ProIdProducto)
                .HasColumnType("NUMBER")
                .HasColumnName("PRO_ID_PRODUCTO");
            entity.Property(e => e.PrvIdProveedor)
                .HasColumnType("NUMBER")
                .HasColumnName("PRV_ID_PROVEEDOR");

            entity.HasOne(d => d.AlmIdAlmacenNavigation).WithMany(p => p.InvInventarioKardices)
                .HasForeignKey(d => d.AlmIdAlmacen)
                .HasConstraintName("FK_ALMACEN_INVENTARIO");

            entity.HasOne(d => d.InvDetalleRecepcionNavigation).WithMany(p => p.InvInventarioKardices)
                .HasForeignKey(d => d.InvDetalleRecepcion)
                .HasConstraintName("FK_DETALLE_RECEPCION_INVENTARIO");

            entity.HasOne(d => d.InvTipoDocumentoSalidaNavigation).WithMany(p => p.InvInventarioKardices)
                .HasForeignKey(d => d.InvTipoDocumentoSalida)
                .HasConstraintName("FK_TIPO_DOCUMENTO_INVENTARIO");

            entity.HasOne(d => d.ProIdProductoNavigation).WithMany(p => p.InvInventarioKardices)
                .HasForeignKey(d => d.ProIdProducto)
                .HasConstraintName("FK_PRODUCTO_INVENTARIO");

            entity.HasOne(d => d.PrvIdProveedorNavigation).WithMany(p => p.InvInventarioKardices)
                .HasForeignKey(d => d.PrvIdProveedor)
                .HasConstraintName("FK_PROVEEDOR_INVENTARIO");
        });

        modelBuilder.Entity<InvMunicipio>(entity =>
        {
            entity.HasKey(e => e.MunIdMunicipio).HasName("SYS_C008861");

            entity.ToTable("INV_MUNICIPIO");

            entity.Property(e => e.MunIdMunicipio)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("MUN_ID_MUNICIPIO");
            entity.Property(e => e.MunDescMunicipio)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("MUN_DESC_MUNICIPIO");
        });

        modelBuilder.Entity<InvProducto>(entity =>
        {
            entity.HasKey(e => e.ProIdProducto).HasName("SYS_C008875");

            entity.ToTable("INV_PRODUCTO");

            entity.Property(e => e.ProIdProducto)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PRO_ID_PRODUCTO");
            entity.Property(e => e.ProImagenDeProducto)
                .HasColumnType("BLOB")
                .HasColumnName("PRO_IMAGEN_DE_PRODUCTO");
            entity.Property(e => e.ProNombreProducto)
                .HasMaxLength(175)
                .IsUnicode(false)
                .HasColumnName("PRO_NOMBRE_PRODUCTO");
            entity.Property(e => e.ProPrecioDeCompra)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("PRO_PRECIO_DE_COMPRA");
            entity.Property(e => e.ProPrecioVenta)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("PRO_PRECIO_VENTA");
            entity.Property(e => e.ProStockMaximo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("PRO_STOCK_MAXIMO");
            entity.Property(e => e.ProStockMinimo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("PRO_STOCK_MINIMO");
            entity.Property(e => e.PrvIdProveedor)
                .HasColumnType("NUMBER")
                .HasColumnName("PRV_ID_PROVEEDOR");
        });

        modelBuilder.Entity<InvProveedore>(entity =>
        {
            entity.HasKey(e => e.PrvIdProveedor).HasName("SYS_C008873");

            entity.ToTable("INV_PROVEEDORES");

            entity.Property(e => e.PrvIdProveedor)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PRV_ID_PROVEEDOR");
            entity.Property(e => e.PrvDireccionProveedor)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("PRV_DIRECCION_PROVEEDOR");
            entity.Property(e => e.PrvNitProveedor)
                .HasColumnType("NUMBER")
                .HasColumnName("PRV_NIT_PROVEEDOR");
            entity.Property(e => e.PrvNombreProveedor)
                .HasMaxLength(175)
                .IsUnicode(false)
                .HasColumnName("PRV_NOMBRE_PROVEEDOR");
            entity.Property(e => e.UndIdUnidadDeMedida)
                .HasColumnType("NUMBER")
                .HasColumnName("UND_ID_UNIDAD_DE_MEDIDA");

            entity.HasOne(d => d.UndIdUnidadDeMedidaNavigation).WithMany(p => p.InvProveedores)
                .HasForeignKey(d => d.UndIdUnidadDeMedida)
                .HasConstraintName("FK_MEDIDA_PROVEEDOR");
        });

        modelBuilder.Entity<InvPuesto>(entity =>
        {
            entity.HasKey(e => e.PueIdPuesto).HasName("SYS_C008863");

            entity.ToTable("INV_PUESTO");

            entity.Property(e => e.PueIdPuesto)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PUE_ID_PUESTO");
            entity.Property(e => e.PueDescDepto)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("PUE_DESC_DEPTO");
            entity.Property(e => e.PueFechaCreacion)
                .HasColumnType("DATE")
                .HasColumnName("PUE_FECHA_CREACION");
        });

        modelBuilder.Entity<InvRecepcion>(entity =>
        {
            entity.HasKey(e => e.RecIdRecepcion).HasName("SYS_C008876");

            entity.ToTable("INV_RECEPCION");

            entity.Property(e => e.RecIdRecepcion)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("REC_ID_RECEPCION");
            entity.Property(e => e.PrvIdProveedor)
                .HasColumnType("NUMBER")
                .HasColumnName("PRV_ID_PROVEEDOR");
            entity.Property(e => e.RecFechaRecepcion)
                .HasColumnType("DATE")
                .HasColumnName("REC_FECHA_RECEPCION");
            entity.Property(e => e.RecTotalFactura)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("REC_TOTAL_FACTURA");

            entity.HasOne(d => d.PrvIdProveedorNavigation).WithMany(p => p.InvRecepcions)
                .HasForeignKey(d => d.PrvIdProveedor)
                .HasConstraintName("FK_PROVEEDOR_RECEPCION");
        });

        modelBuilder.Entity<InvTipoDocumentoSalida>(entity =>
        {
            entity.HasKey(e => e.TipIdDocumento).HasName("SYS_C008864");

            entity.ToTable("INV_TIPO_DOCUMENTO_SALIDA");

            entity.Property(e => e.TipIdDocumento)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("TIP_ID_DOCUMENTO");
            entity.Property(e => e.TipDescripcionDocumento)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("TIP_DESCRIPCION_DOCUMENTO");
        });

        modelBuilder.Entity<InvTrasladoAlmacen>(entity =>
        {
            entity.HasKey(e => e.TraIdTraslado).HasName("SYS_C008883");

            entity.ToTable("INV_TRASLADO_ALMACEN");

            entity.Property(e => e.TraIdTraslado)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("TRA_ID_TRASLADO");
            entity.Property(e => e.AlmIdAlmacen)
                .HasColumnType("NUMBER")
                .HasColumnName("ALM_ID_ALMACEN");
            entity.Property(e => e.DepIdDepartamento)
                .HasColumnType("NUMBER")
                .HasColumnName("DEP_ID_DEPARTAMENTO");
            entity.Property(e => e.TraFechaTraslado)
                .HasColumnType("DATE")
                .HasColumnName("TRA_FECHA_TRASLADO");
            entity.Property(e => e.TraNombreTraslado)
                .HasMaxLength(275)
                .IsUnicode(false)
                .HasColumnName("TRA_NOMBRE_TRASLADO");

            entity.HasOne(d => d.AlmIdAlmacenNavigation).WithMany(p => p.InvTrasladoAlmacens)
                .HasForeignKey(d => d.AlmIdAlmacen)
                .HasConstraintName("FK_ALMACEN_TRASLADO");

            entity.HasOne(d => d.DepIdDepartamentoNavigation).WithMany(p => p.InvTrasladoAlmacens)
                .HasForeignKey(d => d.DepIdDepartamento)
                .HasConstraintName("FK_DEPARTAMENTO_TRASLADO");
        });

        modelBuilder.Entity<InvUnidadDeMedidum>(entity =>
        {
            entity.HasKey(e => e.UndIdUnidadDeMedida).HasName("SYS_C008871");

            entity.ToTable("INV_UNIDAD_DE_MEDIDA");

            entity.Property(e => e.UndIdUnidadDeMedida)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("UND_ID_UNIDAD_DE_MEDIDA");
            entity.Property(e => e.CatIdCategoria)
                .HasColumnType("NUMBER")
                .HasColumnName("CAT_ID_CATEGORIA");
            entity.Property(e => e.UndAbreviatura)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("UND_ABREVIATURA");
            entity.Property(e => e.UndNombre)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("UND_NOMBRE");

            entity.HasOne(d => d.CatIdCategoriaNavigation).WithMany(p => p.InvUnidadDeMedida)
                .HasForeignKey(d => d.CatIdCategoria)
                .HasConstraintName("FK_CATEGORIA_MEDIDA");
        });
        modelBuilder.HasSequence("INV_ACTIVOS_SEQUE");
        modelBuilder.HasSequence("INV_ALMACEN_SEQUE");
        modelBuilder.HasSequence("INV_ASIGNACION_ACTIVOS_SEQUE");
        modelBuilder.HasSequence("INV_CATEGORIAS_SEQUE");
        modelBuilder.HasSequence("INV_DEPARTAMENTO_SEQUE");
        modelBuilder.HasSequence("INV_DETALLE_RECEPCION_SEQUE");
        modelBuilder.HasSequence("INV_EMPLEADO_SEQUE");
        modelBuilder.HasSequence("INV_INVENTARIO_KARDEX_SEQUE");
        modelBuilder.HasSequence("INV_MUNICIPIO_SEQUE");
        modelBuilder.HasSequence("INV_PRODUCTO_SEQUE");
        modelBuilder.HasSequence("INV_PROVEEDORES_SEQUE");
        modelBuilder.HasSequence("INV_PUESTO_SEQUE");
        modelBuilder.HasSequence("INV_RECEPCION_SEQUE");
        modelBuilder.HasSequence("INV_TIPO_DOCUMENTO_SALIDA_SEQUE");
        modelBuilder.HasSequence("INV_TRASLADO_ALMACEN_SEQUE");
        modelBuilder.HasSequence("INV_UNIDAD_DE_MEDIDA_SEQUE");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
