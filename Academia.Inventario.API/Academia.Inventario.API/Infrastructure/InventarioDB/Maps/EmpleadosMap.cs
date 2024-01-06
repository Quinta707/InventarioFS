using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using Farsiman.Domain.Core.Standard.Common;
using Microsoft.EntityFrameworkCore;

namespace Academia.Inventario.API.Infrastructure.InventarioDB.Maps
{
    public class EmpleadosMap : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Empleado> builder)
        {
            builder.HasKey(e => e.EmpleadoId).HasName("PKEmpleados_EmpleadoID");

            builder.HasIndex(e => e.Identidad, "UQ__Empleado__5C06DCB4100B99BC").IsUnique();

            builder.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            builder.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.Activo).HasDefaultValue(true);
            builder.Property(e => e.EstadoCivilId).HasColumnName("EstadoCivilID");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.Genero)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            builder.Property(e => e.Identidad)
                .HasMaxLength(13)
                .IsUnicode(false);
            builder.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            builder.Property(e => e.UsuarioCreacionId).HasColumnName("UsuarioCreacionID");
            builder.Property(e => e.UsuarioModificacionId).HasColumnName("UsuarioModificacionID");

            builder.HasOne(d => d.EstadoCivil).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.EstadoCivilId)
                .HasConstraintName("FK_Empleados_EstadoCivilID");

            builder.HasOne(d => d.UsuarioCreacion).WithMany(p => p.EmpleadoUsuarioCreacions)
                .HasForeignKey(d => d.UsuarioCreacionId)
                .HasConstraintName("FK_Empleados_UsuarioCreacionID");

            builder.HasOne(d => d.UsuarioModificacion).WithMany(p => p.EmpleadoUsuarioModificacions)
                .HasForeignKey(d => d.UsuarioModificacionId)
                .HasConstraintName("FK_Empleados_UsuarioModificacionID");
        }
    }
}
