using SistemaCreditosComplementarios.Core.Dtos.AlumnoActividad;
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
        Task<IEnumerable<CursoAlumnoDto>> GetCursosPorAlumnoAsync(int alumnoId);
        
        Task<AlumnoActividadDto> AddAsync(AlumnoActividadCreateDto alumnoActividadCreateDto);

        Task UpdateAsync(int alumnoId, int actividadId, AlumnoActividadUpdate alumnoActividadDto);

        Task DeleteAsync(int alumnoId, int actividadId);
    }
}
