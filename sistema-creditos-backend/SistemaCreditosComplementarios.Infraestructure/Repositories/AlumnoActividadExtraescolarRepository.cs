using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaCreditosComplementarios.Core.Dtos.ActividadExtraescolarAlumno;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAlumnoActividadExtraescolarRepository;
using SistemaCreditosComplementarios.Core.Models.AlumnoActividadExtraescolares;
using SistemaCreditosComplementarios.Core.Models.Enum;
using SistemaCreditosComplementarios.Infraestructure.Data;

namespace SistemaCreditosComplementarios.Infraestructure.Repositories
{
    public class AlumnoActividadExtraescolarRepository : IAlumnoActividadExtraescolarRepository
    {
        private readonly ApplicationDbContext _context;

        public AlumnoActividadExtraescolarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AlumnoActividadExtraescolar>> GetAllAsync()
        {
            return await _context.AlumnoActividadeExtraescolar
                .Include(aa => aa.Alumno)
                .Include(aa => aa.ActividadExtraescolar)
                .ToListAsync();
        }

        public async Task<AlumnoActividadExtraescolar> GetByIdAsync(int alumnoId, int actividadExtraescolarId)
        {
            return await _context.AlumnoActividadeExtraescolar
                .Include(aa => aa.Alumno)
                .Include(aa => aa.ActividadExtraescolar)
                .FirstOrDefaultAsync(aa => aa.IdAlumno == alumnoId && aa.IdActividadExtraescolar == actividadExtraescolarId);
        }

        public async Task<IEnumerable<AlumnoInscritoExtraescolarDto>> GetAlumnosInscritosPorActividadAsync(int actividadExtraescolarId)
        {
            var inscripciones = await _context.AlumnoActividadeExtraescolar
                .Include(aa => aa.Alumno)
                .Include(aa => aa.ActividadExtraescolar)
                .Where(aa => aa.IdActividadExtraescolar == actividadExtraescolarId)
                .ToListAsync();

            return inscripciones.Select(aa => new AlumnoInscritoExtraescolarDto
            {
                AlumnoId = aa.IdAlumno,
                NombreCompleto = aa.Alumno.Nombre + " " + aa.Alumno.Apellido,
                EstadoAlumnoActividad = aa.EstadoAlumnoActividad,
                CreditosObtenidos = aa.ActividadExtraescolar.Creditos,
                FechaInscripcion = aa.FechaInscripcion
            });
        }

        public async Task<IEnumerable<ExtraescolarAlumnoDto>> GetExtraescolarPorAlumnoAsync(int alumnoId, EstadoAlumnoActividad? estado = null)
        {
            var query = _context.AlumnoActividadeExtraescolar
                .Include(aa => aa.ActividadExtraescolar)
                .Where(aa => aa.IdAlumno == alumnoId);

            if (estado.HasValue)
            {
                query = query.Where(aa => aa.EstadoAlumnoActividad == estado.Value);
            }

            var extraescolar = await query.ToListAsync();

            return extraescolar.Select(aa => new ExtraescolarAlumnoDto
            {
                ActividadId = aa.IdActividadExtraescolar,
                Nombre = aa.ActividadExtraescolar.Nombre,
                Descripcion = aa.ActividadExtraescolar.Descripcion,
                Creditos = aa.ActividadExtraescolar.Creditos,
                FechaInicio = aa.ActividadExtraescolar.FechaInicio,
                FechaFin = aa.ActividadExtraescolar.FechaFin,
                EstadoAlumnoActividad = aa.EstadoAlumnoActividad,
                ImagenNombre = aa.ActividadExtraescolar.ImagenNombre
            });
        }

        public async Task<bool> AlumnoExisteAsync(int alumnoId)
        {
            return await _context.Alumnos.AnyAsync(a => a.Id == alumnoId);
        }

        public async Task<bool> ActividadExtraescolarExisteAsync(int actividadExtraescolarId)
        {
            return await _context.ActividadeExtraescolar.AnyAsync(a => a.Id == actividadExtraescolarId);
        }


        public async Task AddAsync(AlumnoActividadExtraescolar alumnoActividadExtraescolar)
        {
            _context.AlumnoActividadeExtraescolar.Add(alumnoActividadExtraescolar);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int alumnoId, int actividadExtraescolarId, AlumnoActividadExtraescolar alumnoActividadExtraescolar)
        {
            var existing = await GetByIdAsync(alumnoId, actividadExtraescolarId);
            if (existing == null) throw new KeyNotFoundException("Inscripción no encontrada.");

            existing.EstadoAlumnoActividad = alumnoActividadExtraescolar.EstadoAlumnoActividad;
            existing.FechaInscripcion = alumnoActividadExtraescolar.FechaInscripcion;

            _context.AlumnoActividadeExtraescolar.Update(existing);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int alumnoId, int actividadExtraescolarId)
        {
            var existing = await GetByIdAsync(alumnoId, actividadExtraescolarId);
            if (existing == null) throw new KeyNotFoundException("Inscripción no encontrada.");
            _context.AlumnoActividadeExtraescolar.Remove(existing);
            await _context.SaveChangesAsync();

        }
    }
}
