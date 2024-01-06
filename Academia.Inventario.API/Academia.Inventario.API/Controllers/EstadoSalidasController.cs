using Academia.Inventario.API._Features.EstadoSalidas;
using Academia.Inventario.API._Features.EstadoSalidas.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Inventario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoSalidasController : ControllerBase
    {
        private readonly EstadoSalidasService _estadoSalidasService;

        public EstadoSalidasController(EstadoSalidasService estadoSalidasService)
        {
            _estadoSalidasService = estadoSalidasService;
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var respuesta = _estadoSalidasService.ListarEstadoSalidas();
            return Ok(respuesta);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar(EstadoSalidasDto estadoSalidasDto)
        {
            var respuesta = _estadoSalidasService.Insertar(estadoSalidasDto);
            return Ok(respuesta);
        }

        [HttpPut("Editar")]
        public IActionResult Editar(EstadoSalidasDto estadoSalidasDto)
        {
            var respuesta = _estadoSalidasService.Editar(estadoSalidasDto);
            return Ok(respuesta);
        }
    }
}
