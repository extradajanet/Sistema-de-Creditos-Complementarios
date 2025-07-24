using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditosComplementarios.Core.Dtos.AlumnoActividad;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAlumnoActividadService;
using SistemaCreditosComplementarios.Core.Models.Enum;

namespace SistemaCreditosComplementarios.API.Controllers.AlumnoActividadController
{
    /// <summary>
    /// Controlador para gestionar las relaciones entre alumnos y actividades complementarias.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AlumnoActividadController : ControllerBase
    {
        private readonly IAlumnoActividadService _alumnoActividadService;

        /// <summary>
        /// Constructor del controlador de AlumnoActividad.
        /// </summary>
        /// <param name="alumnoActividadService">Servicio de gestión de relaciones alumno-actividad.</param>
        public AlumnoActividadController(IAlumnoActividadService alumnoActividadService)
        {
            _alumnoActividadService = alumnoActividadService;
        }

        /// <summary>
        /// Obtiene todas las relaciones entre alumnos y actividades.
        /// </summary>
        /// <returns>Lista de todas las relaciones alumno-actividad.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _alumnoActividadService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Obtiene una relación específica entre un alumno y una actividad.
        /// </summary>
        /// <param name="alumnoId">ID del alumno.</param>
        /// <param name="actividadId">ID de la actividad.</param>
        /// <returns>La relación encontrada o un error si no existe.</returns>
        [HttpGet("{alumnoId:int}/{actividadId:int}")]
        public async Task<IActionResult> GetById(int alumnoId, int actividadId)
        {
            var result = await _alumnoActividadService.GetByIdAsync(alumnoId, actividadId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Obtiene todos los alumnos inscritos en una actividad específica.
        /// </summary>
        /// <param name="actividadId">ID de la actividad.</param>
        /// <returns>Lista de alumnos inscritos.</returns>
        [HttpGet("alumnos-inscritos/{actividadId:int}")]
        public async Task<IActionResult> GetAlumnosInscritosPorActividad(int actividadId)
        {
            var result = await _alumnoActividadService.GetAlumnosInscritosPorActividadAsync(actividadId);
            return Ok(result);
        }

        /// <summary>
        /// Obtiene las actividades inscritas por un alumno, con posibilidad de filtrar por estado.
        /// </summary>
        /// <param name="alumnoId">ID del alumno.</param>
        /// <param name="estado">Estado opcional de la actividad (por ejemplo: Aprobada, Rechazada, Pendiente).</param>
        /// <returns>Lista de actividades asociadas al alumno.</returns>
        [HttpGet("cursos-alumno/{alumnoId:int}")]
        public async Task<IActionResult> GetCursosPorAlumno(int alumnoId, EstadoAlumnoActividad? estado = null)
        {
            var result = await _alumnoActividadService.GetCursosPorAlumnoAsync(alumnoId, estado);
            return Ok(result);
        }

        /// <summary>
        /// Registra una nueva relación entre un alumno y una actividad.
        /// </summary>
        /// <param name="dto">Datos de la nueva inscripción.</param>
        /// <returns>La relación creada.</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AlumnoActividadCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _alumnoActividadService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { alumnoId = result.AlumnoId, actividadId = result.ActividadId }, result);
        }

        /// <summary>
        /// Actualiza la relación entre un alumno y una actividad existente.
        /// </summary>
        /// <param name="alumnoId">ID del alumno.</param>
        /// <param name="actividadId">ID de la actividad.</param>
        /// <param name="dto">Datos actualizados de la inscripción.</param>
        /// <returns>Respuesta sin contenido si la actualización fue exitosa.</returns>
        [HttpPut("{alumnoId:int}/{actividadId:int}")]
        public async Task<IActionResult> Update(int alumnoId, int actividadId, [FromBody] AlumnoActividadUpdate dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _alumnoActividadService.UpdateAsync(alumnoId, actividadId, dto);
            return NoContent();
        }

        /// <summary>
        /// Elimina la relación entre un alumno y una actividad.
        /// </summary>
        /// <param name="alumnoId">ID del alumno.</param>
        /// <param name="actividadId">ID de la actividad.</param>
        /// <returns>Respuesta sin contenido si la eliminación fue exitosa.</returns>
        [HttpDelete("{alumnoId:int}/{actividadId:int}")]
        public async Task<IActionResult> Delete(int alumnoId, int actividadId)
        {
            await _alumnoActividadService.DeleteAsync(alumnoId, actividadId);
            return NoContent();
        }
    }
}
