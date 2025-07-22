using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Models.Avisos;

namespace SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAvisoRepository
{
    public interface IAvisoRepository
    {
        Task<Aviso> CreateAvisoAsync(Aviso aviso);

        public Task<IEnumerable<Aviso>> GetAllAvisoAsync();
        
        public Task<Aviso> GetByIdAsync(int id);

        public Task DeleteAvisoAsync(Aviso aviso);
    }
}
