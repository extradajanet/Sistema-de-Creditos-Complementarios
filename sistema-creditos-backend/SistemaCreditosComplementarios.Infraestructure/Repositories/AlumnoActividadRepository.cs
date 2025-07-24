using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAlumnoActividadRepository;
using SistemaCreditosComplementarios.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SistemaCreditosComplementarios.Infraestructure.Data;
using SistemaCreditosComplementarios.Core.Models.ActividadModel;
using SistemaCreditosComplementarios.Core.Models.AlumnosActividades;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Dtos.AlumnoActividad;
using Microsoft.EntityFrameworkCore;
using SistemaCreditosComplementarios.Core.Models.Enum;

namespace SistemaCreditosComplementarios.Infraestructure.Repositories
{
    public class AlumnoActividadRepository : IAlumnoActividadRepository
    {
        private readonly ApplicationDbContext _context;

        public AlumnoActividadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AlumnoActividad>> GetAllAsync()
        {
            return await _context.AlumnosActividades
                .Include(aa => aa.Alumno)
                .Include(aa => aa.Actividad)
                .ToListAsync();
        }

        public async Task<AlumnoActividad> GetByIdAsync(int alumnoId, int actividadId)
        {
            return await _context.AlumnosActividades
                .Include(aa => aa.Alumno)
                .Include(aa => aa.Actividad)
                .FirstOrDefaultAsync(aa => aa.IdAlumno == alumnoId && aa.IdActividad == actividadId);
        }

        public async Task<IEnumerable<AlumnoActividad>> GetAlumnosInscritosPorActividadAsync(int actividadId)
{
    return await _context.AlumnosActividades
        .Include(aa => aa.Alumno)
            .ThenInclude(al => al.Carrera)
        .Include(aa => aa.Actividad)
        .Where(aa => aa.IdActividad == actividadId)
        .ToListAsync();
}


        public async Task<IEnumerable<AlumnoActividad>> GetCursosPorAlumnoAsync(int alumnoId, EstadoAlumnoActividad? estado = null, EstadoActividad? estadoAct = null)
        {
            var query = _context.AlumnosActividades
                .Include(aa => aa.Actividad)
                .Where(aa => aa.IdAlumno == alumnoId);

            if (estado.HasValue && estadoAct.HasValue)
            {
                query = query.Where(aa => aa.EstadoAlumnoActividad == estado.Value);
                query = query.Where(aa => aa.Actividad.EstadoActividad == estadoAct.Value);
            }

            return await query.ToListAsync();
        }


        public async Task<bool> AlumnoExisteAsync(int alumnoId)
        {
            return await _context.Alumnos.AnyAsync(a => a.Id == alumnoId);
        }

        public async Task<bool> ActividadExisteAsync(int actividadId)
        {
            return await _context.Actividades.AnyAsync(a => a.Id == actividadId);
        }


        public async Task AddAsync(AlumnoActividad alumnoActividad)
        {
            _context.AlumnosActividades.Add(alumnoActividad);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int alumnoId, int actividadId, EstadoAlumnoActividad estadoAlumnoActividad)
        {
            var existing = await GetByIdAsync(alumnoId, actividadId);
            if (existing == null) throw new KeyNotFoundException("Inscripción no encontrada.");

            existing.EstadoAlumnoActividad = estadoAlumnoActividad;

            _context.AlumnosActividades.Update(existing);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int alumnoId, int actividadId)
        {
            var existing = await GetByIdAsync(alumnoId, actividadId);
            if (existing == null) throw new KeyNotFoundException("Inscripción no encontrada.");
            _context.AlumnosActividades.Remove(existing);
            await _context.SaveChangesAsync();

        }
    }
}
