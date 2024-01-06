using Academia.Inventario.API._Features.SalidasDeInventario.Dtos;
using Academia.Inventario.API._Features.SalidasInventarioDetalles;
using Academia.Inventario.API._Features.SalidasInventarioDetalles.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Inventario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidasInventarioDetallesController : ControllerBase
    {
        private readonly SalidasInventarioDetallesService _salidasInventarioDetallesService;

        public SalidasInventarioDetallesController(SalidasInventarioDetallesService salidasInventarioDetallesService)
        {
            _salidasInventarioDetallesService = salidasInventarioDetallesService;
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var respuesta = _salidasInventarioDetallesService.ListarSalidasInventarioDetalles();
            return Ok(respuesta);
        }

        [HttpGet("ListarPorSalida")]
        public IActionResult ListarPorSalida(int? id)
        {
            var respuesta = _salidasInventarioDetallesService.ListarSalidasInventarioDetallesPorSalida(id);
            return Ok(respuesta);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar(SalidasInventarioDetallesDto salidasInventarioDetallesDto)
        {
            var respuesta = _salidasInventarioDetallesService.Insertar(salidasInventarioDetallesDto);
            return Ok(respuesta);
        }

        //[HttpPut("Editar")]
        //public IActionResult Editar(SalidasInventarioDetallesDto salidasInventarioDetallesDto)
        //{
        //    var respuesta = _salidasInventarioDetallesService.Editar(salidasInventarioDetallesDto);
        //    return Ok(respuesta);
        //}
    }
}
