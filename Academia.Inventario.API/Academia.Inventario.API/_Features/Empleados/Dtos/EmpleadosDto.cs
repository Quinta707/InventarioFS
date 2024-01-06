namespace Academia.Inventario.API._Features.Empleados.Dtos
{
    public class EmpleadosDto
    {
        public int EmpleadoId { get; set; }

        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public string? Identidad { get; set; }

        public string? Telefono { get; set; }

        public string? Genero { get; set; }

        public string? Direccion { get; set; }

        public int? EstadoCivilId { get; set; }
    }
}
