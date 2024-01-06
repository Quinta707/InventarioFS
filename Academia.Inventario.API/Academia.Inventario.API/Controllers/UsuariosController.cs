using Academia.Inventario.API._Features.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Inventario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosService _usuariosService;

        public UsuariosController(UsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var respuesta = _usuariosService.ListarUsuarios();
            return Ok(respuesta);
        }
    }
}
