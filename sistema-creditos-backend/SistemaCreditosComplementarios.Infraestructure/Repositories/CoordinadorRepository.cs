using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.ICoordinadorRepository;
using SistemaCreditosComplementarios.Core.Models.Coordinadores;
using SistemaCreditosComplementarios.Infraestructure.Data;

namespace SistemaCreditosComplementarios.Infraestructure.Repositories
{
    public class CoordinadorRepository : ICoordinadorRepository
    {
        private readonly ApplicationDbContext _context;

        public CoordinadorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Coordinador> GetByIdAsync(int id)
        {
            return await _context.Coordinadores
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Coordinador> GetByUserIdAsync(string userId)
        {
            return await _context.Coordinadores
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(a => a.Usuario.Id == userId);
        }

        public async Task<Coordinador> UpdateAsync(Coordinador coordinador)
        {
            _context.Coordinadores.Update(coordinador);
            await _context.SaveChangesAsync();
            return coordinador;
        }
    }
}
