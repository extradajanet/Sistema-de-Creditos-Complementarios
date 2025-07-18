using SistemaCreditosComplementarios.Core.Dtos.Carrera;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.ICarreraRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.ICarreraService;
using SistemaCreditosComplementarios.Core.Models.Coordinadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Services.CarreraServices
{
    // Lógica de negocio para las carreras
    public class CarreraService : ICarreraService
    {
        private readonly ICarreraRepository _carreraRepository;
        public CarreraService(ICarreraRepository carreraRepository)
        {
            _carreraRepository = carreraRepository;
        }
        // Obtiene todas las carreras
        public async Task<IEnumerable<CarreraDto>> GetAll()
        {
            var carreras = await _carreraRepository.GetAll();
            return carreras.Select(c => new CarreraDto
            {
                Id = c.Id,
                Nombre = c.Nombre
            });
        }


        //Obtiene todas las carreras de un coordinador
        public async Task<IEnumerable<CarreraDto>> GetByCoordinadorId(int coordinadorId)
        {
            var carreras = await _carreraRepository.GetByCoordinadorId(coordinadorId);

            var carreraDtos = carreras.Select(c => new CarreraDto
            {
                Id = c.Id,
                Nombre = c.Nombre,

            }).ToList(); 

            return carreraDtos;
        }
    }
}
