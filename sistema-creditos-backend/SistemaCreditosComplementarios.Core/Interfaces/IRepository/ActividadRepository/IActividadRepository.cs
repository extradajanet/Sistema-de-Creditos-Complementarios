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
        public Task<IEnumerable<ActividadModels>> GetAllAsync();
        public Task<ActividadModels> GetByIdAsync(int id);
        public Task AddAsync(ActividadModels actividad);
        public Task<ActividadModels> UpdateAsync(int id, ActividadModels actividadUpdate);
        public Task DeleteAsync(int id);

        //métodos para buscar en buscador del frontend
        //Task<List<ActividadDto>> GetByNombreAsync(string nombreActividad); 
        //Task<List<ActividadDto>> GetActividadesByTipoAsync(string tipoActividad);
    }
}
