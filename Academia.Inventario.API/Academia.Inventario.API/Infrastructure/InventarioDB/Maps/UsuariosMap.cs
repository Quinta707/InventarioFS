using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using Farsiman.Domain.Core.Standard.Common;
using Microsoft.EntityFrameworkCore;

namespace Academia.Inventario.API.Infrastructure.InventarioDB.Maps
{
    public class UsuariosMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(e => e.UsuarioId).HasName("PK_Usuarios_UsuariosID");

            builder.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            builder.Property(e => e.Clave).IsUnicode(false);
            builder.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            builder.Property(e => e.Activo).HasDefaultValue(true);
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.RolId).HasColumnName("RolID");
            builder.Property(e => e.UsuarioCreacionId).HasColumnName("UsuarioCreacionID");
            builder.Property(e => e.UsuarioModificacionId).HasColumnName("UsuarioModificacionID");

            builder.HasOne(d => d.Empleado).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.EmpleadoId)
                .HasConstraintName("FK_Usuarios_EmpleadoID");

            builder.HasOne(d => d.UsuarioCreacion).WithMany(p => p.InverseUsuarioCreacion)
                .HasForeignKey(d => d.UsuarioCreacionId)
                .HasConstraintName("FK_Usuarios_UsuarioCreacionID");

            builder.HasOne(d => d.UsuarioModificacion).WithMany(p => p.InverseUsuarioModificacion)
                .HasForeignKey(d => d.UsuarioModificacionId)
                .HasConstraintName("FK_Usuarios_UsuarioModificacionID");
        }
    }
}
