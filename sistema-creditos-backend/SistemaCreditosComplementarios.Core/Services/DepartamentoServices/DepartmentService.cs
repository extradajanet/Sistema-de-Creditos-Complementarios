using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Dtos.Departamento;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IDepartamentoRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IDepartmentService;

namespace SistemaCreditosComplementarios.Core.Services.DepartamentoServices
{
    public class DepartmentService : IDepartamentoService
    {
        private readonly IDepartmentRepository _departamentoRepository;

        public DepartmentService(IDepartmentRepository departamentoRepository) 
        { 
            _departamentoRepository = departamentoRepository;
        }

        //Método para obtener un departamento por su ID
        public async Task<DepartamentoDto> GetByIdAsync(int id)
        {
            var departamento = await _departamentoRepository.GetByIdAsync(id);
            if (departamento == null)
            {
                throw new Exception("Departamento no encontrado");
            }
            return new DepartamentoDto
            {
                Id = departamento.Id,
                Nombre = departamento.Nombre,
                CorreoElectronico = departamento.Usuario.Email,
                FechaRegistro = departamento.FechaRegistro,
            };
        }

        public async Task<DepartamentoDto> GetByUserIdAsync(string userId)
        {
            var departamento = await _departamentoRepository.GetByUserIdAsync(userId);
            if (departamento == null)
            {
                throw new Exception("Departamento no encontrado");
            }
            return new DepartamentoDto
            {
                Id = departamento.Id,
                Nombre = departamento.Nombre,
                CorreoElectronico = departamento.Usuario.Email,
                FechaRegistro = departamento.FechaRegistro,
            };
        }

        //Método para actualizar los datos del departamento
        public async Task<DepartamentoDto> UpdateAsync(DepartamentoUpdateDto departamentoUpdateDto)
        {
            var departamentoExistente = await _departamentoRepository.GetByIdAsync(departamentoUpdateDto.Id);
            if (departamentoExistente == null) { throw new Exception("Departamento no encontrado. "); }

            departamentoExistente.Nombre = departamentoUpdateDto.Nombre;

            var departamentoActualizado = await _departamentoRepository.UpdateAsync(departamentoExistente);

            return new DepartamentoDto
            {
                Id = departamentoActualizado.Id,
                Nombre = departamentoActualizado.Nombre,
            };
        }
    }
}
