using SistemaCreditosComplementarios.Core.Dtos.AlumnoActividad;
using SistemaCreditosComplementarios.Core.Models.AlumnosActividades;
using SistemaCreditosComplementarios.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAlumnoActividadRepository
{
    public interface IAlumnoActividadRepository
    {
        Task<IEnumerable<AlumnoActividad>> GetAllAsync(); // obtiene todas las actividades de los alumnos
        Task<AlumnoActividad> GetByIdAsync(int alumnoId, int actividadId); // se obtiene la actividad la actvidad de un alumno por su id y el id de la actividad
        Task<IEnumerable<AlumnoActividad>> GetCursosPorAlumnoAsync(int alumnoId, EstadoAlumnoActividad? estado, EstadoActividad? estadoAct); // alumno ve sus cursos
        Task<IEnumerable<AlumnoActividad>> GetAlumnosInscritosPorActividadAsync(int actividadId); // departamento ve lista de alumnos en un curso

        Task AddAsync(AlumnoActividad alumnoActividad); // agrega una actividad a un alumno
        Task UpdateAsync(int alumnoId, int actividadId, EstadoAlumnoActividad estadoAlumnoActividad);
        Task DeleteAsync(int alumnoId, int actividadId);

        Task<bool> AlumnoExisteAsync(int alumnoId);
        Task<bool> ActividadExisteAsync(int actividadId);

    }
}
