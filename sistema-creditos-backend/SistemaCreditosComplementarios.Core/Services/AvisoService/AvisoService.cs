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
                Fecha = aviso.Fecha,
                DepartamentoId = aviso.DepartamentoId ?? 0,
                DepartamentoNombre = aviso.Departamento?.Nombre,
                CoordinadorId = aviso.CoordinadorId ?? 0,
                CoordinadorNombre = aviso.Coordinador?.Nombre,
                CoordinadorApellido = aviso.Coordinador?.Apellido,
            });
        }

        public async Task<AvisoDto> GetByIdAsync(int id)
        {
            var aviso = await _avisoRepository.GetByIdAsync(id);
            if (aviso == null)
            {
                throw new Exception("Aviso no encontrado");
            }
            return new AvisoDto
            {
                Id = aviso.Id,
                Titulo = aviso.Titulo,
                Mensaje = aviso.Mensaje,
                Fecha = aviso.Fecha,
                DepartamentoId = aviso.DepartamentoId ?? 0,
                DepartamentoNombre = aviso.Departamento?.Nombre,
                CoordinadorId = aviso.CoordinadorId ?? 0,
                CoordinadorNombre = aviso.Coordinador?.Nombre,
                CoordinadorApellido = aviso.Coordinador?.Apellido,
            };
        }

        public async Task<AvisoDto> CreateAvisoAsync(AvisoCreateDto createDto)
        {
            if (createDto.CoordinadorId == 0)
                createDto.CoordinadorId = null;

            if (createDto.DepartamentoId == 0)
                createDto.DepartamentoId = null;
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
                Fecha = DateTime.UtcNow,
                DepartamentoId = createDto?.DepartamentoId,
                CoordinadorId = createDto?.CoordinadorId,
            };

            var created = await _avisoRepository.CreateAvisoAsync(aviso);

            return new AvisoDto
            {
                Id = created.Id,
                Titulo = created.Titulo,
                Mensaje = created.Mensaje,
                Fecha= DateTime.UtcNow,
                DepartamentoId = created?.DepartamentoId,
                CoordinadorId = created?.CoordinadorId,
              
            };

        }


        public async Task DeleteAvisoAsync(int id, int? coordinadorId, int? departamentoId)
        {
            var aviso = await _avisoRepository.GetByIdAsync(id);
            if (aviso == null) { throw new Exception("Aviso no encontrado"); }


            //Ownership Validation
            var isOwnedByRequester = (aviso.DepartamentoId != null && aviso.DepartamentoId == departamentoId) ||(aviso.CoordinadorId != null && aviso.CoordinadorId == coordinadorId);

            if (!isOwnedByRequester)
            {
                throw new UnauthorizedAccessException("No tienes permisos para eliminar este aviso.");
            }



            await _avisoRepository.DeleteAvisoAsync(aviso);
        }

    }
}
