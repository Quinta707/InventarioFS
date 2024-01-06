using System;
using System.Collections.Generic;

namespace Academia.Inventario.API.Infrastructure.InventarioDB.Entities;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string? Nombre { get; set; }

    public string? Clave { get; set; }

    public int? EmpleadoId { get; set; }

    public int? RolId { get; set; }

    public bool? Activo { get; set; }

    public int? UsuarioCreacionId { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual Empleado? Empleado { get; set; }

    public virtual ICollection<Empleado> EmpleadoUsuarioCreacions { get; set; } = new List<Empleado>();

    public virtual ICollection<Empleado> EmpleadoUsuarioModificacions { get; set; } = new List<Empleado>();

    public virtual ICollection<EstadoSalida> EstadoSalidaUsuarioCreacions { get; set; } = new List<EstadoSalida>();

    public virtual ICollection<EstadoSalida> EstadoSalidaUsuarioModificacions { get; set; } = new List<EstadoSalida>();

    public virtual ICollection<EstadosCivile> EstadosCivileUsuarioCreacions { get; set; } = new List<EstadosCivile>();

    public virtual ICollection<EstadosCivile> EstadosCivileUsuarioModificacions { get; set; } = new List<EstadosCivile>();

    public virtual ICollection<Usuario> InverseUsuarioCreacion { get; set; } = new List<Usuario>();

    public virtual ICollection<Usuario> InverseUsuarioModificacion { get; set; } = new List<Usuario>();

    public virtual ICollection<Lote> LoteUsuarioCreacions { get; set; } = new List<Lote>();

    public virtual ICollection<Lote> LoteUsuarioModificacions { get; set; } = new List<Lote>();

    public virtual ICollection<Permiso> PermisoUsuarioCreacions { get; set; } = new List<Permiso>();

    public virtual ICollection<Permiso> PermisoUsuarioModificacions { get; set; } = new List<Permiso>();

    public virtual ICollection<Producto> ProductoUsuarioCreacions { get; set; } = new List<Producto>();

    public virtual ICollection<Producto> ProductoUsuarioModificacions { get; set; } = new List<Producto>();

    public virtual ICollection<Role> RoleUsuarioCreacions { get; set; } = new List<Role>();

    public virtual ICollection<Role> RoleUsuarioModificacions { get; set; } = new List<Role>();

    public virtual ICollection<RolesPorPermiso> RolesPorPermisoUsuarioCreacions { get; set; } = new List<RolesPorPermiso>();

    public virtual ICollection<RolesPorPermiso> RolesPorPermisoUsuarioModificacions { get; set; } = new List<RolesPorPermiso>();

    public virtual ICollection<SalidasInventarioDetalle> SalidasInventarioDetalleUsuarioCreacions { get; set; } = new List<SalidasInventarioDetalle>();

    public virtual ICollection<SalidasInventarioDetalle> SalidasInventarioDetalleUsuarioModificacions { get; set; } = new List<SalidasInventarioDetalle>();

    public virtual ICollection<SalidasInventario> SalidasInventarioUsuarioCreacions { get; set; } = new List<SalidasInventario>();

    public virtual ICollection<SalidasInventario> SalidasInventarioUsuarioModificacions { get; set; } = new List<SalidasInventario>();

    public virtual ICollection<SalidasInventario> SalidasInventarioUsuarioRecibes { get; set; } = new List<SalidasInventario>();

    public virtual ICollection<Sucursale> SucursaleUsuarioCreacions { get; set; } = new List<Sucursale>();

    public virtual ICollection<Sucursale> SucursaleUsuarioModificacions { get; set; } = new List<Sucursale>();

    public virtual Usuario? UsuarioCreacion { get; set; }

    public virtual Usuario? UsuarioModificacion { get; set; }
}
