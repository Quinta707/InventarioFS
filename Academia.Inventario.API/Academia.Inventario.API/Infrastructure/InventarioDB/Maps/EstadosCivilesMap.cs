using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using Farsiman.Domain.Core.Standard.Common;
using Microsoft.EntityFrameworkCore;

namespace Academia.Inventario.API.Infrastructure.InventarioDB.Maps
{
    public class EstadosCivilesMap : IEntityTypeConfiguration<EstadosCivile>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<EstadosCivile> builder)
        {
            builder.HasKey(e => e.EstadoCivilId).HasName("PK_EstadosCiviles_EstadoCivilID");

            builder.Property(e => e.EstadoCivilId).HasColumnName("EstadoCivilID");
            builder.Property(e => e.Descripcion)
                .HasMaxLength(40)
                .IsUnicode(false);
            builder.Property(e => e.Activo).HasDefaultValue(true);
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.UsuarioCreacionId).HasColumnName("UsuarioCreacionID");
            builder.Property(e => e.UsuarioModificacionId).HasColumnName("UsuarioModificacionID");

            builder.HasOne(d => d.UsuarioCreacion).WithMany(p => p.EstadosCivileUsuarioCreacions)
                .HasForeignKey(d => d.UsuarioCreacionId)
                .HasConstraintName("FK_EstadosCiviles_UsuarioCreacionID");

            builder.HasOne(d => d.UsuarioModificacion).WithMany(p => p.EstadosCivileUsuarioModificacions)
                .HasForeignKey(d => d.UsuarioModificacionId)
                .HasConstraintName("FK_EstadosCiviles_UsuarioModificacionID");
        }
    }
}
