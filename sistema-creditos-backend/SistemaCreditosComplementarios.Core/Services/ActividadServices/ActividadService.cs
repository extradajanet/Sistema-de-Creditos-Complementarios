using SistemaCreditosComplementarios.Core.Dtos.Actividad;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.ActividadRepository;
<<<<<<< HEAD
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.ICarreraRepository;
=======
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAlumnoActividadRepository;
>>>>>>> 5d9d5bdc0a9dc4dcca375b423a6abce31b598e07
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IActividadService;
using SistemaCreditosComplementarios.Core.Models.ActividadesCarreras;
using SistemaCreditosComplementarios.Core.Models.ActividadModel;
using SistemaCreditosComplementarios.Core.Models.CarreraModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Services.ActividadService
{
    //lógica de negocio
    public class ActividadService: IActividadService
    {
        private readonly IActividadRepository _actividadRepository;
        private readonly ICarreraRepository _carreraRepository;
        public ActividadService(IActividadRepository actividadRepository, ICarreraRepository carreraRepository)
        {
            _actividadRepository = actividadRepository;
            _carreraRepository = carreraRepository;
        }

        // obtiene todas las actividades
        public async Task<IEnumerable<ActividadDto>> GetAllAsync()
        {
            var actividades = await _actividadRepository.GetAllAsync();
            return actividades.Select(a => new ActividadDto
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Descripcion = a.Descripcion,
                FechaInicio = a.FechaInicio,
                FechaFin = a.FechaFin,
                Creditos = a.Creditos,
                Capacidad = a.Capacidad,
                Dias = a.Dias,
                HoraInicio = a.HoraInicio,
                HoraFin = a.HoraFin,
                TipoActividad = a.TipoActividad,
                EstadoActividad = a.EstadoActividad,
                ImagenNombre = a.ImagenNombre,
                DepartamentoId = a.DepartamentoId,
                DepartamentoNombre = a.Departamento?.Nombre,
                CarreraNombres = a.ActividadesCarreras?.Select(ac => ac.Carrera?.Nombre).ToList() ?? new List<string>()
            });
        }

        // obtiene por id
        public async Task<ActividadDto> GetByIdAsync(int id)
        {
            var actividad = await _actividadRepository.GetByIdAsync(id);
            if (actividad == null)
            {
                throw new Exception("Actividad no encontrada.");
            }
            return new ActividadDto
            {
                Id = actividad.Id,
                Nombre = actividad.Nombre,
                Descripcion = actividad.Descripcion,
                FechaInicio = actividad.FechaInicio,
                FechaFin = actividad.FechaFin,
                Creditos = actividad.Creditos,
                Capacidad = actividad.Capacidad,
                Dias = actividad.Dias,
                HoraInicio = actividad.HoraInicio,
                HoraFin = actividad.HoraFin,
                TipoActividad = actividad.TipoActividad,
                EstadoActividad = actividad.EstadoActividad,
                ImagenNombre = actividad.ImagenNombre,
                DepartamentoId = actividad.DepartamentoId,
                DepartamentoNombre = actividad.Departamento?.Nombre,
                CarreraNombres = actividad.ActividadesCarreras?.Select(ac => ac.Carrera?.Nombre).ToList() ?? new List<string>()

            };
        }

        // agrega actividad
        public async Task<ActividadDto> AddAsync(ActividadCreateDto actividadCreateDto)
        {

            var actividad = new Actividad
            {
                Nombre = actividadCreateDto.Nombre,
                Descripcion = actividadCreateDto.Descripcion,
                FechaInicio = actividadCreateDto.FechaInicio,
                FechaFin = actividadCreateDto.FechaFin,
                Creditos = actividadCreateDto.Creditos,
                Capacidad = actividadCreateDto.Capacidad,
                Dias = actividadCreateDto.Dias,
                HoraInicio = actividadCreateDto.HoraInicio,
                HoraFin = actividadCreateDto.HoraFin,
                TipoActividad = actividadCreateDto.TipoActividad,
                EstadoActividad = actividadCreateDto.EstadoActividad,
                ImagenNombre = actividadCreateDto.ImagenNombre,
                DepartamentoId = actividadCreateDto.DepartamentoId,
                ActividadesCarreras = []
            };

            // Asignar las carreras asociadas a la actividad
            foreach (var carreraId in actividadCreateDto.CarreraIds)
            {
                var carrera = await _carreraRepository.GetByIdAsync(carreraId) ?? throw new Exception($"Carrera con ID {carreraId} no encontrada.");

                actividad.ActividadesCarreras.Add(new ActividadCarrera
                {
                    IdCarrera = carrera.Id,
                    Carrera = carrera
                });
            }

            await _actividadRepository.AddAsync(actividad);
            var actividadCreada = await _actividadRepository.GetByIdAsync(actividad.Id);

            return new ActividadDto
            {
                Id = actividadCreada.Id,
                Nombre = actividadCreada.Nombre,
                Descripcion = actividadCreada.Descripcion,
                FechaInicio = actividadCreada.FechaInicio,
                FechaFin = actividadCreada.FechaFin,
                Creditos = actividadCreada.Creditos,
                Capacidad = actividadCreada.Capacidad,
                Dias = actividadCreada.Dias,
                HoraInicio = actividadCreada.HoraInicio,
                HoraFin = actividadCreada.HoraFin,
                TipoActividad = actividadCreada.TipoActividad,
                EstadoActividad = actividadCreada.EstadoActividad,
                ImagenNombre = actividadCreada.ImagenNombre,
                DepartamentoId = actividadCreada.DepartamentoId,
                DepartamentoNombre = actividadCreada.Departamento?.Nombre,
                CarreraNombres = actividadCreada.ActividadesCarreras?.Select(ac => ac.Carrera?.Nombre).ToList() ?? new List<string>()
            };
        }

        // actualiza actividad
        public async Task<ActividadDto> UpdateAsync(int id, ActividadCreateDto actividadUpdateDto)
        {
            var actividad = await _actividadRepository.GetByIdAsync(id) ?? throw new Exception("Actividad no encontrada.");
            // Actualizar los campos de la actividad
            actividad.Nombre = actividadUpdateDto.Nombre;
            actividad.Descripcion = actividadUpdateDto.Descripcion;
            actividad.FechaInicio = actividadUpdateDto.FechaInicio;
            actividad.FechaFin = actividadUpdateDto.FechaFin;
            actividad.Creditos = actividadUpdateDto.Creditos;
            actividad.Capacidad = actividadUpdateDto.Capacidad;
            actividad.Dias = actividadUpdateDto.Dias;
            actividad.HoraInicio = actividadUpdateDto.HoraInicio;
            actividad.HoraFin = actividadUpdateDto.HoraFin;
            actividad.TipoActividad = actividadUpdateDto.TipoActividad;
            actividad.EstadoActividad = actividadUpdateDto.EstadoActividad;
            actividad.ImagenNombre = actividadUpdateDto.ImagenNombre;
            actividad.DepartamentoId = actividadUpdateDto.DepartamentoId;
            actividad.ActividadesCarreras.Clear(); // Limpiar las carreras asociadas antes de agregar nuevas

            foreach (var carreraId in actividadUpdateDto.CarreraIds)
            {
                var carrera = await _carreraRepository.GetByIdAsync(carreraId) ?? throw new Exception($"Carrera con ID {carreraId} no encontrada.");
                actividad.ActividadesCarreras.Add(new ActividadCarrera
                {
                    IdCarrera = carrera.Id,
                    Carrera = carrera 
                });
            }

            var updatedActividad = await _actividadRepository.UpdateAsync(id, actividad);

            return new ActividadDto
            {
                Id = updatedActividad.Id,
                Nombre = updatedActividad.Nombre,
                Descripcion = updatedActividad.Descripcion,
                FechaInicio = updatedActividad.FechaInicio,
                FechaFin = updatedActividad.FechaFin,
                Creditos = updatedActividad.Creditos,
                Capacidad = updatedActividad.Capacidad,
                Dias = updatedActividad.Dias,
                HoraInicio = updatedActividad.HoraInicio,
                HoraFin = updatedActividad.HoraFin,
                TipoActividad = updatedActividad.TipoActividad,
                EstadoActividad = updatedActividad.EstadoActividad,
                ImagenNombre = updatedActividad.ImagenNombre,
                DepartamentoId = updatedActividad.DepartamentoId,
                DepartamentoNombre = updatedActividad.Departamento?.Nombre,
                CarreraNombres = updatedActividad.ActividadesCarreras?.Select(ac => ac.Carrera?.Nombre).ToList() ?? new List<string>()

            };
        }

        // elimina actividad
        public async Task DeleteAsync(int id)
        {
            var actividad = await _actividadRepository.GetByIdAsync(id);
            if (actividad == null)
            {
                throw new Exception("Actividad no encontrada.");
            }
            await _actividadRepository.DeleteAsync(id);
        }
    }
}
