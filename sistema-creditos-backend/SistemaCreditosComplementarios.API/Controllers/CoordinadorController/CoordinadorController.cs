﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditosComplementarios.Core.Dtos.Coordinador;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.ICoordinadorService;

namespace SistemaCreditosComplementarios.API.Controllers.CoordinadorController
{
    /// <summary>
    /// Controlador para gestionar coordinadores.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CoordinadorController : ControllerBase
    {
        private readonly ICoordinadorService _coordinadorService;

        /// <summary>
        /// Constructor del controlador de coordinadores.
        /// </summary>
        /// <param name="coordinadorService">Servicio de coordinadores.</param>
        public CoordinadorController(ICoordinadorService coordinadorService)
        {
            _coordinadorService = coordinadorService;
        }

        /// <summary>
        /// Obtiene un coordinador por su ID.
        /// </summary>
        /// <param name="id">ID del coordinador.</param>
        /// <returns>El coordinador correspondiente o un mensaje de error si no se encuentra.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CoordinadorDto>> GetById(int id)
        {
            try
            {
                var coordinador = await _coordinadorService.GetByIdAsync(id);
                if (coordinador == null)
                    return NotFound($"Coordinador con ID {id} no encontrado. ");

                return Ok(coordinador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza los datos de un coordinador existente.
        /// </summary>
        /// <param name="coordinadorUpdateDto">Datos actualizados del coordinador.</param>
        /// <returns>El coordinador actualizado o un mensaje de error si la actualización falla.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<CoordinadorDto>> Update([FromBody] CoordinadorUpdateDto coordinadorUpdateDto)
        {
            try
            {
                if (coordinadorUpdateDto == null)
                    return BadRequest("Datos del Coordinador no válidos");

                var updatedCoordinador = await _coordinadorService.UpdateAsync(coordinadorUpdateDto);
                return Ok(updatedCoordinador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
