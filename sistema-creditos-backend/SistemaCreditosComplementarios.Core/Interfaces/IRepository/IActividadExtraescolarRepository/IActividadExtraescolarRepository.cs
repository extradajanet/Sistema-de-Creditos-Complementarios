using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Models.ActividadExtraescolar;

namespace SistemaCreditosComplementarios.Core.Interfaces.IRepository.IActividadExtraescolarRepository
{
    public interface IActividadExtraescolarRepository
    {
        public Task<IEnumerable<ActividadExtraescolar>> GetAllAsync();
        public Task<ActividadExtraescolar> GetByIdAsync(int id);
        public Task AddAsync(ActividadExtraescolar actividadextraescolar);
        public Task<ActividadExtraescolar> UpdateAsync(int id, ActividadExtraescolar actividadextraescolarUpdate);
        public Task DeleteAsync(int id);

    }
}
