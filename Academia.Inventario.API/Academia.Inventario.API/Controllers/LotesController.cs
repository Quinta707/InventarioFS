using Academia.Inventario.API._Features.Lotes;
using Academia.Inventario.API._Features.Lotes.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Inventario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotesController : ControllerBase
    {
        private readonly LotesService _lotesService;

        public LotesController(LotesService lotesService)
        {
            _lotesService = lotesService;
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var respuesta = _lotesService.ListarLotes();
            return Ok(respuesta);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar(LotesDto lotesDto)
        {
            var respuesta = _lotesService.Insertar(lotesDto);
            return Ok(respuesta);
        }

        [HttpPut("Editar")]
        public IActionResult Editar(LotesDto lotesDto)
        {
            var respuesta = _lotesService.Editar(lotesDto);
            return Ok(respuesta);
        }
    }
}
