using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Dtos.ActividadExtraescolarAlumno;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.ActividadRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IActividadExtraescolarRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAlumnoActividadExtraescolarRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAlumnoActividadRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAlumnoRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAlumnoActividadExtraescolarService;
using SistemaCreditosComplementarios.Core.Models.ActividadExtraescolarModel;
using SistemaCreditosComplementarios.Core.Models.AlumnoActividadExtraescolares;
using SistemaCreditosComplementarios.Core.Models.AlumnosActividades;
using SistemaCreditosComplementarios.Core.Models.Enum;

namespace SistemaCreditosComplementarios.Core.Services.AlumnoActividadExtraescolarServices
{
    public class AlumnoActividadExtraescolarService : IAlumnoActividadExtraescolarService
    {
        private readonly IAlumnoActividadExtraescolarRepository _alumnoActividadExtraescolarRepository;
        private readonly IAlumnoRepository _alumnoRepository;
        private readonly IActividadExtraescolarRepository _actividadExtraescolarRepository;
        public AlumnoActividadExtraescolarService(IAlumnoActividadExtraescolarRepository alumnoActividadExtraescolarRepository, IAlumnoRepository alumnoRepository, IActividadExtraescolarRepository actividadExtraescolarRepository)
        {
            _alumnoActividadExtraescolarRepository = alumnoActividadExtraescolarRepository;
            _alumnoRepository = alumnoRepository;
            _actividadExtraescolarRepository = actividadExtraescolarRepository;

        }

        // obtiene todas las actividades de los alumnos
        public async Task<IEnumerable<AlumnoActividadExtraescolarDto>> GetAllAsync()
        {
            var alumnoActividadesExtraescolar = await _alumnoActividadExtraescolarRepository.GetAllAsync();
            return alumnoActividadesExtraescolar.Select(aa => new AlumnoActividadExtraescolarDto 
            {
                AlumnoId = aa.IdAlumno,
                AlumnoNombre = aa.Alumno.Nombre + " " + aa.Alumno.Apellido,
                ActividadExtraescolarId = aa.IdActividadExtraescolar,
                ActividadExtraescolarNombre = aa.ActividadExtraescolar.Nombre,
                EstadoAlumnoActividad = aa.EstadoAlumnoActividad,
                FechaRegistro = aa.FechaInscripcion,
                CreditosObtenidos = aa.ActividadExtraescolar.Creditos
            });
        }


        public async Task<AlumnoActividadExtraescolarDto> GetByIdAsync(int alumnoId, int actividadExtraescolarId) 
        {
            var alumnoActividadExtraescolar = await _alumnoActividadExtraescolarRepository.GetByIdAsync(alumnoId, actividadExtraescolarId);
            if (alumnoActividadExtraescolar == null)
            {
                throw new Exception("Actividad extraescolar del alumno no encontrada.");
            }
            return new AlumnoActividadExtraescolarDto
            {
                AlumnoId = alumnoActividadExtraescolar.IdAlumno,
                AlumnoNombre = alumnoActividadExtraescolar.Alumno.Nombre + " " + alumnoActividadExtraescolar.Alumno.Apellido,
                ActividadExtraescolarId = alumnoActividadExtraescolar.IdActividadExtraescolar,
                ActividadExtraescolarNombre = alumnoActividadExtraescolar.ActividadExtraescolar.Nombre,
                EstadoAlumnoActividad = alumnoActividadExtraescolar.EstadoAlumnoActividad,
                FechaRegistro = alumnoActividadExtraescolar.FechaInscripcion,
                CreditosObtenidos = alumnoActividadExtraescolar.ActividadExtraescolar.Creditos
            };
        }

   
        public async Task<IEnumerable<AlumnoInscritoExtraescolarDto>> GetAlumnosInscritosPorActividadAsync(int actividadExtraescolarId)
        {
            var alumnos = await _alumnoActividadExtraescolarRepository.GetAlumnosInscritosPorActividadAsync(actividadExtraescolarId);
            return alumnos;
        }

      
        public async Task<IEnumerable<ExtraescolarAlumnoDto>> GetCursosPorAlumnoAsync(int alumnoId, EstadoAlumnoActividad? estado = null)
        {
            var extraescolar = await _alumnoActividadExtraescolarRepository.GetExtraescolarPorAlumnoAsync(alumnoId, estado);
            return extraescolar;
        }

        
        public async Task<AlumnoActividadExtraescolarDto> AddAsync(AlumnoActividadExtraescolarCreateDto alumnoActividadExtraescolarCreateDto)
        {
            var alumno = await _alumnoRepository.GetByIdAsync(alumnoActividadExtraescolarCreateDto.AlumnoId)
                ?? throw new Exception($"El alumno con ID {alumnoActividadExtraescolarCreateDto.AlumnoId} no existe.");

            var actividadExtraescolar = await _actividadExtraescolarRepository.GetByIdAsync(alumnoActividadExtraescolarCreateDto.ActividadExtraescolarId)
                ?? throw new Exception($"La actividad extraescolar con ID {alumnoActividadExtraescolarCreateDto.ActividadExtraescolarId} no existe.");

        
            var actividadExistente = await _alumnoActividadExtraescolarRepository.GetByIdAsync(alumnoActividadExtraescolarCreateDto.AlumnoId, alumnoActividadExtraescolarCreateDto.ActividadExtraescolarId);

            if (actividadExistente != null)
            {
                throw new Exception($"El alumno con ID {alumnoActividadExtraescolarCreateDto.AlumnoId} ya está inscrito en la actividad con ID {alumnoActividadExtraescolarCreateDto.ActividadExtraescolarId}.");
            }

            var inscritos = await _alumnoActividadExtraescolarRepository.GetAlumnosInscritosPorActividadAsync(alumnoActividadExtraescolarCreateDto.ActividadExtraescolarId);
            int cantidadInscritos = inscritos.Count();

            if (cantidadInscritos >= actividadExtraescolar.Capacidad)
            {
                throw new Exception($"La actividad extraescolar con ID {alumnoActividadExtraescolarCreateDto.ActividadExtraescolarId} ha alcanzado su capacidad máxima de inscritos.");
            }

            var alumnoActividadExtraescolar = new AlumnoActividadExtraescolar
            {
                IdAlumno = alumnoActividadExtraescolarCreateDto.AlumnoId,
                IdActividadExtraescolar = alumnoActividadExtraescolarCreateDto.ActividadExtraescolarId,
                EstadoAlumnoActividad = alumnoActividadExtraescolarCreateDto.EstadoAlumnoActividad,
                FechaInscripcion = DateTime.UtcNow
            };

            if (alumnoActividadExtraescolarCreateDto.EstadoAlumnoActividad == EstadoAlumnoActividad.Acreditado)
            {
                alumno.TotalCreditos += actividadExtraescolar.Creditos;
                await _alumnoRepository.UpdateAsync(alumno); 
            }

            await _alumnoActividadExtraescolarRepository.AddAsync(alumnoActividadExtraescolar); 


            return new AlumnoActividadExtraescolarDto
            {
                AlumnoId = alumnoActividadExtraescolar.IdAlumno,
                AlumnoNombre = $"{alumno.Nombre} {alumno.Apellido}",
                ActividadExtraescolarId = alumnoActividadExtraescolar.IdActividadExtraescolar,
                ActividadExtraescolarNombre = actividadExtraescolar.Nombre,
                EstadoAlumnoActividad = alumnoActividadExtraescolar.EstadoAlumnoActividad,
                FechaRegistro = alumnoActividadExtraescolar.FechaInscripcion,
                CreditosObtenidos = actividadExtraescolar.Creditos
            };
        }

