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
        // implementa el modelo de actividad y los métodos que se van a utilizar en el servicio
        public Task<IEnumerable<ActividadModels>> GetAllAsync();
        public Task<ActividadModels> GetByIdAsync(int id);
        // Crear la actividad 
        public Task AddAsync(ActividadModels actividad);
        // Actualizar la actividad
        public Task<ActividadModels> UpdateAsync(int id, ActividadModels actividadUpdate); // recibe el id de la actividad a actualizar y el modelo con los nuevos datos
        // Eliminar la actividad
        public Task DeleteAsync(int id);

        //métodos para buscar en buscador del frontend
        //Task<List<ActividadDto>> GetByNombreAsync(string nombreActividad); 
        //Task<List<ActividadDto>> GetActividadesByTipoAsync(string tipoActividad);
    }
}
