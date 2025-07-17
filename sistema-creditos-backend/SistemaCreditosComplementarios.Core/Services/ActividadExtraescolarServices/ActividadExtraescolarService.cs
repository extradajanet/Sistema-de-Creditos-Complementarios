using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Dtos.Actividad;
using SistemaCreditosComplementarios.Core.Dtos.ActividadesExtraescolares;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IActividadExtraescolarRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IActividadExtraescolarService;
using SistemaCreditosComplementarios.Core.Models.ActividadExtraescolarModel;
using SistemaCreditosComplementarios.Core.Models.ActividadModel;

namespace SistemaCreditosComplementarios.Core.Services.ActividadExtraescolarServices
{
    public class ActividadExtraescolarService : IActividadExtraescolarService
    {
        private readonly IActividadExtraescolarRepository _actividadExtraescolarRepository;

        public ActividadExtraescolarService(IActividadExtraescolarRepository _ActividadExtraescolarRepository)
        {
            _actividadExtraescolarRepository = _ActividadExtraescolarRepository;

        }

        public async Task<IEnumerable<ActividadExtraescolarDto>> GetAllAsync()
        {
            var actividadesextraescolares = await _actividadExtraescolarRepository.GetAllAsync();
            return actividadesextraescolares.Select(a => new ActividadExtraescolarDto
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
                DepartamentoNombre = a.Departamento?.Nombre

            });    
        }

        public async Task<ActividadExtraescolarDto> GetByIdAsync(int id)
        {
            var actividadesextraescolares = await _actividadExtraescolarRepository.GetByIdAsync(id);
            if (actividadesextraescolares == null)
            {
                throw new Exception("Actividad extraescolar no encontrada ");
            }
            return new ActividadExtraescolarDto
            {
                Id = actividadesextraescolares.Id,
                Nombre = actividadesextraescolares.Nombre,
                Descripcion = actividadesextraescolares.Descripcion,
                FechaInicio = actividadesextraescolares.FechaInicio,
                FechaFin = actividadesextraescolares.FechaFin,
                Creditos = actividadesextraescolares.Creditos,
                Capacidad = actividadesextraescolares.Capacidad,
                Dias = actividadesextraescolares.Dias,
                HoraInicio = actividadesextraescolares.HoraInicio,
                HoraFin = actividadesextraescolares.HoraFin,
                TipoActividad = actividadesextraescolares.TipoActividad,
                EstadoActividad = actividadesextraescolares.EstadoActividad,
                ImagenNombre = actividadesextraescolares.ImagenNombre,
                DepartamentoId = actividadesextraescolares.DepartamentoId,
                DepartamentoNombre = actividadesextraescolares.Departamento?.Nombre,
            };
        }

        public async Task<ActividadExtraescolarDto> AddAsync(ActividadExtraescolarCreateDto actividadExtraescolarCreateDto)
        {

            var actividadesextraescolares = new ActividadExtraescolar
            {
                Nombre = actividadExtraescolarCreateDto.Nombre,
                Descripcion = actividadExtraescolarCreateDto.Descripcion,
                FechaInicio = actividadExtraescolarCreateDto.FechaInicio,
                FechaFin = actividadExtraescolarCreateDto.FechaFin,
                Creditos = actividadExtraescolarCreateDto.Creditos,
                Capacidad = actividadExtraescolarCreateDto.Capacidad,
                Dias = actividadExtraescolarCreateDto.Dias,
                HoraInicio = actividadExtraescolarCreateDto.HoraInicio,
                HoraFin = actividadExtraescolarCreateDto.HoraFin,
                TipoActividad = actividadExtraescolarCreateDto.TipoActividad,
                EstadoActividad = actividadExtraescolarCreateDto.EstadoActividad,
                ImagenNombre = actividadExtraescolarCreateDto.ImagenNombre,
                DepartamentoId = actividadExtraescolarCreateDto.DepartamentoId
            };

            await _actividadExtraescolarRepository.AddAsync(actividadesextraescolares);
            var actividadExtraescolarCreada = await _actividadExtraescolarRepository.GetByIdAsync(actividadesextraescolares.Id);

            return new ActividadExtraescolarDto
            {
                Id = actividadExtraescolarCreada.Id,
                Nombre = actividadExtraescolarCreada.Nombre,
                Descripcion = actividadExtraescolarCreada.Descripcion,
                FechaInicio = actividadExtraescolarCreada.FechaInicio,
                FechaFin = actividadExtraescolarCreada.FechaFin,
                Creditos = actividadExtraescolarCreada.Creditos,
                Capacidad = actividadExtraescolarCreada.Capacidad,
                Dias = actividadExtraescolarCreada.Dias,
                HoraInicio = actividadExtraescolarCreada.HoraInicio,
                HoraFin = actividadExtraescolarCreada.HoraFin,
                TipoActividad = actividadExtraescolarCreada.TipoActividad,
                EstadoActividad = actividadExtraescolarCreada.EstadoActividad,
                ImagenNombre = actividadExtraescolarCreada.ImagenNombre,
                DepartamentoId = actividadExtraescolarCreada.DepartamentoId,
                DepartamentoNombre = actividadExtraescolarCreada.Departamento?.Nombre,
            };
        }

