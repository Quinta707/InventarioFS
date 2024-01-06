using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using Farsiman.Domain.Core.Standard.Common;
using Microsoft.EntityFrameworkCore;

namespace Academia.Inventario.API.Infrastructure.InventarioDB.Maps
{
    public class SalidasInventarioDetallesMap :IEntityTypeConfiguration<SalidasInventarioDetalle>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SalidasInventarioDetalle> builder)
        {
            builder.HasKey(e => e.SalidaInventarioDetalleId).HasName("PK_SalidasInventarioDetalles_SalidaInventarioDetalleID");

            builder.Property(e => e.SalidaInventarioDetalleId).HasColumnName("SalidaInventarioDetalleID");
            builder.Property(e => e.Activo).HasDefaultValue(true);
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.LoteId).HasColumnName("LoteID");
            builder.Property(e => e.SalidaInventarioId).HasColumnName("SalidaInventarioID");
            builder.Property(e => e.UsuarioCreacionId).HasColumnName("UsuarioCreacionID");
            builder.Property(e => e.UsuarioModificacionId).HasColumnName("UsuarioModificacionID");

            builder.HasOne(d => d.Lote).WithMany(p => p.SalidasInventarioDetalles)
                .HasForeignKey(d => d.LoteId)
                .HasConstraintName("FK_SalidasInventarioDetalles_LoteID");

            builder.HasOne(d => d.SalidaInventario).WithMany(p => p.SalidasInventarioDetalles)
                .HasForeignKey(d => d.SalidaInventarioId)
                .HasConstraintName("FK_SalidasInventarioDetalles_SalidaInventarioID");

            builder.HasOne(d => d.UsuarioCreacion).WithMany(p => p.SalidasInventarioDetalleUsuarioCreacions)
                .HasForeignKey(d => d.UsuarioCreacionId)
                .HasConstraintName("FK_SalidasInventarioDetalles_UsuarioCreacionID");

            builder.HasOne(d => d.UsuarioModificacion).WithMany(p => p.SalidasInventarioDetalleUsuarioModificacions)
                .HasForeignKey(d => d.UsuarioModificacionId)
                .HasConstraintName("FK_SalidasInventarioDetalles_UsuarioModificacionID");
        }
    }
}
