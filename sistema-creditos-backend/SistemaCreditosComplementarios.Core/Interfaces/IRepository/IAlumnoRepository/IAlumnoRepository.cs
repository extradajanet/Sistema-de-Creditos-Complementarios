using SistemaCreditosComplementarios.Core.Models.Alumno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAlumnoRepository
{
    public interface IAlumnoRepository
    {
        Task<IEnumerable<Alumno>> GetAllAsync();
        Task<Alumno> GetByIdAsync(int id);
        Task AddAsync(Alumno alumno);
        Task<Alumno> UpdateAsync(Alumno alumno);
        Task DeleteAsync(int id);
    }
}
