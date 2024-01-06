using Academia.Inventario.API._Features.Permisos;
using Academia.Inventario.API._Features.Permisos.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Inventario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisosController : ControllerBase
    {
        private readonly PermisosService _permisosService;

        public PermisosController(PermisosService permisosService)
        {
            _permisosService = permisosService;
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var respuesta = _permisosService.ListarPermisos();
            return Ok(respuesta);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar(PermisosDto permisosDto)
        {
            var respuesta = _permisosService.Insertar(permisosDto);
            return Ok(respuesta);
        }

        [HttpPut("Editar")]
        public IActionResult Editar(PermisosDto permisosDto)
        {
            var respuesta = _permisosService.Editar(permisosDto);
            return Ok(respuesta);
        }
    }
}
