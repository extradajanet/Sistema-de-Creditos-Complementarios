using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditosComplementarios.Core.Dtos.Carrera;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.ICarreraService;

namespace SistemaCreditosComplementarios.API.Controllers.ControllerCarreras
{
    /// <summary>
    /// Controlador para gestionar carreras.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CarreraController : ControllerBase
    {
        private readonly ICarreraService _carreraService;

        /// <summary>
        /// Constructor del controlador de carreras.
        /// </summary>
        /// <param name="carreraService">Servicio de carreras.</param>
        public CarreraController(ICarreraService carreraService)
        {
            _carreraService = carreraService;
        }

        /// <summary>
        /// Obtiene todas las carreras disponibles.
        /// </summary>
        /// <returns>Lista de carreras.</returns>
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

        /// <summary>
        /// Obtiene las carreras asociadas a un coordinador específico.
        /// </summary>
        /// <param name="coordinadorId">ID del coordinador.</param>
        /// <returns>Lista de carreras relacionadas con el coordinador.</returns>
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

        //// Método alternativo (comentado) que devuelve IActionResult en lugar de IEnumerable directamente
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
