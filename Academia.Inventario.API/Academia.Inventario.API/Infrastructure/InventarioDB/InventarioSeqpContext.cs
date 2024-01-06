using System;
using System.Collections.Generic;
using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using Academia.Inventario.API.Infrastructure.InventarioDB.Maps;
using Microsoft.EntityFrameworkCore;

namespace Academia.Inventario.API.Infrastructure.InventarioDB;

public partial class InventarioSeqpContext : DbContext
{
    public InventarioSeqpContext(DbContextOptions<InventarioSeqpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<EstadoSalida> EstadoSalidas { get; set; }

    public virtual DbSet<EstadosCivile> EstadosCiviles { get; set; }

    public virtual DbSet<Lote> Lotes { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolesPorPermiso> RolesPorPermisos { get; set; }

    public virtual DbSet<SalidasInventario> SalidasInventarios { get; set; }

    public virtual DbSet<SalidasInventarioDetalle> SalidasInventarioDetalles { get; set; }

    public virtual DbSet<Sucursale> Sucursales { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmpleadosMap());
        modelBuilder.ApplyConfiguration(new EstadoSalidasMap());
        modelBuilder.ApplyConfiguration(new EstadosCivilesMap());
        modelBuilder.ApplyConfiguration(new LotesMap());
        modelBuilder.ApplyConfiguration(new PermisosMap());
        modelBuilder.ApplyConfiguration(new ProductosMap());
        modelBuilder.ApplyConfiguration(new RolesMap());
        modelBuilder.ApplyConfiguration(new RolesPorPermisoMap());
        modelBuilder.ApplyConfiguration(new SalidasInventarioMap());
        modelBuilder.ApplyConfiguration(new SalidasInventarioDetallesMap());
        modelBuilder.ApplyConfiguration(new SucursalesMap());
        modelBuilder.ApplyConfiguration(new UsuariosMap());
    }

}
