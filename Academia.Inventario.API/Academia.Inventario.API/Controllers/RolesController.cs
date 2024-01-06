using Academia.Inventario.API._Features.Roles;
using Academia.Inventario.API._Features.Roles.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Inventario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RolesService _rolesService;

        public RolesController(RolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var respuesta = _rolesService.ListarRoles();
            return Ok(respuesta);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar(RolesDto rolesDto)
        {
            var respuesta = _rolesService.Insertar(rolesDto);
            return Ok(respuesta);
        }

        [HttpPut("Editar")]
        public IActionResult Editar(RolesDto rolesDto)
        {
            var respuesta = _rolesService.Editar(rolesDto);
            return Ok(respuesta);
        }
    }
}
