using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IDepartamentoRepository;
using SistemaCreditosComplementarios.Core.Models.Departamentos;
using SistemaCreditosComplementarios.Infraestructure.Data;

namespace SistemaCreditosComplementarios.Infraestructure.Repositories
{
    public class DepartamentoRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        
        public DepartamentoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Departamento> GetByIdAsync(int id)
        {
            return await _context.Departamentos
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Departamento> GetByUserIdAsync(string userId)
        {
            return await _context.Departamentos
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(a => a.Usuario.Id == userId);
        }

        public async Task<Departamento> UpdateAsync(Departamento departamento)
        {
            _context.Departamentos.Update(departamento);
            await _context.SaveChangesAsync();
            return departamento;
        }

    }
}
