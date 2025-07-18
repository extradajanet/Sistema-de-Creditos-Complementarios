using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditosComplementarios.Core.Dtos.Actividad;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IActividadService;

namespace SistemaCreditosComplementarios.API.Controllers.ActividadesController
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActividadesController : ControllerBase
    {
        //inyección de dependencias del servicio de actividades
        private readonly IActividadService _actividadService;

        //constructor que recibe el servicio de actividades
        public ActividadesController(IActividadService actividadService)
        {
            _actividadService = actividadService;
        }

        // GET: api/actividades
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<ActividadDto>>> GetAll()
        {
            try
            {
                var actividades = await _actividadService.GetAllAsync();
                return Ok(actividades);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/actividades/{id}
        [HttpGet("{id}")]
       
        public async Task<ActionResult<ActividadDto>> GetById(int id)
        {
            try
            {
                var actividad = await _actividadService.GetByIdAsync(id);

                if (actividad == null) return NotFound($"Actividad con ID {id} no encontrada.");

                return Ok(actividad);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/actividades
        [HttpPost]
        
        public async Task<ActionResult<ActividadDto>> Create([FromBody] ActividadCreateDto actividadCreateDto)
        {
            try
            {
                if (actividadCreateDto == null) return BadRequest("Datos de actividad no válidos.");
                var nuevaActividad = await _actividadService.AddAsync(actividadCreateDto);
                return CreatedAtAction(nameof(GetById), new { id = nuevaActividad.Id }, nuevaActividad);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Error interno del servidor",
                    message = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }

        }

        // PUT: api/actividades/{id}
        [HttpPut("{id}")]
        
        public async Task<ActionResult<ActividadDto>> Update(int id, [FromBody] ActividadCreateDto actividadUpdateDto)
        {
            try
            {
                if (actividadUpdateDto == null) return BadRequest("Datos de actividad no válidos.");
                var actividadActualizada = await _actividadService.UpdateAsync(id, actividadUpdateDto);
                return Ok(actividadActualizada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/actividades/{id}
        [HttpDelete("{id}")]
      
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _actividadService.DeleteAsync(id);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }


        }
    }
}
