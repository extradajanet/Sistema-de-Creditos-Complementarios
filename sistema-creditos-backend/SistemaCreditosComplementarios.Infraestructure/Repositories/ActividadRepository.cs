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
        public async Task<IEnumerable<ActividadModels>> GetAllAsync()
        {
            //incluye la relación para obtener el nombre los datos de la carrera
            return await _context.Actividades
                .Include(a => a.Carrera)
                .ToListAsync();

        }

        // obtiene por id
        public async Task<ActividadModels> GetByIdAsync(int id)
        {
            return await _context.Actividades
                .Include(a => a.Carrera)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // añade actividad
        public async Task AddAsync(ActividadModels actividad)
        {
            _context.Actividades.Add(actividad);
            await _context.SaveChangesAsync();
        }

        // actualiza una actividad
        public async Task<ActividadModels> UpdateAsync(int id, ActividadModels actividadUpdate)
        {
            var actividad = await GetByIdAsync(id);
            if (actividad == null)
            {
                throw new Exception("Actividad no encontrada.");
            }
            // se actualiza
            actividad.Nombre = actividadUpdate.Nombre;
            actividad.Descripcion = actividadUpdate.Descripcion;
            actividad.FechaInicio = actividadUpdate.FechaInicio;
            actividad.FechaFin = actividadUpdate.FechaFin;
            actividad.Creditos = actividadUpdate.Creditos;
            actividad.TipoActividad = actividadUpdate.TipoActividad;
            actividad.CarreraId = actividadUpdate.CarreraId;
            actividad.EstadoActividad = actividadUpdate.EstadoActividad;
            actividad.ImagenNombre = actividadUpdate.ImagenNombre;
            _context.Actividades.Update(actividad);
            await _context.SaveChangesAsync();
            return actividad;
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
