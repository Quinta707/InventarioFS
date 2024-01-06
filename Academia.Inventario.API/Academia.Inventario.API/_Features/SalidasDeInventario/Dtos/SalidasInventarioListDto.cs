using Academia.Inventario.API._Features.SalidasInventarioDetalles.Dtos;

namespace Academia.Inventario.API._Features.SalidasDeInventario.Dtos
{
    public class SalidasInventarioListDto
    {
        public int SalidaInventarioId { get; set; }

        public int? SucursalId { get; set; }

        public string? SucursalNombre { get; set; }

        public DateTime? FechaSalida { get; set; }

        public int? Total { get; set; }

        public DateTime? FechaRecibido { get; set; }

        public int? UsuarioRecibeId { get; set; }

        public string? UsuarioRecibeNombre { get; set; }


        public List<SalidasInventarioDetallesListDto> SalidasInventarioDetalles { get; set; } = new List<SalidasInventarioDetallesListDto>();
    }
}
