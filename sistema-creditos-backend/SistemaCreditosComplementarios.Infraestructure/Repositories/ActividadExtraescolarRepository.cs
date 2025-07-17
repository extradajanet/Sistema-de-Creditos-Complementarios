using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IActividadExtraescolarRepository;
using SistemaCreditosComplementarios.Core.Models.ActividadExtraescolarModel;
using SistemaCreditosComplementarios.Infraestructure.Data;

namespace SistemaCreditosComplementarios.Infraestructure.Repositories
{
    public class ActividadExtraescolarRepository : IActividadExtraescolarRepository
    {

        private readonly ApplicationDbContext _context; 

        public ActividadExtraescolarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ActividadExtraescolar>> GetAllAsync()
        {
            return await _context.ActividadeExtraescolar
                .Include(a => a.Departamento)
                .ToListAsync();

        }

        public async Task<ActividadExtraescolar> GetByIdAsync(int id)
        {
            return await _context.ActividadeExtraescolar
                .Include(a => a.Departamento)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(ActividadExtraescolar actividadExtraescolar)
        {
            _context.ActividadeExtraescolar.Add(actividadExtraescolar);
            await _context.SaveChangesAsync();
        }

        public async Task<ActividadExtraescolar> UpdateAsync(int id, ActividadExtraescolar actividadExtraescolarUpdate)
        {
            var actividadExtraescolar = await GetByIdAsync(id);
            if (actividadExtraescolar == null)
            {
                throw new Exception("Actividad extraescolar no encontrada.");
            }
            actividadExtraescolar.Nombre = actividadExtraescolarUpdate.Nombre;
            actividadExtraescolar.Descripcion = actividadExtraescolarUpdate.Descripcion;
            actividadExtraescolar.FechaInicio = actividadExtraescolarUpdate.FechaInicio;
            actividadExtraescolar.FechaFin = actividadExtraescolarUpdate.FechaFin;
            actividadExtraescolar.Creditos = actividadExtraescolarUpdate.Creditos;
            actividadExtraescolar.Capacidad = actividadExtraescolarUpdate.Capacidad;
            actividadExtraescolar.Dias = actividadExtraescolarUpdate.Dias;
            actividadExtraescolar.HoraInicio = actividadExtraescolarUpdate.HoraInicio;
            actividadExtraescolar.HoraFin = actividadExtraescolarUpdate.HoraFin;
            actividadExtraescolar.TipoActividad = actividadExtraescolarUpdate.TipoActividad;
            actividadExtraescolar.EstadoActividad = actividadExtraescolarUpdate.EstadoActividad;
            actividadExtraescolar.ImagenNombre = actividadExtraescolarUpdate.ImagenNombre;
            actividadExtraescolar.DepartamentoId = actividadExtraescolarUpdate.DepartamentoId;

            _context.ActividadeExtraescolar.Update(actividadExtraescolar);
            await _context.SaveChangesAsync();
            return actividadExtraescolar; 

        }

        public async Task DeleteAsync(int id)
        {
            var actividadExtraescolar = await GetByIdAsync(id);
            if (actividadExtraescolar == null)
            {
                throw new Exception("Actividad extraescolar no encontrada.");
            }
            _context.ActividadeExtraescolar.Remove(actividadExtraescolar);
            await _context.SaveChangesAsync();
        }
    }
}
