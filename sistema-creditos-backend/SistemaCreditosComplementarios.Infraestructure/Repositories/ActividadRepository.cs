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
    public class ActividadRepository : IActividadRepository
    {
        private readonly ApplicationDbContext _context; // Asumiendo que tienes un DbContext configurado para tu base de datos

        //Constructor que recibe el contexto de la base de datos
        public ActividadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para obtener todas las actividades
        public async Task<IEnumerable<ActividadModels>> GetAllAsync()
        {
            return await _context.Actividades.ToListAsync(); // se usa ToListAsync() para obtener todas las actividades de la base de datos
        }

        // Método para obtener una actividad por su ID
        public async Task<ActividadModels> GetByIdAsync(int id)
        {
            return await _context.Actividades.FindAsync(id); // se usa FindAsync() para buscar la actividad por su ID
        }

        // Método para agregar una nueva actividad
        public async Task AddAsync(ActividadModels actividad)
        {
            _context.Actividades.Add(actividad); // se agrega la actividad al contexto
            await _context.SaveChangesAsync(); // se guardan los cambios en la base de datos
        }

        // Método para actualizar una actividad existente
        public async Task<ActividadModels> UpdateAsync(int id, ActividadModels actividadUpdate)
        {
            var actividad = await GetByIdAsync(id); // se obtiene la actividad por su ID
            if (actividad == null)
            {
                throw new Exception("Actividad no encontrada."); // si no se encuentra la actividad, se lanza una excepción
            }
            // Actualizar los campos de la actividad
            actividad.Nombre = actividadUpdate.Nombre;
            actividad.Descripcion = actividadUpdate.Descripcion;
            actividad.FechaInicio = actividadUpdate.FechaInicio;
            actividad.FechaFin = actividadUpdate.FechaFin;
            actividad.Creditos = actividadUpdate.Creditos;
            actividad.TipoActividad = actividadUpdate.TipoActividad;
            _context.Actividades.Update(actividad); // se actualiza la actividad en el contexto
            await _context.SaveChangesAsync(); // se guardan los cambios en la base de datos
            return actividad; // se devuelve la actividad actualizada
        }

        // Método para eliminar una actividad por su ID
        public async Task DeleteAsync(int id)
        {
            var actividad = await GetByIdAsync(id); // se obtiene la actividad por su ID
            if (actividad == null)
            {
                throw new Exception("Actividad no encontrada."); // si no se encuentra la actividad, se lanza una excepción
            }
            _context.Actividades.Remove(actividad); // se elimina la actividad del contexto
            await _context.SaveChangesAsync(); // se guardan los cambios en la base de datos
        }

    }
}
