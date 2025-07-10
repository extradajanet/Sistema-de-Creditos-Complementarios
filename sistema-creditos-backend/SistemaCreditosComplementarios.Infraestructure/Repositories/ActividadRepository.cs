using Microsoft.EntityFrameworkCore;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.ActividadRepository;
using SistemaCreditosComplementarios.Core.Models.ActividadModel;
using SistemaCreditosComplementarios.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Infraestructure.Repositories
{
    // usa la interfaz IActividadRepository para implementar los métodos de acceso a datos relacionados con las actividades con el modelo de actividad
    //es la capa de datos
    public class ActividadRepository: IActividadRepository
    {
        private readonly ApplicationDbContext _context; // Asumiendo que tienes un DbContext configurado para tu base de datos

        //Constructor que recibe el contexto de la base de datos
        public ActividadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // obtiene todas las actividades
        public async Task<IEnumerable<Actividad>> GetAllAsync()
        {
            //incluye la relación para obtener el nombre los datos de la carrera
            return await _context.Actividades
                .Include(a => a.Departamento)
                .Include(a => a.ActividadesCarreras) // Incluye la relación con ActividadCarreras
                .ThenInclude(c => c.Carrera) // Incluye la relación con Carrera dentro de ActividadCarreras
                .ToListAsync();

        }

        // obtiene por id
        public async Task<Actividad> GetByIdAsync(int id)
        {
            return await _context.Actividades
                .Include(a => a.Departamento)
                .Include(ac => ac.ActividadesCarreras) // Incluye la relación con ActividadCarreras
                .ThenInclude(c => c.Carrera) // Incluye la relación con Carrera dentro de ActividadCarreras
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // añade actividad
        public async Task AddAsync(Actividad actividad)
        {
            _context.Actividades.Add(actividad);
            await _context.SaveChangesAsync();
        }

        // actualiza una actividad
        public async Task<Actividad> UpdateAsync(int id, Actividad actividadUpdate)
        {
            var actividad = await GetByIdAsync(id);
            if (actividad == null)
            {
                throw new Exception("Actividad no encontrada.");
            }
            // Actualiza los campos de la actividad
            actividad.Nombre = actividadUpdate.Nombre;
            actividad.Descripcion = actividadUpdate.Descripcion;
            actividad.FechaInicio = actividadUpdate.FechaInicio;
            actividad.FechaFin = actividadUpdate.FechaFin;
            actividad.Creditos = actividadUpdate.Creditos;
            actividad.Capacidad = actividadUpdate.Capacidad;
            actividad.Dias = actividadUpdate.Dias;
            actividad.HoraInicio = actividadUpdate.HoraInicio;
            actividad.HoraFin = actividadUpdate.HoraFin;
            actividad.TipoActividad = actividadUpdate.TipoActividad;
            actividad.EstadoActividad = actividadUpdate.EstadoActividad;
            actividad.ImagenNombre = actividadUpdate.ImagenNombre;
            actividad.DepartamentoId = actividadUpdate.DepartamentoId;

            _context.Actividades.Update(actividad);
            await _context.SaveChangesAsync();
            return actividad; // Devuelve la entidad actualizada

        }

        // elimina una actividad
        public async Task DeleteAsync(int id)
        {
            var actividad = await GetByIdAsync(id);
            if (actividad == null)
            {
                throw new Exception("Actividad no encontrada.");
            }
            _context.Actividades.Remove(actividad);
            await _context.SaveChangesAsync();
        }

    }
}
