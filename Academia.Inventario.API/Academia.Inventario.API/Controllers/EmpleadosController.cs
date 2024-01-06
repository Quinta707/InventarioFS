using Academia.Inventario.API._Features.Empleados;
using Academia.Inventario.API._Features.Empleados.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Inventario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly EmpleadosService _empleadosService;
        public EmpleadosController(EmpleadosService empleadosService)
        {
            _empleadosService = empleadosService;
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var respuesta = _empleadosService.ListarEmpleados();
            return Ok(respuesta);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar(EmpleadosDto empleadosDto)
        {
            var respuesta = _empleadosService.InsertarEmpleados(empleadosDto);
            return Ok(respuesta);
        }

        [HttpPut("Editar")]
        public IActionResult Editar(EmpleadosDto empleadosDto)
        {
            var respuesta = _empleadosService.EditarEmpleados(empleadosDto);
            return Ok(respuesta);
        }
    }
}
