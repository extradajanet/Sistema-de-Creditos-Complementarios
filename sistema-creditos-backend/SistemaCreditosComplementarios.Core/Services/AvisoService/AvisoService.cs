using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Dtos.Aviso;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAvisoRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.ICoordinadorRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IDepartamentoRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAvisoService;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IDepartmentService;
using SistemaCreditosComplementarios.Core.Models.Avisos;

namespace SistemaCreditosComplementarios.Core.Services.AvisoService
{
    public class AvisoService : IAvisoService
    {
        private readonly IAvisoRepository _avisoRepository;
        private readonly IDepartmentRepository _departamentoRepository;
        private readonly ICoordinadorRepository _coordinadorRepository;

        public AvisoService(IAvisoRepository avisoRepository, IDepartmentRepository departmentRepository, ICoordinadorRepository coordinadorRepository)
        {
            _avisoRepository = avisoRepository;
            _departamentoRepository = departmentRepository;
            _coordinadorRepository = coordinadorRepository;
        }

        public async Task<IEnumerable<AvisoDto>> GetAllAvisoAsync()
        {
            var avisos = await _avisoRepository.GetAllAvisoAsync();
            return avisos.Select(aviso => new AvisoDto
            {
                Id = aviso.Id,
                Titulo = aviso.Titulo,
                Mensaje = aviso.Mensaje,
                DepartamentoId = aviso.DepartamentoId ?? 0,
                DepartamentoNombre = aviso.Departamento?.Nombre,
                CoordinadorId = aviso.CoordinadorId ?? 0,
                CoordinadorNombre = aviso.Coordinador?.Nombre,
                CoordinadorApellido = aviso.Coordinador?.Apellido,
            });
        }

        public async Task<AvisoDto> CreateAvisoAsync(AvisoCreateDto createDto)
        {
            // Validation: only one sender allowed
            if (createDto.CoordinadorId != null && createDto.DepartamentoId != null)
            {
                throw new Exception("Solo puede haber un remitente: Coordinador o Departamento.");
            }

            if (createDto.CoordinadorId == null && createDto.DepartamentoId == null)
            {
                throw new Exception("Debe especificar un Coordinador o un Departamento como remitente.");
            }

            var aviso = new Aviso
            {
                Titulo = createDto.Titulo,
                Mensaje = createDto.Mensaje,
                DepartamentoId = createDto?.DepartamentoId,
                CoordinadorId = createDto?.CoordinadorId,
            };

            var created = await _avisoRepository.CreateAvisoAsync(aviso);

            return new AvisoDto
            {
                Id = created.Id,
                Titulo = created.Titulo,
                Mensaje = created.Mensaje,
                DepartamentoId = created?.DepartamentoId,
                CoordinadorId = created?.CoordinadorId,
              
            };

        }

    }
}
