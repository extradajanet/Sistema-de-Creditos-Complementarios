using Microsoft.AspNetCore.Mvc;
using SistemaCreditosComplementarios.Core.Dtos.Actividad;
using SistemaCreditosComplementarios.Core.Dtos.ActividadesExtraescolares;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IActividadExtraescolarService;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IActividadService;
using SistemaCreditosComplementarios.Core.Services.ActividadService;

namespace SistemaCreditosComplementarios.API.Controllers.ActividadExtraescolarController
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActividadExtraescolarController : ControllerBase
    {
            private readonly IActividadExtraescolarService _actividadExtraescolarService;

            public ActividadExtraescolarController(IActividadExtraescolarService actividadExtraescolarService)
            {
                 _actividadExtraescolarService = actividadExtraescolarService;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<ActividadExtraescolarDto>>> GetAll()
            {
                try
                {
                    var actividadeextraescolar = await _actividadExtraescolarService.GetAllAsync();
                    return Ok(actividadeextraescolar);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error interno del servidor: {ex.Message}");
                }
            }

            [HttpGet("{id}")]

            public async Task<ActionResult<ActividadExtraescolarDto>> GetById(int id)
            {
                try
                {
                    var actividadeextraescolar = await _actividadExtraescolarService.GetByIdAsync(id);

                    if (actividadeextraescolar == null) return NotFound($"Actividad con ID {id} no encontrada.");

                    return Ok(actividadeextraescolar);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error interno del servidor: {ex.Message}");
                }
            }

            [HttpPost]

            public async Task<ActionResult<ActividadExtraescolarDto>> Create([FromBody] ActividadExtraescolarCreateDto actividadExtraescolarCreateDto)
            {
                try
                {
                    if (actividadExtraescolarCreateDto == null) return BadRequest("Datos de actividad no válidos.");
                    var nuevaActividadExtraescolar = await _actividadExtraescolarService.AddAsync(actividadExtraescolarCreateDto);
                    return CreatedAtAction(nameof(GetById), new { id = nuevaActividadExtraescolar.Id }, nuevaActividadExtraescolar);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error interno del servidor: {ex.Message}");
                }
            }


            [HttpPut("{id}")]
            public async Task<ActionResult<ActividadExtraescolarDto>> Update(int id, [FromBody] ActividadExtraescolarCreateDto actividadExtraescolarUpdateDto)
            {
                try
                {
                    if (actividadExtraescolarUpdateDto == null) return BadRequest("Datos de actividad no válidos.");
                    var actividadActualizada = await _actividadExtraescolarService.UpdateAsync(id, actividadExtraescolarUpdateDto);
                    return Ok(actividadActualizada);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error interno del servidor: {ex.Message}");
                }
            }

            [HttpDelete("{id}")]

            public async Task<IActionResult> Delete(int id)
            {
                try
                {
                    await _actividadExtraescolarService.DeleteAsync(id);
                    return NoContent(); 
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error interno del servidor: {ex.Message}");
                }


            }
        
    }
}
