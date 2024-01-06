using Academia.Inventario.API._Features.SalidasDeInventario;
using Academia.Inventario.API._Features.SalidasDeInventario.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Inventario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidasInventarioController : ControllerBase
    {
        private readonly SalidasInventarioService _salidasInventarioService;

        public SalidasInventarioController(SalidasInventarioService salidasInventarioService)
        {
            _salidasInventarioService = salidasInventarioService;
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var respuesta = _salidasInventarioService.ListarSalidasInventario();
            return Ok(respuesta);
        }

        [HttpGet("ListarPorId")]
        public IActionResult ListarPorId(int? id)
        {
            var respuesta = _salidasInventarioService.ListarSalidasInventarioPorId(id);
            return Ok(respuesta);
        }

        [HttpGet("ListarPorSucursal")]
        public IActionResult ListarPorSucursal(int? id)
        {
            var respuesta = _salidasInventarioService.ListarSalidasInventarioPorSucursalId(id);
            return Ok(respuesta);
        }


        [HttpGet("ListadoFormulario")]
        public IActionResult ListadoFORM(DateTime? inicio, DateTime? fin, int? id)
        {
            var respuesta = _salidasInventarioService.ListarFormulario(inicio, fin, id);
            return Ok(respuesta);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar(SalidasInventarioDto salidasInventarioDto)
        {
            var respuesta = _salidasInventarioService.InsertarSalidas(salidasInventarioDto);
            return Ok(respuesta);
        }

        [HttpPut("RecibirSalida")]
        public IActionResult RecibirSalida(SalidasInventarioDto salidasInventarioDto)
        {
            var respuesta = _salidasInventarioService.RecibirSalida(salidasInventarioDto);
            return Ok(respuesta);
        }
    }
}
