using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditosComplementarios.Core.Dtos.Alumno;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAlumnoService;

namespace SistemaCreditosComplementarios.API.Controllers.ControllerAlumno
{
    /// <summary>
    /// Controlador para gestionar alumnos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AlumnoController : ControllerBase
    {
        private readonly IAlumnoService _alumnoService;

        /// <summary>
        /// Constructor del controlador de alumnos.
        /// </summary>
        /// <param name="alumnoService">Servicio de alumnos.</param>
        public AlumnoController(IAlumnoService alumnoService)
        {
            _alumnoService = alumnoService;
        }

        /// <summary>
        /// Obtiene todos los alumnos registrados.
        /// </summary>
        /// <returns>Lista de alumnos.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlumnoDto>>> GetAll()
        {
            try
            {
                var alumnos = await _alumnoService.GetAllAsync();
                return Ok(alumnos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene un alumno por su ID.
        /// </summary>
        /// <param name="id">ID del alumno.</param>
        /// <returns>El alumno correspondiente o error si no se encuentra.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AlumnoDto>> GetById(int id)
        {
            try
            {
                var alumno = await _alumnoService.GetByIdAsync(id);
                if (alumno == null) return NotFound($"Alumno con ID {id} no encontrado.");
                return Ok(alumno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene el total de créditos acumulados por un alumno.
        /// </summary>
        /// <param name="alumnoId">ID del alumno.</param>
        /// <returns>Total de créditos.</returns>
        [HttpGet("total-creditos/{alumnoId}")]
        public async Task<ActionResult<double>> GetTotalCreditos(int alumnoId)
        {
            try
            {
                var totalCreditos = await _alumnoService.GetTotalCreditosAsync(alumnoId);
                return Ok(totalCreditos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene alumnos filtrados por coordinador, carrera y/o cantidad de créditos.
        /// </summary>
        /// <param name="coordinadorId">ID del coordinador.</param>
        /// <param name="carreraId">ID opcional de la carrera.</param>
        /// <param name="cantCreditos">Cantidad opcional de créditos mínimos.</param>
        /// <returns>Lista de alumnos filtrados.</returns>
        [HttpGet("filtrados/{coordinadorId}")]
        public async Task<IActionResult> GetAlumnosFiltrados(int coordinadorId, [FromQuery] int? carreraId, [FromQuery] double? cantCreditos)
        {
            var alumnos = await _alumnoService.GetAlumnosFiltradosByCoordinadorIdAsync(coordinadorId, carreraId, cantCreditos);
            return Ok(alumnos);
        }

        /// <summary>
        /// Crea un nuevo alumno.
        /// </summary>
        /// <param name="alumnoCreateDto">Datos del alumno a crear.</param>
        /// <returns>El alumno creado.</returns>
        [HttpPost]
        private async Task<ActionResult<AlumnoDto>> Create([FromBody] AlumnoCreateDto alumnoCreateDto)
        {
            try
            {
                if (alumnoCreateDto == null) return BadRequest("Datos de alumno no válidos.");

                var nuevoAlumno = await _alumnoService.AddAsync(alumnoCreateDto);
                return CreatedAtAction(nameof(GetById), new { id = nuevoAlumno.Id }, nuevoAlumno);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear alumno: {ex.Message}");
                Console.WriteLine("Inner: " + ex.InnerException?.Message);
                throw;
            }
        }

        /// <summary>
        /// Actualiza un alumno existente.
        /// </summary>
        /// <param name="alumnoUpdateDto">Datos actualizados del alumno.</param>
        /// <returns>El alumno actualizado.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<AlumnoDto>> Update([FromBody] AlumnoUpdateDto alumnoUpdateDto)
        {
            try
            {
                if (alumnoUpdateDto == null) return BadRequest("Datos de alumno no válidos.");

                var updatedAlumno = await _alumnoService.UpdateAsync(alumnoUpdateDto);
                return Ok(updatedAlumno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un alumno por su ID.
        /// </summary>
        /// <param name="id">ID del alumno a eliminar.</param>
        /// <returns>El alumno eliminado.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<AlumnoDto>> Delete(int id)
        {
            try
            {
                var deletedAlumno = await _alumnoService.DeleteAsync(id);
                return Ok(deletedAlumno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
