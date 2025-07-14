using Microsoft.EntityFrameworkCore;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAlumnoRepository;
using SistemaCreditosComplementarios.Core.Models.Alumnos;
using SistemaCreditosComplementarios.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Infraestructure.Repositories
{
    public class AlumnoRepository : IAlumnoRepository 
    {
        private readonly ApplicationDbContext _context;

        public AlumnoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Alumno>> GetAllAsync()
        {
            return await _context.Alumnos
                .Include(a => a.Usuario)
                .ToListAsync(); // Obtiene todos los alumnos de la base de datos
        }

        public async Task<Alumno> GetByIdAsync(int id)
        {
            return await _context.Alumnos
                .Include(a => a.Usuario) // Incluye la entidad Usuario relacionada
                .Include(a=> a.Carrera)
                .FirstOrDefaultAsync(a => a.Id == id); // Busca el alumno por su ID
        }

        public async Task<Alumno> GetByUserIdAsync(string userId)
        {
            return await _context.Alumnos
                .Include(a => a.Usuario) // Incluye la entidad Usuario relacionada
                .Include(a => a.Carrera)
                .FirstOrDefaultAsync(a => a.Usuario.Id == userId); // Busca el alumno por su UserId
        }

        public async Task AddAsync(Alumno alumno)
        {
            _context.Alumnos.Add(alumno); 
            await _context.SaveChangesAsync(); 
        }

        public async Task<Alumno> UpdateAsync(Alumno alumno)
        {
            _context.Alumnos.Update(alumno); 
            await _context.SaveChangesAsync();
            return alumno; 

        }

        public async Task DeleteAsync(int id)
        {
            var alumno = await GetByIdAsync(id); // Busca el alumno por su ID
            if (alumno != null)
            {
                _context.Alumnos.Remove(alumno);
                await _context.SaveChangesAsync(); 
            }
            else
            {
                throw new Exception("Alumno no encontrado."); // Lanza una excepción si el alumno no existe
            }
        }
    }
}
