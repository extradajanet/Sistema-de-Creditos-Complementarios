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

        public async Task<IEnumerable<AlumnoInscritoDto>> GetAlumnosInscritosPorActividadAsync(int actividadId)
        {
            var inscripciones = await _context.AlumnosActividades
                .Include(aa => aa.Alumno)
                    .ThenInclude(al => al.Carrera)
                .Include(aa => aa.Actividad)
                .Where(aa => aa.IdActividad == actividadId)
                .ToListAsync();

            return inscripciones.Select(aa => new AlumnoInscritoDto
            {
                AlumnoId = aa.IdAlumno,
                NombreCompleto = aa.Alumno.Nombre + " " + aa.Alumno.Apellido,
                CarreraNombre = aa.Alumno.Carrera.Nombre,
                EstadoActividad = aa.EstadoActividad,
                CreditosObtenidos = aa.Actividad.Creditos,
                FechaInscripcion = aa.FechaInscripcion
            });
        }

        public async Task<IEnumerable<CursoAlumnoDto>> GetCursosPorAlumnoAsync(int alumnoId)
        {
            var cursos = await _context.AlumnosActividades
                .Include(aa => aa.Actividad)
                .Where(aa => aa.IdAlumno == alumnoId)
                .ToListAsync();
            return cursos.Select(aa => new CursoAlumnoDto
            {
                ActividadId = aa.IdActividad,
                Nombre = aa.Actividad.Nombre,
                Descripcion = aa.Actividad.Descripcion,
                Creditos = aa.Actividad.Creditos,
                FechaInicio = aa.Actividad.FechaInicio,
                FechaFin = aa.Actividad.FechaFin,
                EstadoActividad = aa.EstadoActividad,
                ImagenNombre = aa.Actividad.ImagenNombre
            });
        }

        public async Task AddAsync(AlumnoActividad alumnoActividad)
        {
            _context.AlumnosActividades.Add(alumnoActividad);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int alumnoId, int actividadId, AlumnoActividad alumnoActividad)
        {
            var existing = await GetByIdAsync(alumnoId, actividadId);
            if (existing == null) throw new KeyNotFoundException("Inscripción no encontrada.");

            existing.EstadoActividad = alumnoActividad.EstadoActividad;
            existing.FechaInscripcion = alumnoActividad.FechaInscripcion;

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
