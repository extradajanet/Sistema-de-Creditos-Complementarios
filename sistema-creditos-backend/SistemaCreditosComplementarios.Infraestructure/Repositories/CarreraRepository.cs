using Microsoft.EntityFrameworkCore;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.ICarreraRepository;
using SistemaCreditosComplementarios.Core.Models.CarreraModel;
using SistemaCreditosComplementarios.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Infraestructure.Repositories
{
    //Implementa la interfaz para acceder a los datos del modelo 
    public class CarreraRepository : ICarreraRepository
    {
        private readonly ApplicationDbContext _context;
        // contexto db
        public CarreraRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        // Método para obtener todas las carreras
        public async Task<IEnumerable<Carrera>> GetAll()
        {
            return await _context.Carreras.ToListAsync();
        }
    }
}
