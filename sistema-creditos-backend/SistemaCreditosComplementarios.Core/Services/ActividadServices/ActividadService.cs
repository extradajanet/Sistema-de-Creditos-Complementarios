using SistemaCreditosComplementarios.Core.Dtos.Actividad;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.ActividadRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IActividadService;
using SistemaCreditosComplementarios.Core.Models.ActividadModel;
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
        public ActividadService(IActividadRepository actividadRepository)
        {
            _actividadRepository = actividadRepository;
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
                TipoActividad = a.TipoActividad,
                EstadoActividad = a.EstadoActividad,
                CarreraId = a.CarreraId,
                CarreraNombre = a.Carrera?.Nombre,
                ImagenNombre = a.ImagenNombre
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
                TipoActividad = actividad.TipoActividad,
                EstadoActividad = actividad.EstadoActividad,
                CarreraId = actividad.CarreraId,
                CarreraNombre = actividad.Carrera?.Nombre,
                ImagenNombre = actividad.ImagenNombre
            };
        }

        // agrega actividad
        public async Task<ActividadDto> AddAsync(ActividadCreateDto actividadCreateDto)
        {
            var actividad = new ActividadModels
            {
                Creditos = actividadCreateDto.Creditos,
                Descripcion = actividadCreateDto.Descripcion,
                FechaInicio = actividadCreateDto.FechaInicio,
                FechaFin = actividadCreateDto.FechaFin,
                Nombre = actividadCreateDto.Nombre,
                TipoActividad = actividadCreateDto.TipoActividad,
                CarreraId = actividadCreateDto.CarreraId,
                EstadoActividad = actividadCreateDto.EstadoActividad,
                ImagenNombre = actividadCreateDto.ImagenNombre

            };
            await _actividadRepository.AddAsync(actividad);

            var actividadConCarrera = await _actividadRepository.GetByIdAsync(actividad.Id);

            return new ActividadDto
            {
                Id = actividadConCarrera.Id,
                Nombre = actividadConCarrera.Nombre,
                Descripcion = actividadConCarrera.Descripcion,
                FechaInicio = actividadConCarrera.FechaInicio,
                FechaFin = actividadConCarrera.FechaFin,
                Creditos = actividadConCarrera.Creditos,
                TipoActividad = actividadConCarrera.TipoActividad,
                EstadoActividad = actividadConCarrera.EstadoActividad,
                CarreraId = actividadConCarrera.CarreraId,
                CarreraNombre = actividadConCarrera.Carrera?.Nombre,
                ImagenNombre = actividadConCarrera.ImagenNombre
            };
        }

        // actualiza actividad
        public async Task<ActividadDto> UpdateAsync(int id, ActividadCreateDto actividadUpdateDto)
        {
            var actividad = await _actividadRepository.GetByIdAsync(id);
            if (actividad == null)
            {
                throw new Exception("Actividad no encontrada.");
            }
            // Actualizar los campos de la actividad
            actividad.Nombre = actividadUpdateDto.Nombre;
            actividad.Descripcion = actividadUpdateDto.Descripcion;
            actividad.FechaInicio = actividadUpdateDto.FechaInicio;
            actividad.FechaFin = actividadUpdateDto.FechaFin;
            actividad.Creditos = actividadUpdateDto.Creditos;
            actividad.TipoActividad = actividadUpdateDto.TipoActividad;
            actividad.EstadoActividad = actividadUpdateDto.EstadoActividad;
            actividad.CarreraId = actividadUpdateDto.CarreraId;
            actividad.ImagenNombre = actividadUpdateDto.ImagenNombre;

            var updatedActividad = await _actividadRepository.UpdateAsync(id, actividad);

            return new ActividadDto
            {
                Id = updatedActividad.Id,
                Nombre = updatedActividad.Nombre,
                Descripcion = updatedActividad.Descripcion,
                FechaInicio = updatedActividad.FechaInicio,
                FechaFin = updatedActividad.FechaFin,
                Creditos = updatedActividad.Creditos,
                TipoActividad = updatedActividad.TipoActividad,
                EstadoActividad = updatedActividad.EstadoActividad,
                CarreraId = updatedActividad.CarreraId,
                CarreraNombre = updatedActividad.Carrera?.Nombre,
                ImagenNombre = updatedActividad.ImagenNombre
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
