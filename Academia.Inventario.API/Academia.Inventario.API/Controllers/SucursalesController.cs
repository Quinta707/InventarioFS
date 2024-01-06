using Academia.Inventario.API._Features.Sucursales;
using Academia.Inventario.API._Features.Sucursales.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Inventario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly SucursalesService _sucursalesService;
        public SucursalesController(SucursalesService sucursalesService)
        {
            _sucursalesService = sucursalesService;
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var respuesta = _sucursalesService.ListarSucursales();
            return Ok(respuesta);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar(SucursalesDto sucursalesDto)
        {
            var respuesta = _sucursalesService.Insertar(sucursalesDto);
            return Ok(respuesta);
        }

        [HttpPut("Editar")]
        public IActionResult Editar(SucursalesDto sucursalesDto)
        {
            var respuesta = _sucursalesService.Editar(sucursalesDto);
            return Ok(respuesta);
        }
    }
}
