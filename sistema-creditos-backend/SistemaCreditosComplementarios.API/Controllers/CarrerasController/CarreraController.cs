using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditosComplementarios.Core.Dtos.Carrera;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.ICarreraService;

namespace SistemaCreditosComplementarios.API.Controllers.ControllerCarreras
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarreraController : ControllerBase
    {
        private readonly ICarreraService _carreraService;

        public CarreraController(ICarreraService carreraService)
        {
            _carreraService = carreraService;
        }

        // GET: api/carreras
        [HttpGet("carreras")]
     
        public async Task<IEnumerable<CarreraDto>> GetAllCarreras()
        {
            try
            {
                var carreras = await _carreraService.GetAll();
                return carreras;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las carreras: {ex.Message}");
            }
        }
        // GET: api/carreras
        [HttpGet("coordinador/{coordinadorId}")]
        public async Task<ActionResult<IEnumerable<CarreraDto>>> GetCarrerasByCoordinadorId(int coordinadorId)
        {
            try
            {
                var carreras = await _carreraService.GetByCoordinadorId(coordinadorId);
                if (carreras == null || !carreras.Any())
                    return NotFound("No se encontraron carreras para este coordinador.");

                return Ok(carreras);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener carreras: {ex.Message}");
            }
        }



        //public async Task<IActionResult> GetAllCarreras()
        //{
        //    try
        //    {
        //        var carreras = await _carreraService.GetAll();
        //        return Ok(carreras);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}
    }
}
