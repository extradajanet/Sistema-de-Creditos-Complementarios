using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Models.Departamentos;

namespace SistemaCreditosComplementarios.Core.Interfaces.IRepository.IDepartamentoRepository
{
    public interface IDepartmentRepository
    {
        Task<Departamento> GetByIdAsync(int id);
        Task<Departamento> GetByUserIdAsync(string userId);

        Task<Departamento> UpdateAsync(Departamento departamento);
    }
}
