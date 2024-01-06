using System;
using System.Collections.Generic;

namespace Academia.Inventario.API.Infrastructure.InventarioDB.Entities;

public partial class RolesPorPermiso
{
    public int RolPorPermisoId { get; set; }

    public int? RolId { get; set; }

    public int? PermisoId { get; set; }

    public bool? Activo { get; set; }

    public int? UsuarioCreacionId { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual Permiso? Permiso { get; set; }

    public virtual Role? Rol { get; set; }

    public virtual Usuario? UsuarioCreacion { get; set; }

    public virtual Usuario? UsuarioModificacion { get; set; }
}
