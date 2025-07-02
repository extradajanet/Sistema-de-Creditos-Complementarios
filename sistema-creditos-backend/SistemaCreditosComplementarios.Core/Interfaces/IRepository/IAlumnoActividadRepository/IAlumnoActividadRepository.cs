using SistemaCreditosComplementarios.Core.Dtos.AlumnoActividad;
using SistemaCreditosComplementarios.Core.Models.AlumnosActividades;
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
        Task<AlumnoActividad> GetByIdAsync(int alumnoId, int actividadId); // obtiene por id la actividad de un alumno
        Task<IEnumerable<CursoAlumnoDto>> GetCursosPorAlumnoAsync(int alumnoId); // alumno ve sus cursos
        Task<IEnumerable<AlumnoInscritoDto>> GetAlumnosInscritosPorActividadAsync(int actividadId); // departamento ve lista de alumnos en un curso

        Task AddAsync(AlumnoActividad alumnoActividad); // agrega una actividad a un alumno
        Task UpdateAsync(int alumnoId, int actividadId, AlumnoActividad alumnoActividad);
        Task DeleteAsync(int alumnoId, int actividadId);
    }
}
