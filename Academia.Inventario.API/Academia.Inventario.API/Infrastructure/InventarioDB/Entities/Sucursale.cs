using System;
using System.Collections.Generic;

namespace Academia.Inventario.API.Infrastructure.InventarioDB.Entities;

public partial class Sucursale
{
    public int SucursalId { get; set; }

    public string? Nombre { get; set; }

    public bool? Activo { get; set; }

    public int? UsuarioCreacionId { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual Usuario? UsuarioCreacion { get; set; }

    public virtual Usuario? UsuarioModificacion { get; set; }
}
