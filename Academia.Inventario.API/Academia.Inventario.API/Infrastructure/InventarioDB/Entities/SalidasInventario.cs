using System;
using System.Collections.Generic;

namespace Academia.Inventario.API.Infrastructure.InventarioDB.Entities;

public partial class SalidasInventario
{
    public int SalidaInventarioId { get; set; }

    public int? SucursalId { get; set; }

    public DateTime? FechaSalida { get; set; }

    public int? Total { get; set; }

    public DateTime? FechaRecibido { get; set; }

    public int? UsuarioRecibeId { get; set; }

    public bool? Activo { get; set; }

    public int? UsuarioCreacionId { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? EstadoId { get; set; }

    public virtual ICollection<SalidasInventarioDetalle> SalidasInventarioDetalles { get; set; } = new List<SalidasInventarioDetalle>();

    public virtual Usuario? UsuarioCreacion { get; set; }

    public virtual Usuario? UsuarioModificacion { get; set; }

    public virtual Usuario? UsuarioRecibe { get; set; }
}
