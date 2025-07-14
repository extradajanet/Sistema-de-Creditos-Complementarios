using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Models.Coordinadores;

namespace SistemaCreditosComplementarios.Core.Interfaces.IRepository.ICoordinadorRepository
{
    public interface ICoordinadorRepository
    {
        Task<Coordinador> GetByIdAsync(int id);
        Task<Coordinador> GetByUserIdAsync(string  userId);
        Task <Coordinador> UpdateAsync(Coordinador coordinador);
    }
}
