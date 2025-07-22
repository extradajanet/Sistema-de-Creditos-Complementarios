using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditosComplementarios.Core.Dtos.Aviso;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAvisoService;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.ICoordinadorService;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IDepartmentService;

namespace SistemaCreditosComplementarios.API.Controllers.AvisoController
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvisoController : ControllerBase
    {
        private readonly IAvisoService _avisoService;
        private readonly ICoordinadorService _coordinadorService;
        private readonly IDepartamentoService _departamentoService;
        public AvisoController(IAvisoService avisoService,ICoordinadorService coordinadorService, IDepartamentoService departamentoService)
        {
            _avisoService = avisoService;
            _coordinadorService = coordinadorService;
            _departamentoService = departamentoService;
        }

        // GET: api/avisos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvisoDto>>> GetAll()
        {
            try
            {
                var avisos = await _avisoService.GetAllAvisoAsync();
                return Ok(avisos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $" Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]

        public async Task <ActionResult<AvisoDto>> GetById(int id)
        {
            try
            {
                var aviso = await _avisoService.GetByIdAsync(id);
                if (aviso == null) return NotFound($"Aviso con ID {id} no encontrada. ");
                return Ok(aviso);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del seervidor: {ex.Message}");
            }
        }

        [Authorize(Roles = "Coordinador,Departamento")]
        [HttpPost]
        public async Task<ActionResult<AvisoDto>> Create([FromBody] AvisoCreateDto avisoCreateDto)
        {
            try
            {
                if (avisoCreateDto == null) return BadRequest("Datos del aviso no válidos. ");
                var nuevoAviso = await _avisoService.CreateAvisoAsync(avisoCreateDto);
                return nuevoAviso;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [Authorize(Roles = "Coordinador,Departamento")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Extract user role and ID from JWT
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

                int? coordinadorId = null;
                int? departamentoId = null;

                if (roles.Contains("Coordinador"))
                {
                    var coord = await _coordinadorService.GetByUserIdAsync(userId); //obtains the coordinator id
                    coordinadorId = coord?.Id;
                }
                else if (roles.Contains("Departamento"))
                {
                    var dept = await _departamentoService.GetByUserIdAsync(userId); //obtains the department id
                    departamentoId = dept?.Id;
                }

                await _avisoService.DeleteAvisoAsync(id, coordinadorId, departamentoId);
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


    }
}
