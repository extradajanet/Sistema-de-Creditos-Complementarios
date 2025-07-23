using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditosComplementarios.Core.Dtos.Actividad;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IActividadService;

namespace SistemaCreditosComplementarios.API.Controllers.ActividadesController
{
    /// <summary>
    /// Controlador para gestionar actividades complementarias.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ActividadesController : ControllerBase
    {
        private readonly IActividadService _actividadService;

        /// <summary>
        /// Constructor del controlador de actividades.
        /// </summary>
        /// <param name="actividadService">Servicio de actividades.</param>
        public ActividadesController(IActividadService actividadService)
        {
            _actividadService = actividadService;
        }

        /// <summary>
        /// Obtiene todas las actividades registradas.
        /// </summary>
        /// <returns>Una lista de actividades.</returns>
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

        /// <summary>
        /// Obtiene una actividad por su ID.
        /// </summary>
        /// <param name="id">ID de la actividad.</param>
        /// <returns>La actividad correspondiente o un error si no se encuentra.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ActividadDto>> GetById(int id)
        {
            try
            {
                var actividad = await _actividadService.GetByIdAsync(id);
                if (actividad == null)
                    return NotFound($"Actividad con ID {id} no encontrada.");

                return Ok(actividad);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene actividades asociadas a un coordinador específico.
        /// </summary>
        /// <param name="coordinadorId">ID del coordinador.</param>
        /// <returns>Lista de actividades del coordinador.</returns>
        [HttpGet("coordinador/{coordinadorId}")]
        public async Task<IActionResult> GetByCoordinador(int coordinadorId)
        {
            var actividades = await _actividadService.GetByCoordinadorIdAsync(coordinadorId);
            return Ok(actividades);
        }

        /// <summary>
        /// Crea una nueva actividad.
        /// </summary>
        /// <param name="actividadCreateDto">Datos de la actividad a crear.</param>
        /// <returns>La actividad creada.</returns>
        [HttpPost]
        public async Task<ActionResult<ActividadDto>> Create([FromBody] ActividadCreateDto actividadCreateDto)
        {
            try
            {
                if (actividadCreateDto == null)
                    return BadRequest("Datos de actividad no válidos.");

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

        /// <summary>
        /// Actualiza una actividad existente.
        /// </summary>
        /// <param name="id">ID de la actividad a actualizar.</param>
        /// <param name="actividadUpdateDto">Nuevos datos de la actividad.</param>
        /// <returns>La actividad actualizada.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ActividadDto>> Update(int id, [FromBody] ActividadUpdateDto actividadUpdateDto)
        {
            try
            {
                if (actividadUpdateDto == null)
                    return BadRequest("Datos de actividad no válidos.");

                var actividadActualizada = await _actividadService.UpdateAsync(id, actividadUpdateDto);
                return Ok(actividadActualizada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina una actividad por su ID.
        /// </summary>
        /// <param name="id">ID de la actividad a eliminar.</param>
        /// <returns>Respuesta sin contenido si la eliminación fue exitosa.</returns>
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