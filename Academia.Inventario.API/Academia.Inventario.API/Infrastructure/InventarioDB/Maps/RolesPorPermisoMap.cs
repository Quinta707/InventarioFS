using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using Farsiman.Domain.Core.Standard.Common;
using Microsoft.EntityFrameworkCore;

namespace Academia.Inventario.API.Infrastructure.InventarioDB.Maps
{
    public class RolesPorPermisoMap : IEntityTypeConfiguration<RolesPorPermiso>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RolesPorPermiso> builder)
        {
            builder.HasKey(e => e.RolPorPermisoId).HasName("PK_RolesPorPermiso_RolPorPermisoID");
            builder.ToTable("RolesPorPermiso");

            builder.Property(e => e.RolPorPermisoId).HasColumnName("RolPorPermisoID");
            builder.Property(e => e.Activo).HasDefaultValue(true);
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.PermisoId).HasColumnName("PermisoID");
            builder.Property(e => e.RolId).HasColumnName("RolID");
            builder.Property(e => e.UsuarioCreacionId).HasColumnName("UsuarioCreacionID");
            builder.Property(e => e.UsuarioModificacionId).HasColumnName("UsuarioModificacionID");

            builder.HasOne(d => d.Permiso).WithMany(p => p.RolesPorPermisos)
                .HasForeignKey(d => d.PermisoId)
                .HasConstraintName("FK_RolesPorPermiso_PermisoID");

            builder.HasOne(d => d.Rol).WithMany(p => p.RolesPorPermisos)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK_RolesPorPermiso_RolID");

            builder.HasOne(d => d.UsuarioCreacion).WithMany(p => p.RolesPorPermisoUsuarioCreacions)
                .HasForeignKey(d => d.UsuarioCreacionId)
                .HasConstraintName("FK_RolesPorPermiso_UsuarioCreacionID");

            builder.HasOne(d => d.UsuarioModificacion).WithMany(p => p.RolesPorPermisoUsuarioModificacions)
                .HasForeignKey(d => d.UsuarioModificacionId)
                .HasConstraintName("FK_RolesPorPermiso_UsuarioModificacionID");
        }
    }
}
