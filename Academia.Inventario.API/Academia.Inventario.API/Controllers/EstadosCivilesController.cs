using Academia.Inventario.API._Features.EstadosCiviles;
using Academia.Inventario.API._Features.EstadosCiviles.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Inventario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosCivilesController : ControllerBase
    {
        private readonly EstadosCivilesService _estadosCivilesService;

        public EstadosCivilesController(EstadosCivilesService estadosCivilesService)
        {
            _estadosCivilesService = estadosCivilesService;
        }

        [HttpGet("Listar")]
        public IActionResult Index()
        {
            var respuesta = _estadosCivilesService.ListarEstadosCiviles();
            return Ok(respuesta);
        }

        [HttpPost("Insertar")]
        public IActionResult Insert(EstadosCivilesDto estadosCivilesDto)
        {
            var respuesta = _estadosCivilesService.InsertarEstadosCiviles(estadosCivilesDto);
            return Ok(respuesta);
        }

        [HttpPut("Editar")]
        public IActionResult Editar(EstadosCivilesDto estadosCivilesDto)
        {
            var respuesta = _estadosCivilesService.EditarEstadoCivil(estadosCivilesDto);
            return Ok(respuesta);
        }

        [HttpPut("Desactivar")]
        public IActionResult Desactivar(EstadosCivilesDto estadosCivilesDto)
        {
            var respuesta = _estadosCivilesService.DesactivarEstadoCivil(estadosCivilesDto);
            return Ok(respuesta);
        }

        [HttpPut("Activar")]
        public IActionResult Activar(EstadosCivilesDto estadosCivilesDto)
        {
            var respuesta = _estadosCivilesService.ActivarEstadoCivil(estadosCivilesDto);
            return Ok(respuesta);
        }
    }
}
