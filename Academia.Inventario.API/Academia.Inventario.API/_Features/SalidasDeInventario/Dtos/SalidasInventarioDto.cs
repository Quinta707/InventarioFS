using Academia.Inventario.API._Features.SalidasInventarioDetalles.Dtos;

namespace Academia.Inventario.API._Features.SalidasDeInventario.Dtos
{
    public class SalidasInventarioDto
    {
        public int SalidaInventarioId { get; set; }

        public int? SucursalId { get; set; }

        public DateTime? FechaSalida { get; set; }

        public int? Total { get; set; }

        public DateTime? FechaRecibido { get; set; }

        public int? UsuarioRecibeId { get; set; }

        public int? UsuarioCreacionId { get; set; }

        public List<SalidasInventarioDetallesDto> SalidasDetalles { get; set; } = new List<SalidasInventarioDetallesDto>();

    }
}
