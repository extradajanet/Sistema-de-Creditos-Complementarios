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

        // Constructor que recibe el repositorio de actividades
        public ActividadService(IActividadRepository actividadRepository)
        {
            _actividadRepository = actividadRepository;
        }

        // Método para obtener todas las actividades
        public async Task<IEnumerable<ActividadDto>> GetAllAsync()
        {
            var actividades = await _actividadRepository.GetAllAsync(); // Llamada al repositorio para obtener todas las actividades
            return actividades.Select(a => new ActividadDto
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Descripcion = a.Descripcion,
                FechaInicio = a.FechaInicio,
                FechaFin = a.FechaFin,
                Creditos = a.Creditos,
                TipoActividad = a.TipoActividad
            });
        }

        // Método para obtener una actividad por su ID
        public async Task<ActividadDto> GetByIdAsync(int id)
        {
            var actividad = await _actividadRepository.GetByIdAsync(id); // Llamada al repositorio para obtener la actividad por ID
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
                TipoActividad = actividad.TipoActividad
            };
        }

        // Método para agregar una nueva actividad
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
                EstadoActividad = actividadCreateDto.EstadoActividad, 
                ImagenNombre = actividadCreateDto.ImagenNombre

            };
            await _actividadRepository.AddAsync(actividad); // Llamada al repositorio para agregar la actividad
            
            return new ActividadDto
            {
                Id = actividad.Id, // Asignar el ID generado por la base de datos
                Nombre = actividad.Nombre,
                Descripcion = actividad.Descripcion,
                FechaInicio = actividad.FechaInicio,
                FechaFin = actividad.FechaFin,
                Creditos = actividad.Creditos,
                TipoActividad = actividad.TipoActividad,
                EstadoActividad = actividad.EstadoActividad, 
                ImagenNombre = actividad.ImagenNombre
            };
        }

        // Método para actualizar una actividad existente
        public async Task<ActividadDto> UpdateAsync(int id, ActividadCreateDto actividadUpdateDto)
        {
            var actividad = await _actividadRepository.GetByIdAsync(id); // Llamada al repositorio para obtener la actividad por ID
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
            actividad.ImagenNombre = actividadUpdateDto.ImagenNombre;
            var updatedActividad = await _actividadRepository.UpdateAsync(id, actividad); // Llamada al repositorio para actualizar la actividad
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
                ImagenNombre = updatedActividad.ImagenNombre
            };
        }

        // Método para eliminar una actividad por su ID
        public async Task DeleteAsync(int id)
        {
            var actividad = await _actividadRepository.GetByIdAsync(id); // Llamada al repositorio para obtener la actividad por ID
            if (actividad == null)
            {
                throw new Exception("Actividad no encontrada.");
            }
            await _actividadRepository.DeleteAsync(id); // Llamada al repositorio para eliminar la actividad
        }   

    }
}
