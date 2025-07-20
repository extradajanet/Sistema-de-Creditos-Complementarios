using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAvisoRepository;
using SistemaCreditosComplementarios.Core.Models.Avisos;
using SistemaCreditosComplementarios.Infraestructure.Data;

namespace SistemaCreditosComplementarios.Infraestructure.Repositories
{
    public class AvisoRepository : IAvisoRepository
    {
        private readonly ApplicationDbContext _context;

        public AvisoRepository (ApplicationDbContext context)
        {
            _context = context;
        }


        //Obtiene todos los avisos
        public async Task<IEnumerable<Aviso>> GetAllAvisoAsync()
        {
            return await _context.Avisos
                .Include(a => a.Departamento)
                .Include(a => a.Coordinador)
                .ToListAsync();

        }

        // Crear un aviso
        public async Task<Aviso> CreateAvisoAsync(Aviso aviso)
        {
            _context.Avisos.Add(aviso);
            await _context.SaveChangesAsync();

            return await _context.Avisos
                .Include(a => a.Coordinador)
                .Include(a => a.Departamento)
                .FirstOrDefaultAsync(a => a.Id == aviso.Id);
        }

    }
}
