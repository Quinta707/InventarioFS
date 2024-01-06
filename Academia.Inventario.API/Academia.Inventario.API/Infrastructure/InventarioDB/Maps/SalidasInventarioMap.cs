using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using Farsiman.Domain.Core.Standard.Common;
using Microsoft.EntityFrameworkCore;

namespace Academia.Inventario.API.Infrastructure.InventarioDB.Maps
{
    public class SalidasInventarioMap : IEntityTypeConfiguration<SalidasInventario>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SalidasInventario> builder)
        {
            builder.HasKey(e => e.SalidaInventarioId).HasName("PK_SalidasInventario_SalidaInventarioID");
            builder.ToTable("SalidasInventario");

            builder.Property(e => e.SalidaInventarioId).HasColumnName("SalidaInventarioID");
            builder.Property(e => e.Activo).HasDefaultValue(true);
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.FechaRecibido).HasColumnType("datetime");
            builder.Property(e => e.FechaSalida).HasColumnType("datetime");
            builder.Property(e => e.SucursalId).HasColumnName("SucursalID");
            builder.Property(e => e.UsuarioCreacionId).HasColumnName("UsuarioCreacionID");
            builder.Property(e => e.UsuarioModificacionId).HasColumnName("UsuarioModificacionID");
            builder.Property(e => e.UsuarioRecibeId).HasColumnName("UsuarioRecibeID");
            builder.Property(e => e.EstadoId).HasColumnName("EstadoID").HasDefaultValue(1);

            builder.HasOne(d => d.UsuarioCreacion).WithMany(p => p.SalidasInventarioUsuarioCreacions)
                .HasForeignKey(d => d.UsuarioCreacionId)
                .HasConstraintName("FK_SalidasInventario_UsuarioCreacionID");

            builder.HasOne(d => d.UsuarioModificacion).WithMany(p => p.SalidasInventarioUsuarioModificacions)
                .HasForeignKey(d => d.UsuarioModificacionId)
                .HasConstraintName("FK_SalidasInventario_UsuarioModificacionID");

            builder.HasOne(d => d.UsuarioRecibe).WithMany(p => p.SalidasInventarioUsuarioRecibes)
                .HasForeignKey(d => d.UsuarioRecibeId)
                .HasConstraintName("FK_SalidasInventario_UsuarioRecibeID");
        }
    }
}
