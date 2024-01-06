namespace Academia.Inventario.API._Features.SalidasInventarioDetalles.Dtos
{
    public class SalidasInventarioDetallesListDto
    {
        public int SalidaInventarioDetalleId { get; set; }

        public int? SalidaInventarioId { get; set; }

        public int? LoteId { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        public int? ProductoID { get; set; }

        public string? ProductoNombre { get; set; }

        public decimal? CostoUnitario { get; set; }

        public int? Cantidad { get; set; }

        public int? TotalDetalle { get; set; }

    }
}
