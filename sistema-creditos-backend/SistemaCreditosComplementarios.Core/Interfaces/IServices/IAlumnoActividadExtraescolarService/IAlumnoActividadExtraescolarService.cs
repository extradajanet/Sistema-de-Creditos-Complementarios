using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Dtos.ActividadExtraescolarAlumno;
using SistemaCreditosComplementarios.Core.Models.Enum;

namespace SistemaCreditosComplementarios.Core.Interfaces.IServices.IAlumnoActividadExtraescolarService
{
    public interface IAlumnoActividadExtraescolarService
    {
        Task<IEnumerable<AlumnoActividadExtraescolarDto>> GetAllAsync();

        Task<AlumnoActividadExtraescolarDto> GetByIdAsync(int alumnoId, int actividadExtraescolarId);

        Task<IEnumerable<AlumnoInscritoExtraescolarDto>> GetAlumnosInscritosPorActividadAsync(int actividadExtraescolarId);
        Task<IEnumerable<ExtraescolarAlumnoDto>> GetCursosPorAlumnoAsync(int alumnoId, EstadoAlumnoActividad? estado = null);

        Task<AlumnoActividadExtraescolarDto> AddAsync(AlumnoActividadExtraescolarCreateDto alumnoActividadCreateDto);

        Task UpdateAsync(int alumnoId, int actividadExtraescolarId, AlumnoActividadExtraescolarUpdate alumnoActividadExtraescolarDto);

        Task DeleteAsync(int alumnoId, int actividadExtraescolarId);
    }
}
