using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using Farsiman.Domain.Core.Standard.Common;
using Microsoft.EntityFrameworkCore;

namespace Academia.Inventario.API.Infrastructure.InventarioDB.Maps
{
    public class LotesMap : IEntityTypeConfiguration<Lote>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Lote> builder)
        {
            builder.HasKey(e => e.LoteId).HasName("PK_Lotes_LoteID");

            builder.Property(e => e.LoteId).HasColumnName("LoteID");
            builder.Property(e => e.CostoUnitario).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.Activo).HasDefaultValue(true);
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.FechaVencimiento).HasColumnType("datetime");
            builder.Property(e => e.ProductoId).HasColumnName("ProductoID");
            builder.Property(e => e.UsuarioCreacionId).HasColumnName("UsuarioCreacionID");
            builder.Property(e => e.UsuarioModificacionId).HasColumnName("UsuarioModificacionID");

            builder.HasOne(d => d.Producto).WithMany(p => p.Lotes)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK_Lotes_ProductoID");

            builder.HasOne(d => d.UsuarioCreacion).WithMany(p => p.LoteUsuarioCreacions)
                .HasForeignKey(d => d.UsuarioCreacionId)
                .HasConstraintName("FK_Lotes_UsuarioCreacionID");

            builder.HasOne(d => d.UsuarioModificacion).WithMany(p => p.LoteUsuarioModificacions)
                .HasForeignKey(d => d.UsuarioModificacionId)
                .HasConstraintName("FK_Lotes_UsuarioModificacionID");
        }
    }
}
