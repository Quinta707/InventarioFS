using Academia.Inventario.API._Features.RolesPorPermisos;
using Academia.Inventario.API._Features.RolesPorPermisos.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Inventario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesPorPermisoController : ControllerBase
    {
        private readonly RolesPorPermisosService _rolesPorPermisosService;

        public RolesPorPermisoController(RolesPorPermisosService rolesPorPermisosService)
        {
            _rolesPorPermisosService = rolesPorPermisosService;
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var respuesta = _rolesPorPermisosService.ListarRolesPorPermiso();
            return Ok(respuesta);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar(RolesPorPermisosDto rolesPorPermisosDto)
        {
            var respuesta = _rolesPorPermisosService.Insertar(rolesPorPermisosDto);
            return Ok(respuesta);
        }

        [HttpPut("Editar")]
        public IActionResult Editar(RolesPorPermisosDto rolesPorPermisosDto)
        {
            var respuesta = _rolesPorPermisosService.Editar(rolesPorPermisosDto);
            return Ok(respuesta);
        }
    }
}
