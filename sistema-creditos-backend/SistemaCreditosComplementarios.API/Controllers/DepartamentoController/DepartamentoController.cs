using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditosComplementarios.Core.Dtos.Departamento;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IDepartmentService;

namespace SistemaCreditosComplementarios.API.Controllers.DepartamentoController
{
    /// <summary>
    /// Controlador para gestionar departamentos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoService _departamentoService;

        /// <summary>
        /// Constructor del controlador de departamentos.
        /// </summary>
        /// <param name="departamentoService">Servicio de departamentos.</param>
        public DepartamentoController(IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        /// <summary>
        /// Obtiene un departamento por su ID.
        /// </summary>
        /// <param name="id">ID del departamento.</param>
        /// <returns>El departamento correspondiente o un mensaje de error si no se encuentra.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartamentoDto>> GetById(int id)
        {
            try
            {
                var departamento = await _departamentoService.GetByIdAsync(id);
                if (departamento == null)
                    return NotFound($"Departamento con ID {id} no encontrado. ");

                return Ok(departamento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza los datos de un departamento existente.
        /// </summary>
        /// <param name="departamentoUpdateDto">Datos actualizados del departamento.</param>
        /// <returns>El departamento actualizado o un mensaje de error si la actualización falla.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<DepartamentoDto>> Update([FromBody] DepartamentoUpdateDto departamentoUpdateDto)
        {
            try
            {
                if (departamentoUpdateDto == null)
                    return BadRequest("Datos del departamento no válidos");

                var updatedDepartamento = await _departamentoService.UpdateAsync(departamentoUpdateDto);
                return Ok(updatedDepartamento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