        public async Task<ActividadExtraescolarDto> UpdateAsync(int id, ActividadExtraescolarCreateDto actividadExtraescolarUpdateDto)
        {
            var actividadextraescolar = await _actividadExtraescolarRepository.GetByIdAsync(id) ?? throw new Exception("Actividad extraescolar no encontrada");

            actividadextraescolar.Nombre = actividadExtraescolarUpdateDto.Nombre;
            actividadextraescolar.Descripcion = actividadExtraescolarUpdateDto.Descripcion;
            actividadextraescolar.FechaInicio = actividadExtraescolarUpdateDto.FechaInicio;
            actividadextraescolar.FechaFin = actividadExtraescolarUpdateDto.FechaFin;
            actividadextraescolar.Creditos = actividadExtraescolarUpdateDto.Creditos;
            actividadextraescolar.Capacidad = actividadExtraescolarUpdateDto.Capacidad;
            actividadextraescolar.Dias = actividadExtraescolarUpdateDto.Dias;
            actividadextraescolar.HoraInicio = actividadExtraescolarUpdateDto.HoraInicio;
            actividadextraescolar.HoraFin = actividadExtraescolarUpdateDto.HoraFin;
            actividadextraescolar.TipoActividad = actividadExtraescolarUpdateDto.TipoActividad;
            actividadextraescolar.EstadoActividad = actividadExtraescolarUpdateDto.EstadoActividad;
            actividadextraescolar.ImagenNombre = actividadExtraescolarUpdateDto.ImagenNombre;
            actividadextraescolar.DepartamentoId = actividadExtraescolarUpdateDto.DepartamentoId;

            var updateActividadExtraescolar = await _actividadExtraescolarRepository.UpdateAsync(id, actividadextraescolar);

            return new ActividadExtraescolarDto
            {
                Id = updateActividadExtraescolar.Id,
                Nombre = updateActividadExtraescolar.Nombre,
                Descripcion = updateActividadExtraescolar.Descripcion,
                FechaInicio = updateActividadExtraescolar.FechaInicio,
                FechaFin = updateActividadExtraescolar.FechaFin,
                Creditos = updateActividadExtraescolar.Creditos,
                Capacidad = updateActividadExtraescolar.Capacidad,
                Dias = updateActividadExtraescolar.Dias,
                HoraInicio = updateActividadExtraescolar.HoraInicio,
                HoraFin = updateActividadExtraescolar.HoraFin,
                TipoActividad = updateActividadExtraescolar.TipoActividad,
                EstadoActividad = updateActividadExtraescolar.EstadoActividad,
                ImagenNombre = updateActividadExtraescolar.ImagenNombre,
                DepartamentoId = updateActividadExtraescolar.DepartamentoId,
                DepartamentoNombre = updateActividadExtraescolar.Departamento?.Nombre
            };
        }

        public async Task DeleteAsync(int id)
        {
            var actividadextraescolar = await _actividadExtraescolarRepository.GetByIdAsync(id);
            if (actividadextraescolar == null)
            {
                throw new Exception("Actividad extraescolar no encontrada");
            }
            await _actividadExtraescolarRepository.DeleteAsync(id);
        }





    }
}
