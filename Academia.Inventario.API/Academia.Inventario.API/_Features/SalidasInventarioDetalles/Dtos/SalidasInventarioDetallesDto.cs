namespace Academia.Inventario.API._Features.SalidasInventarioDetalles.Dtos
{
    public class SalidasInventarioDetallesDto
    {
        public int SalidaInventarioDetalleId { get; set; }

        public int? SalidaInventarioId { get; set; }

        public int? ProductoId { get; set; }

        public int? LoteId { get; set; }

        public int? Cantidad { get; set; }

    }
}
