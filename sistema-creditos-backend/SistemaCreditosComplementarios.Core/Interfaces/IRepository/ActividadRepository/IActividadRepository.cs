using SistemaCreditosComplementarios.Core.Models.ActividadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Interfaces.IRepository.ActividadRepository
{
    public interface IActividadRepository
    {
        public Task<IEnumerable<Actividad>> GetAllAsync();
        public Task<Actividad> GetByIdAsync(int id);
        public Task<IEnumerable<Actividad>> GetByCarreraIds(IEnumerable<int> carreraIds);

        public Task AddAsync(Actividad actividad);
        public Task<Actividad> UpdateAsync(int id, Actividad actividadUpdate);
        public Task DeleteAsync(int id);

        //métodos para buscar en buscador del frontend
        //Task<List<ActividadDto>> GetByNombreAsync(string nombreActividad); 
        //Task<List<ActividadDto>> GetActividadesByTipoAsync(string tipoActividad);
    }
}
