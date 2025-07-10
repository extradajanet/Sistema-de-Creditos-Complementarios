using SistemaCreditosComplementarios.Core.Models.CarreraModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Interfaces.IRepository.ICarreraRepository
{
    public interface ICarreraRepository
    {
        Task<IEnumerable<Carrera>> GetAll();
        Task<Carrera> GetByIdAsync(int id);

    }
}
