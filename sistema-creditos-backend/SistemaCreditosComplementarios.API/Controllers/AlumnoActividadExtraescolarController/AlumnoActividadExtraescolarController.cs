using Microsoft.AspNetCore.Mvc;
using SistemaCreditosComplementarios.Core.Dtos.ActividadExtraescolarAlumno;
using SistemaCreditosComplementarios.Core.Dtos.AlumnoActividad;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAlumnoActividadExtraescolarService;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAlumnoActividadService;
using SistemaCreditosComplementarios.Core.Models.Enum;

namespace SistemaCreditosComplementarios.API.Controllers.AlumnoActividadExtraescolarController
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlumnoActividadExtraescolarController : ControllerBase
    {
        private readonly IAlumnoActividadExtraescolarService _alumnoActividadExtraescolarService;

        public AlumnoActividadExtraescolarController(IAlumnoActividadExtraescolarService alumnoActividadExtraescolarService)
        {
            _alumnoActividadExtraescolarService = alumnoActividadExtraescolarService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _alumnoActividadExtraescolarService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{alumnoId:int}/{actividadExtraescolarId:int}")]
        public async Task<IActionResult> GetById(int alumnoId, int actividadExtraescolarId)
        {
            var result = await _alumnoActividadExtraescolarService.GetByIdAsync(alumnoId, actividadExtraescolarId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("alumnos-inscritos/{actividadExtraescolarId:int}")]
        public async Task<IActionResult> GetAlumnosInscritosPorActividad(int actividadExtraescolarId)
        {
            var result = await _alumnoActividadExtraescolarService.GetAlumnosInscritosPorActividadAsync(actividadExtraescolarId);
            return Ok(result);
        }

        [HttpGet("extraescolar-alumno/{alumnoId:int}")]
        public async Task<IActionResult> GetCursosPorAlumno(int alumnoId, EstadoAlumnoActividad? estado = null)
        {
            var result = await _alumnoActividadExtraescolarService.GetCursosPorAlumnoAsync(alumnoId, estado);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AlumnoActividadExtraescolarCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _alumnoActividadExtraescolarService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { alumnoId = result.AlumnoId, actividadExtraescolarId = result.ActividadExtraescolarId }, result);
        }


        [HttpPut("{alumnoId:int}/{actividadExtraescolarId:int}")]
        public async Task<IActionResult> Update(int alumnoId, int actividadExtraescolarId, [FromBody] AlumnoActividadExtraescolarUpdate dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _alumnoActividadExtraescolarService.UpdateAsync(alumnoId, actividadExtraescolarId, dto);
            return NoContent();
        }

        [HttpDelete("{alumnoId:int}/{actividadExtraescolarId:int}")]
        public async Task<IActionResult> Delete(int alumnoId, int actividadExtraescolarId)
        {
            await _alumnoActividadExtraescolarService.DeleteAsync(alumnoId, actividadExtraescolarId);
            return NoContent();
        }
    }
}
