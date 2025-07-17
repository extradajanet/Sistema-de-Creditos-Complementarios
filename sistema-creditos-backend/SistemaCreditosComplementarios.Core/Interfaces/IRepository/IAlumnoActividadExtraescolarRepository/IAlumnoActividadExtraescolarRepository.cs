using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Dtos.ActividadExtraescolarAlumno;
using SistemaCreditosComplementarios.Core.Models.AlumnoActividadExtraescolares;
using SistemaCreditosComplementarios.Core.Models.Enum;

namespace SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAlumnoActividadExtraescolarRepository
{
    public interface IAlumnoActividadExtraescolarRepository
    {
        Task<IEnumerable<AlumnoActividadExtraescolar>> GetAllAsync(); 
        Task<AlumnoActividadExtraescolar> GetByIdAsync(int alumnoId, int actividadExtraescolarId); 
        Task<IEnumerable<ExtraescolarAlumnoDto>> GetExtraescolarPorAlumnoAsync(int alumnoId, EstadoAlumnoActividad? estado); 
        Task<IEnumerable<AlumnoInscritoExtraescolarDto>> GetAlumnosInscritosPorActividadAsync(int actividadExtraescolarId); 

        Task AddAsync(AlumnoActividadExtraescolar alumnoActividadExtraescolar); 
        Task UpdateAsync(int alumnoId, int actividadExtraescolarId, AlumnoActividadExtraescolar alumnoActividadExtraescolar);
        Task DeleteAsync(int alumnoId, int actividadExtraescolarId);

        Task<bool> AlumnoExisteAsync(int alumnoId);
        Task<bool> ActividadExtraescolarExisteAsync(int actividadExtraescolarId);
    }
}
