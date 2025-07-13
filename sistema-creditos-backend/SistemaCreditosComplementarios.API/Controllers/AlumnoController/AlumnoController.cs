using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditosComplementarios.Core.Dtos.Alumno;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAlumnoService;

namespace SistemaCreditosComplementarios.API.Controllers.ControllerAlumno
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlumnoController : ControllerBase
    {
        private readonly IAlumnoService _alumnoService;

        public AlumnoController(IAlumnoService alumnoService)
        {
            _alumnoService = alumnoService;
        }

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

        [HttpPut]
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
