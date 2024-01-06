using Academia.Inventario.API._Common;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Academia.Inventario.API.Infrastructure.InventarioDB.Entities;

public partial class SalidasInventarioDetalle
{
    public int SalidaInventarioDetalleId { get; set; }

    public int? SalidaInventarioId { get; set; }

    public int? LoteId { get; set; }

    public int? Cantidad { get; set; }

    public bool? Activo { get; set; }

    public int? UsuarioCreacionId { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual Lote? Lote { get; set; }

    public virtual SalidasInventario? SalidaInventario { get; set; }

    public virtual Usuario? UsuarioCreacion { get; set; }

    public virtual Usuario? UsuarioModificacion { get; set; }

    public class SalidaInventarioDetalleValidation : AbstractValidator<SalidasInventarioDetalle>
    {
        public SalidaInventarioDetalleValidation()
        {
            RuleFor(r => r.SalidaInventarioId).NotEmpty().WithMessage(Mensajes.Campos_Vacios("SalidaInventarioId"));
            RuleFor(r => r.LoteId).NotEmpty().WithMessage(Mensajes.Campos_Vacios("LoteId"));
            RuleFor(r => r.Cantidad).NotEmpty().WithMessage(Mensajes.Campos_Vacios("Cantidad"));
        }
    }
}
