using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using Farsiman.Domain.Core.Standard.Common;
using Microsoft.EntityFrameworkCore;

namespace Academia.Inventario.API.Infrastructure.InventarioDB.Maps
{
    public class EstadoSalidasMap : IEntityTypeConfiguration<EstadoSalida>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<EstadoSalida> builder)
        {
            builder.HasKey(e => e.EstadoId).HasName("PK_EstadoSalidas_EstadoID");

            builder.Property(e => e.EstadoId).HasColumnName("EstadoID");
            builder.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.Activo).HasDefaultValue(true);
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.UsuarioCreacionId).HasColumnName("UsuarioCreacionID");
            builder.Property(e => e.UsuarioModificacionId).HasColumnName("UsuarioModificacionID");

            builder.HasOne(d => d.UsuarioCreacion).WithMany(p => p.EstadoSalidaUsuarioCreacions)
                .HasForeignKey(d => d.UsuarioCreacionId)
                .HasConstraintName("FK_EstadoSalidas_UsuarioCreacionID");

            builder.HasOne(d => d.UsuarioModificacion).WithMany(p => p.EstadoSalidaUsuarioModificacions)
                .HasForeignKey(d => d.UsuarioModificacionId)
                .HasConstraintName("FK_EstadoSalidas_UsuarioModificacionID");
        }
    }
}
