using Microsoft.AspNetCore.Mvc;
using SistemaCreditosComplementarios.Core.Dtos.AlumnoActividad;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAlumnoActividadService;

namespace SistemaCreditosComplementarios.API.Controllers.AlumnoActividadController
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlumnoActividadController : ControllerBase
    {
        private readonly IAlumnoActividadService _alumnoActividadService;

        public AlumnoActividadController(IAlumnoActividadService alumnoActividadService)
        {
            _alumnoActividadService = alumnoActividadService;
        }

        // GET: api/AlumnoActividad
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _alumnoActividadService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/AlumnoActividad/{alumnoId}/{actividadId}
        [HttpGet("{alumnoId:int}/{actividadId:int}")]
        public async Task<IActionResult> GetById(int alumnoId, int actividadId)
        {
            var result = await _alumnoActividadService.GetByIdAsync(alumnoId, actividadId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // GET: api/AlumnoActividad/alumnos-inscritos/{actividadId}
        [HttpGet("alumnos-inscritos/{actividadId:int}")]
        public async Task<IActionResult> GetAlumnosInscritosPorActividad(int actividadId)
        {
            var result = await _alumnoActividadService.GetAlumnosInscritosPorActividadAsync(actividadId);
            return Ok(result);
        }

        // GET: api/AlumnoActividad/cursos-alumno/{alumnoId}
        [HttpGet("cursos-alumno/{alumnoId:int}")]
        public async Task<IActionResult> GetCursosPorAlumno(int alumnoId)
        {
            var result = await _alumnoActividadService.GetCursosPorAlumnoAsync(alumnoId);
            return Ok(result);
        }

        // POST: api/AlumnoActividad
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AlumnoActividadCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _alumnoActividadService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { alumnoId = result.AlumnoId, actividadId = result.ActividadId }, result);
        }

        // PUT: api/AlumnoActividad/{alumnoId}/{actividadId}
        [HttpPut("{alumnoId:int}/{actividadId:int}")]
        public async Task<IActionResult> Update(int alumnoId, int actividadId, [FromBody] AlumnoActividadUpdate dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _alumnoActividadService.UpdateAsync(alumnoId, actividadId, dto);
            return NoContent();
        }

        // DELETE: api/AlumnoActividad/{alumnoId}/{actividadId}
        [HttpDelete("{alumnoId:int}/{actividadId:int}")]
        public async Task<IActionResult> Delete(int alumnoId, int actividadId)
        {
            await _alumnoActividadService.DeleteAsync(alumnoId, actividadId);
            return NoContent();
        }
    }
}
