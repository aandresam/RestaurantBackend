using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RestaurantBackend.Infrastructure.Persistence.Entities;

namespace RestaurantBackend.Infrastructure.Persistence;

public partial class RestaurantDbContext : DbContext
{
    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<Mesero> Meseros { get; set; }

    public virtual DbSet<Supervisor> Supervisors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("RESTAURANTE")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("SYS_C008306");

            entity.ToTable("CLIENTE");

            entity.HasIndex(e => e.Identificacion, "SYS_C008307").IsUnique();

            entity.Property(e => e.IdCliente)
                .HasPrecision(10)
                .HasColumnName("ID_CLIENTE");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("APELLIDOS");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("SYSTIMESTAMP ")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DIRECCION");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("IDENTIFICACION");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRES");
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasColumnName("UPDATED_AT");
        });

        modelBuilder.Entity<DetalleFactura>(entity =>
        {
            entity.HasKey(e => e.IdDetalleFactura).HasName("SYS_C008343");

            entity.ToTable("DETALLE_FACTURA");

            entity.HasIndex(e => e.IdFactura, "IDX_DETALLE_FACTURA");

            entity.HasIndex(e => e.IdSupervisor, "IDX_DETALLE_SUPERVISOR");

            entity.Property(e => e.IdDetalleFactura)
                .HasPrecision(10)
                .HasColumnName("ID_DETALLE_FACTURA");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("SYSTIMESTAMP ")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.IdFactura)
                .HasPrecision(10)
                .HasColumnName("ID_FACTURA");
            entity.Property(e => e.IdSupervisor)
                .HasPrecision(10)
                .HasColumnName("ID_SUPERVISOR");
            entity.Property(e => e.Plato)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("PLATO");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasColumnName("UPDATED_AT");
            entity.Property(e => e.Valor)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("VALOR");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DETALLE_FACTURA");

            entity.HasOne(d => d.IdSupervisorNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.IdSupervisor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DETALLE_SUPERVISOR");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("SYS_C008332");

            entity.ToTable("FACTURA");

            entity.HasIndex(e => e.IdCliente, "IDX_FACTURA_CLIENTE");

            entity.HasIndex(e => e.Fecha, "IDX_FACTURA_FECHA");

            entity.HasIndex(e => e.IdMesero, "IDX_FACTURA_MESERO");

            entity.HasIndex(e => e.NroFactura, "SYS_C008333").IsUnique();

            entity.Property(e => e.IdFactura)
                .HasPrecision(10)
                .HasColumnName("ID_FACTURA");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("SYSTIMESTAMP ")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.Fecha)
                .HasColumnType("DATE")
                .HasColumnName("FECHA");
            entity.Property(e => e.IdCliente)
                .HasPrecision(10)
                .HasColumnName("ID_CLIENTE");
            entity.Property(e => e.IdMesa)
                .HasPrecision(10)
                .HasColumnName("ID_MESA");
            entity.Property(e => e.IdMesero)
                .HasPrecision(10)
                .HasColumnName("ID_MESERO");
            entity.Property(e => e.NroFactura)
                .HasPrecision(10)
                .HasColumnName("NRO_FACTURA");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasColumnName("UPDATED_AT");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FACTURA_CLIENTE");

            entity.HasOne(d => d.IdMesaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdMesa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FACTURA_MESA");

            entity.HasOne(d => d.IdMeseroNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdMesero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FACTURA_MESERO");
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.IdMesa).HasName("SYS_C008323");

            entity.ToTable("MESA");

            entity.HasIndex(e => e.NroMesa, "SYS_C008324").IsUnique();

            entity.Property(e => e.IdMesa)
                .HasPrecision(10)
                .HasColumnName("ID_MESA");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("SYSTIMESTAMP ")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.NroMesa)
                .HasPrecision(10)
                .HasColumnName("NRO_MESA");
            entity.Property(e => e.Puestos)
                .HasPrecision(10)
                .HasColumnName("PUESTOS");
            entity.Property(e => e.Reservada)
                .HasColumnType("NUMBER(1)")
                .HasColumnName("RESERVADA");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasColumnName("UPDATED_AT");
        });

        modelBuilder.Entity<Mesero>(entity =>
        {
            entity.HasKey(e => e.IdMesero).HasName("SYS_C008312");

            entity.ToTable("MESERO");

            entity.Property(e => e.IdMesero)
                .HasPrecision(10)
                .HasColumnName("ID_MESERO");
            entity.Property(e => e.Antiguedad)
                .HasPrecision(10)
                .HasColumnName("ANTIGUEDAD");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("APELLIDOS");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("SYSTIMESTAMP ")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.Edad)
                .HasPrecision(10)
                .HasColumnName("EDAD");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRES");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasColumnName("UPDATED_AT");
        });

        modelBuilder.Entity<Supervisor>(entity =>
        {
            entity.HasKey(e => e.IdSupervisor).HasName("SYS_C008317");

            entity.ToTable("SUPERVISOR");

            entity.Property(e => e.IdSupervisor)
                .HasPrecision(10)
                .HasColumnName("ID_SUPERVISOR");
            entity.Property(e => e.Antiguedad)
                .HasPrecision(10)
                .HasColumnName("ANTIGUEDAD");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("APELLIDOS");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("SYSTIMESTAMP ")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.Edad)
                .HasPrecision(10)
                .HasColumnName("EDAD");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRES");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasColumnName("UPDATED_AT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
