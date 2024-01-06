using Academia.Inventario.API._Features.Productos;
using Academia.Inventario.API._Features.Productos.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Inventario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ProductosService _productosService;

        public ProductosController(ProductosService productosService)
        {
            _productosService = productosService;
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var respuesta = _productosService.ListarProductos();
            return Ok(respuesta);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar(ProductosDto productosDto)
        {
            var respuesta = _productosService.Insertar(productosDto);
            return Ok(respuesta);
        }

        [HttpPut("Editar")]
        public IActionResult Listar(ProductosDto productosDto)
        {
            var respuesta = _productosService.Editar(productosDto);
            return Ok(respuesta);
        }
    }
}
