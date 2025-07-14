using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditosComplementarios.Core.Dtos.Departamento;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IDepartmentService;

namespace SistemaCreditosComplementarios.API.Controllers.DepartamentoController
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoService _departamentoService;
      
        public DepartamentoController(IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        [HttpGet]
        public async Task<ActionResult<DepartamentoDto>> GetById(int id)
        {
            try
            {
                var departamento = await _departamentoService.GetByIdAsync(id);
                if (departamento == null) return NotFound($"Departamento con ID {id} no encontrado. ");
                return Ok(departamento);
            }catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<DepartamentoDto>> Update([FromBody] DepartamentoUpdateDto departamentoUpdateDto)
        {
            try
            {
                if (departamentoUpdateDto == null) return BadRequest("Datos del departamentos no válidos");
                var updatedDepartamento = await _departamentoService.UpdateAsync(departamentoUpdateDto);
                return Ok(updatedDepartamento);
            }catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
