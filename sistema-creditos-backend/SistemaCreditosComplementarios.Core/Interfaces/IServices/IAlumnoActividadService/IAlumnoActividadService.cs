using SistemaCreditosComplementarios.Core.Dtos.AlumnoActividad;
using SistemaCreditosComplementarios.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Interfaces.IServices.IAlumnoActividadService
{
    public interface IAlumnoActividadService
    {
        Task<IEnumerable<AlumnoActividadDto>> GetAllAsync();

        Task<AlumnoActividadDto> GetByIdAsync(int alumnoId, int actividadId);

        Task<IEnumerable<AlumnoInscritoDto>> GetAlumnosInscritosPorActividadAsync(int actividadId);
        Task<IEnumerable<CursoAlumnoDto>> GetCursosPorAlumnoAsync(int alumnoId, EstadoAlumnoActividad? estado = null, EstadoActividad? estadoAct= null);
        
        Task<AlumnoActividadDto> AddAsync(AlumnoActividadCreateDto alumnoActividadCreateDto);

        Task UpdateAsync(int alumnoId, int actividadId, EstadoAlumnoActividad estadoAlumnoActividad);

        Task DeleteAsync(int alumnoId, int actividadId);
    }
}