        public async Task UpdateAsync(int alumnoId, int actividadExtraescolarId, AlumnoActividadExtraescolarUpdate alumnoActividadExtraescolarDto)
        {
            var alumnoActividadExtraescolar = await _alumnoActividadExtraescolarRepository.GetByIdAsync(alumnoId, actividadExtraescolarId)
                ?? throw new Exception("Actividad extraescolar del alumno no encontrada para actualizar.");

            var alumno = await _alumnoRepository.GetByIdAsync(alumnoId) ?? throw new Exception($"El alumno con ID {alumnoId} no existe.");

            var estadoAnterior = alumnoActividadExtraescolar.EstadoAlumnoActividad;
            var estadoNuevo = alumnoActividadExtraescolarDto.EstadoAlumnoActividad;

            if (estadoAnterior == EstadoAlumnoActividad.Acreditado && estadoNuevo != EstadoAlumnoActividad.Acreditado)
            {
                alumno.TotalCreditos -= alumnoActividadExtraescolar.ActividadExtraescolar.Creditos; 
                await _alumnoRepository.UpdateAsync(alumno);
            }
            else if (estadoAnterior != EstadoAlumnoActividad.Acreditado && estadoNuevo == EstadoAlumnoActividad.Acreditado)
            {
                alumno.TotalCreditos += alumnoActividadExtraescolar.ActividadExtraescolar.Creditos; 
                await _alumnoRepository.UpdateAsync(alumno);
            }

            alumnoActividadExtraescolar.EstadoAlumnoActividad = alumnoActividadExtraescolarDto.EstadoAlumnoActividad;

            await _alumnoActividadExtraescolarRepository.UpdateAsync(alumnoId, actividadExtraescolarId, alumnoActividadExtraescolar);
        }

        // elimina la actividad de un alumno
        public async Task DeleteAsync(int alumnoId, int actividadExtraescolarId)
        {
            var alumnoActividadExtraescolar = await _alumnoActividadExtraescolarRepository.GetByIdAsync(alumnoId, actividadExtraescolarId);

            var alumno = await _alumnoRepository.GetByIdAsync(alumnoId) ?? throw new Exception($"El alumno con ID {alumnoId} no existe.");

            if (alumno.TotalCreditos >= alumnoActividadExtraescolar.ActividadExtraescolar.Creditos)
            {
                alumno.TotalCreditos -= alumnoActividadExtraescolar.ActividadExtraescolar.Creditos;
                await _alumnoRepository.UpdateAsync(alumno); // Actualiza el alumno con los nuevos créditos
            }

            if (alumnoActividadExtraescolar == null)
            {
                throw new Exception("Actividad del alumno no encontrada para eliminar.");
            }
            await _alumnoActividadExtraescolarRepository.DeleteAsync(alumnoId, actividadExtraescolarId);
        }
    }
}
