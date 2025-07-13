using SistemaCreditosComplementarios.Core.Dtos.AlumnoActividad;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.ActividadRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAlumnoActividadRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAlumnoRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAlumnoActividadService;
using SistemaCreditosComplementarios.Core.Models.AlumnosActividades;
using SistemaCreditosComplementarios.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Services.AlumnoActividadServices
{
    //lógica de negocio
    public class AlumnoActividadService : IAlumnoActividadService
    {
        private readonly IAlumnoActividadRepository _alumnoActividadRepository;
        private readonly IAlumnoRepository _alumnoRepository;
        private readonly IActividadRepository _actividadRepository;
        public AlumnoActividadService(IAlumnoActividadRepository alumnoActividadRepository, IAlumnoRepository alumnoRepository, IActividadRepository actividadRepository)
        {
            _alumnoActividadRepository = alumnoActividadRepository;
            _alumnoRepository = alumnoRepository;
            _actividadRepository = actividadRepository;

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
                EstadoAlumnoActividad = aa.EstadoAlumnoActividad,
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
                EstadoAlumnoActividad = alumnoActividad.EstadoAlumnoActividad,
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
        public async Task<IEnumerable<CursoAlumnoDto>> GetCursosPorAlumnoAsync(int alumnoId, EstadoAlumnoActividad? estado = null)
        {
            var cursos = await _alumnoActividadRepository.GetCursosPorAlumnoAsync(alumnoId, estado);
            return cursos;
        }

        // añade una relación con curso y alumno
        public async Task<AlumnoActividadDto> AddAsync(AlumnoActividadCreateDto alumnoActividadCreateDto)
        {
            var alumno = await _alumnoRepository.GetByIdAsync(alumnoActividadCreateDto.AlumnoId)
                ?? throw new Exception($"El alumno con ID {alumnoActividadCreateDto.AlumnoId} no existe.");

            var actividad = await _actividadRepository.GetByIdAsync(alumnoActividadCreateDto.ActividadId)
                ?? throw new Exception($"La actividad con ID {alumnoActividadCreateDto.ActividadId} no existe.");

            // Verifica si el alumno ya está inscrito en la actividad
            var actividadExistente = await _alumnoActividadRepository.GetByIdAsync(alumnoActividadCreateDto.AlumnoId, alumnoActividadCreateDto.ActividadId);

            if (actividadExistente != null)
            {
                throw new Exception($"El alumno con ID {alumnoActividadCreateDto.AlumnoId} ya está inscrito en la actividad con ID {alumnoActividadCreateDto.ActividadId}.");
            }

            //obtener la cantidad de alumnos inscritos en la actividad
            var inscritos = await _alumnoActividadRepository.GetAlumnosInscritosPorActividadAsync(alumnoActividadCreateDto.ActividadId);
            int cantidadInscritos = inscritos.Count();

            // Verifica si la actividad tiene un límite de inscritos y si se ha alcanzado
            if (cantidadInscritos >= actividad.Capacidad)
            {
                throw new Exception($"La actividad con ID {alumnoActividadCreateDto.ActividadId} ha alcanzado su capacidad máxima de inscritos.");
            }

            var alumnoActividad = new AlumnoActividad
            {
                IdAlumno = alumnoActividadCreateDto.AlumnoId,
                IdActividad = alumnoActividadCreateDto.ActividadId,
                EstadoAlumnoActividad = alumnoActividadCreateDto.EstadoAlumnoActividad,
                FechaInscripcion = DateTime.UtcNow
            };

            // Si el estado es "Acreditado", se suman los créditos de la actividad al alumno
            if (alumnoActividadCreateDto.EstadoAlumnoActividad == EstadoAlumnoActividad.Acreditado)
            {
                alumno.TotalCreditos += actividad.Creditos;
                await _alumnoRepository.UpdateAsync(alumno); // Actualiza el alumno con los nuevos créditos
            }
            
            await _alumnoActividadRepository.AddAsync(alumnoActividad); // llama al repositorio para agregar la actividad del alumno


            return new AlumnoActividadDto
            {
                AlumnoId = alumnoActividad.IdAlumno,
                AlumnoNombre = $"{alumno.Nombre} {alumno.Apellido}",
                ActividadId = alumnoActividad.IdActividad,
                ActividadNombre = actividad.Nombre,
                EstadoAlumnoActividad = alumnoActividad.EstadoAlumnoActividad,
                FechaRegistro = alumnoActividad.FechaInscripcion,
                CreditosObtenidos = actividad.Creditos
            };
        }

        // actualiza la actividad de un alumno
        public async Task UpdateAsync(int alumnoId, int actividadId, AlumnoActividadUpdate alumnoActividadDto)
        {
            var alumnoActividad = await _alumnoActividadRepository.GetByIdAsync(alumnoId, actividadId) 
                ?? throw new Exception("Actividad del alumno no encontrada para actualizar.");

            var alumno = await _alumnoRepository.GetByIdAsync(alumnoId) ?? throw new Exception($"El alumno con ID {alumnoId} no existe.");

            var estadoAnterior = alumnoActividad.EstadoAlumnoActividad;
            var estadoNuevo = alumnoActividadDto.EstadoAlumnoActividad;

            // Si el estado ha cambiado de "Acreditado" a cualquier otro estado, se restan los créditos
            if (estadoAnterior == EstadoAlumnoActividad.Acreditado && estadoNuevo != EstadoAlumnoActividad.Acreditado)
            {
                alumno.TotalCreditos -= alumnoActividad.Actividad.Creditos; // Resta los créditos del alumno
                await _alumnoRepository.UpdateAsync(alumno); 
            }
            else if (estadoAnterior != EstadoAlumnoActividad.Acreditado && estadoNuevo == EstadoAlumnoActividad.Acreditado)
            {
                // Si el estado ha cambiado a "Acreditado", se suman los créditos
                alumno.TotalCreditos += alumnoActividad.Actividad.Creditos; // Aumenta los créditos del alumno
                await _alumnoRepository.UpdateAsync(alumno); 
            }

            // Actualiza los campos necesarios
            alumnoActividad.EstadoAlumnoActividad = alumnoActividadDto.EstadoAlumnoActividad;

            // Puedes agregar más campos si es necesario
            await _alumnoActividadRepository.UpdateAsync(alumnoId, actividadId, alumnoActividad);
        }

        // elimina la actividad de un alumno
        public async Task DeleteAsync(int alumnoId, int actividadId)
        {
            var alumnoActividad = await _alumnoActividadRepository.GetByIdAsync(alumnoId, actividadId);

            var alumno = await _alumnoRepository.GetByIdAsync(alumnoId) ?? throw new Exception($"El alumno con ID {alumnoId} no existe.");

            if (alumno.TotalCreditos >= alumnoActividad.Actividad.Creditos)
            {
                alumno.TotalCreditos -= alumnoActividad.Actividad.Creditos;
                await _alumnoRepository.UpdateAsync(alumno); // Actualiza el alumno con los nuevos créditos
            }

            if (alumnoActividad == null)
            {
                throw new Exception("Actividad del alumno no encontrada para eliminar.");
            }
            await _alumnoActividadRepository.DeleteAsync(alumnoId, actividadId);
        }
    }
}
