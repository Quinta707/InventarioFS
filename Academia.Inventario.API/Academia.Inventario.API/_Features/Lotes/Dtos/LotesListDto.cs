namespace Academia.Inventario.API._Features.Lotes.Dtos
{
    public class LotesListDto
    {
        public int LoteId { get; set; }

        public int? ProductoId { get; set; }

        public string? ProductoDescripcion { get; set; }

        public int? CantidadInicial { get; set; }

        public decimal? CostoUnitario { get; set; }

        public int? CantidadActual { get; set; }

        public DateTime? FechaVencimiento { get; set; }

    }
}
