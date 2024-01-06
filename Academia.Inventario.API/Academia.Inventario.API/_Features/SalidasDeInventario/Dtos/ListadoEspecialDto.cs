namespace Academia.Inventario.API._Features.SalidasDeInventario.Dtos
{
    public class ListadoEspecialDto
    {
        public int SalidaInventarioId { get; set; }

        public int? SucursalId { get; set; }

        public string? SucursalNombre { get; set; }

        public DateTime? FechaSalida { get; set; }

        public int? Unidades { get; set; }

        public int? CostoTotal { get; set; }

        public DateTime? FechaRecibido { get; set; }

        public int? UsuarioRecibeId { get; set; }
    }
}
