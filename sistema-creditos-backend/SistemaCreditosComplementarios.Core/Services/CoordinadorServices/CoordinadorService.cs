using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Dtos.Coordinador;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.ICoordinadorRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.ICoordinadorService;

namespace SistemaCreditosComplementarios.Core.Services.CoordinadorServices
{
    public class CoordinadorService : ICoordinadorService
    {
        private readonly ICoordinadorRepository _coordinadorRepository;

        public CoordinadorService(ICoordinadorRepository coordinadorRepository)
        {
            _coordinadorRepository = coordinadorRepository;
        }


        //Método para obtener un coordinador por su ID
        public async Task<CoordinadorDto> GetByIdAsync(int id)
        {
            var coordinador = await _coordinadorRepository.GetByIdAsync(id);
            if (coordinador == null) { throw new Exception("Coordinador no encontrado"); }
            return new CoordinadorDto
            {
                Id = coordinador.Id,
                Nombre = coordinador.Nombre,
                CorreoElectronico = coordinador.Usuario.Email,
                FechaRegistro = coordinador.FechaRegistro,
            };
        }

        public async Task<CoordinadorDto> GetByUserIdAsync(string userId)
        {
            var coordinador = await _coordinadorRepository.GetByUserIdAsync(userId);
            if (coordinador == null) { throw new Exception("Coordinador no encontrado"); }
            return new CoordinadorDto
            {
                Id = coordinador.Id,
                Nombre = coordinador.Nombre,
                CorreoElectronico = coordinador.Usuario.Email,
                FechaRegistro = coordinador.FechaRegistro,
            };
        }

        //Método para actualizar los datos del departamento
        public async Task<CoordinadorDto> UpdateAsync(CoordinadorUpdateDto coordinadorUpdateDto)
        {
            var coordinadorExistente = await _coordinadorRepository.GetByIdAsync(coordinadorUpdateDto.Id);
            if (coordinadorExistente == null) { throw new Exception("Coordinador no encontrado"); }
            coordinadorExistente.Nombre = coordinadorUpdateDto.Nombre;
            var coordinadorActualizado = await _coordinadorRepository.UpdateAsync(coordinadorExistente);
            return new CoordinadorDto
            {
                Id = coordinadorActualizado.Id,
                Nombre = coordinadorActualizado.Nombre,
            };
        }
    }
}
