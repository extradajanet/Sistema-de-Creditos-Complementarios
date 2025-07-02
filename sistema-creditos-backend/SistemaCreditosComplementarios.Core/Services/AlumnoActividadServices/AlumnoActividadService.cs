using SistemaCreditosComplementarios.Core.Dtos.AlumnoActividad;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAlumnoActividadRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAlumnoActividadService;
using SistemaCreditosComplementarios.Core.Models.AlumnosActividades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Services.AlumnoActividadServices
{
    //lógica de negocio
    public class AlumnoActividadService : IAlumnoActividadService
    {
        private readonly IAlumnoActividadRepository _alumnoActividadRepository;
        public AlumnoActividadService(IAlumnoActividadRepository alumnoActividadRepository)
        {
            _alumnoActividadRepository = alumnoActividadRepository;
        }

        // obtiene todas las actividades de los alumnos
        public async Task<IEnumerable<AlumnoActividadDto>> GetAllAsync()
        {
            var alumnoActividades = await _alumnoActividadRepository.GetAllAsync();
            return alumnoActividades.Select(aa => new AlumnoActividadDto // convierte el modelo AlumnoActividad a AlumnoActividadDto
            {
                AlumnoId = aa.IdAlumno,
                AlumnoNombre = aa.Alumno.Nombre + " " + aa.Alumno.Apellido,
                ActividadId = aa.IdActividad,
                ActividadNombre = aa.Actividad.Nombre,
                EstadoActividad = aa.EstadoActividad,
                FechaRegistro = aa.FechaInscripcion,
                CreditosObtenidos = aa.Actividad.Creditos
            });
        }


        public async Task<AlumnoActividadDto> GetByIdAsync(int alumnoId, int actividadId) // obtiene por id la actividad de un alumno
        {
            var alumnoActividad = await _alumnoActividadRepository.GetByIdAsync(alumnoId, actividadId);
            if (alumnoActividad == null)
            {
                throw new Exception("Actividad del alumno no encontrada.");
            }
            return new AlumnoActividadDto
            {
                AlumnoId = alumnoActividad.IdAlumno,
                AlumnoNombre = alumnoActividad.Alumno.Nombre + " " + alumnoActividad.Alumno.Apellido,
                ActividadId = alumnoActividad.IdActividad,
                ActividadNombre = alumnoActividad.Actividad.Nombre,
                EstadoActividad = alumnoActividad.EstadoActividad,
                FechaRegistro = alumnoActividad.FechaInscripcion,
                CreditosObtenidos = alumnoActividad.Actividad.Creditos
            };
        }

        //obtener a los alumnos inscritos por actividad
        public async Task<IEnumerable<AlumnoInscritoDto>> GetAlumnosInscritosPorActividadAsync(int actividadId)
        {
            var alumnos = await _alumnoActividadRepository.GetAlumnosInscritosPorActividadAsync(actividadId);
            return alumnos;
        }

        // obtener los cursos por alumno
        public async Task<IEnumerable<CursoAlumnoDto>> GetCursosPorAlumnoAsync(int alumnoId)
        {
            var cursos = await _alumnoActividadRepository.GetCursosPorAlumnoAsync(alumnoId);
            return cursos;
        }

        // añade una relación con cuso y alumno
        public async Task<AlumnoActividadDto> AddAsync(AlumnoActividadCreateDto alumnoActividadCreateDto)
        {
            // Validar si el alumno existe
            var alumnoExiste = await _alumnoActividadRepository.AlumnoExisteAsync(alumnoActividadCreateDto.AlumnoId);
            if (!alumnoExiste)
                throw new Exception($"El alumno con ID {alumnoActividadCreateDto.AlumnoId} no existe.");

            // Validar si la actividad existe
            var actividadExiste = await _alumnoActividadRepository.ActividadExisteAsync(alumnoActividadCreateDto.ActividadId);
            if (!actividadExiste)
                throw new Exception($"La actividad con ID {alumnoActividadCreateDto.ActividadId} no existe.");

            var alumnoActividad = new AlumnoActividad
            {
                IdAlumno = alumnoActividadCreateDto.AlumnoId,
                IdActividad = alumnoActividadCreateDto.ActividadId,
                EstadoActividad = alumnoActividadCreateDto.EstadoActividad,
                FechaInscripcion = DateTime.UtcNow
            };

            await _alumnoActividadRepository.AddAsync(alumnoActividad); // llama al repositorio para agregar la actividad del alumno

            // Obtener datos para devolver en el DTO
            var actividad = await _alumnoActividadRepository.GetByIdAsync(alumnoActividad.IdAlumno, alumnoActividad.IdActividad);
            if (actividad == null)
                throw new Exception("No se pudo recuperar la relación después de guardarla.");

            return new AlumnoActividadDto
            {
                AlumnoId = actividad.IdAlumno,
                AlumnoNombre = actividad.Alumno.Nombre + " " + actividad.Alumno.Apellido,
                ActividadId = actividad.IdActividad,
                ActividadNombre = actividad.Actividad.Nombre,
                EstadoActividad = actividad.EstadoActividad,
                FechaRegistro = actividad.FechaInscripcion,
                CreditosObtenidos = actividad.Actividad.Creditos
            };
        }

        // actualiza la actividad de un alumno
        public async Task UpdateAsync(int alumnoId, int actividadId, AlumnoActividadUpdate alumnoActividadDto)
        {
            var alumnoActividad = await _alumnoActividadRepository.GetByIdAsync(alumnoId, actividadId);
            if (alumnoActividad == null)
            {
                throw new Exception("Actividad del alumno no encontrada para actualizar.");
            }
            // Actualiza los campos necesarios
            alumnoActividad.EstadoActividad = alumnoActividadDto.EstadoActividad;
            // Puedes agregar más campos si es necesario
            await _alumnoActividadRepository.UpdateAsync(alumnoId, actividadId, alumnoActividad);
        }

        // elimina la actividad de un alumno
        public async Task DeleteAsync(int alumnoId, int actividadId)
        {
            var alumnoActividad = await _alumnoActividadRepository.GetByIdAsync(alumnoId, actividadId);
            if (alumnoActividad == null)
            {
                throw new Exception("Actividad del alumno no encontrada para eliminar.");
            }
            await _alumnoActividadRepository.DeleteAsync(alumnoId, actividadId);
        }
    }
}
