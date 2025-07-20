using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditosComplementarios.Core.Dtos.Aviso;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAvisoService;

namespace SistemaCreditosComplementarios.API.Controllers.AvisoController
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvisoController : ControllerBase
    {
        private readonly IAvisoService _avisoService;
        public AvisoController (IAvisoService avisoService)
        {
            _avisoService = avisoService;
        }

        // GET: api/avisos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvisoDto>>> GetAll()
        {
            try
            {
                var avisos = await _avisoService.GetAllAvisoAsync();
                return Ok(avisos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $" Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AvisoDto>> Create([FromBody] AvisoCreateDto avisoCreateDto)
        {
            try
            {
                if (avisoCreateDto == null) return BadRequest("Datos del aviso no válidos. ");
                var nuevoAviso = await _avisoService.CreateAvisoAsync(avisoCreateDto);
                return nuevoAviso;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
