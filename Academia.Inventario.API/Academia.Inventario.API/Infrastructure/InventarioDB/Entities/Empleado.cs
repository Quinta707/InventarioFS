using Academia.Inventario.API._Common;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Academia.Inventario.API.Infrastructure.InventarioDB.Entities;

public partial class Empleado
{
    public int EmpleadoId { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Identidad { get; set; }

    public string? Telefono { get; set; }

    public string? Genero { get; set; }

    public string? Direccion { get; set; }

    public int? EstadoCivilId { get; set; }

    public bool? Activo { get; set; }

    public int? UsuarioCreacionId { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual EstadosCivile? EstadoCivil { get; set; }

    public virtual Usuario? UsuarioCreacion { get; set; }

    public virtual Usuario? UsuarioModificacion { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

    public class EmpleadoValidator : AbstractValidator<Empleado>
    {
        public EmpleadoValidator()
        {
            RuleFor(r => r.Nombre).NotEmpty().WithMessage(Mensajes.Campos_Vacios("Nombres"));
            RuleFor(r => r.Apellido).NotEmpty().WithMessage(Mensajes.Campos_Vacios("Apellidos"));
            RuleFor(r => r.Identidad).NotEmpty().MaximumLength(13).MinimumLength(13).WithMessage(Mensajes.Longitd_Excedida("Identidad", 13));
            RuleFor(r => r.Genero).NotEmpty().Must(x => x == "F" || x == "M").WithMessage(Mensajes.Genero_Invalido);
            RuleFor(r => r.Telefono).NotEmpty().MaximumLength(8).MinimumLength(8).WithMessage(Mensajes.Longitd_Excedida("Telefono", 8));
            RuleFor(r => r.Direccion).NotEmpty().WithMessage(Mensajes.Campos_Vacios("Dirección"));
            RuleFor(r => r.EstadoCivilId).NotEmpty().WithMessage(Mensajes.Campos_Vacios("EstadoCivilId"));
        }
    }
}
