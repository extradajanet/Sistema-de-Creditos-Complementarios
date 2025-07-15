using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditosComplementarios.Core.Dtos.Coordinador;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.ICoordinadorService;

namespace SistemaCreditosComplementarios.API.Controllers.CoordinadorController
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoordinadorController : ControllerBase
    {

        private readonly ICoordinadorService _coordinadorService;

        public CoordinadorController(ICoordinadorService coordinadorService)
        {
            _coordinadorService = coordinadorService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CoordinadorDto>> GetById(int id)
        {
            try
            {
                var coordinador = await _coordinadorService.GetByIdAsync(id);
                if (coordinador == null) return NotFound($"Coordinador con ID {id} no encontrado. ");
                return Ok(coordinador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CoordinadorDto>> Update([FromBody] CoordinadorUpdateDto coordinadorUpdateDto)
        {
            try
            {
                if (coordinadorUpdateDto == null) return BadRequest("Datos del Coordinador no válidos");
                var updatedCoordinador = await _coordinadorService.UpdateAsync(coordinadorUpdateDto);
                return Ok(updatedCoordinador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
