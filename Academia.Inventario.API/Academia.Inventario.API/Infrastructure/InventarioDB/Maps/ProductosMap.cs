using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using Farsiman.Domain.Core.Standard.Common;
using Microsoft.EntityFrameworkCore;

namespace Academia.Inventario.API.Infrastructure.InventarioDB.Maps
{
    public class ProductosMap : IEntityTypeConfiguration<Producto>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Producto> builder)
        {
            builder.HasKey(e => e.ProductoId).HasName("PK_Productos_ProductoID");

            builder.Property(e => e.ProductoId).HasColumnName("ProductoID");
            builder.Property(e => e.Activo).HasDefaultValue(true);
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.UsuarioCreacionId).HasColumnName("UsuarioCreacionID");
            builder.Property(e => e.UsuarioModificacionId).HasColumnName("UsuarioModificacionID");

            builder.HasOne(d => d.UsuarioCreacion).WithMany(p => p.ProductoUsuarioCreacions)
                .HasForeignKey(d => d.UsuarioCreacionId)
                .HasConstraintName("FK_Productos_UsuarioCreacionID");

            builder.HasOne(d => d.UsuarioModificacion).WithMany(p => p.ProductoUsuarioModificacions)
                .HasForeignKey(d => d.UsuarioModificacionId)
                .HasConstraintName("FK_Productos_UsuarioModificacionID");
        }
    }
}
