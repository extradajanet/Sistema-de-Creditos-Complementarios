using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Dtos.ActividadesExtraescolares;

namespace SistemaCreditosComplementarios.Core.Interfaces.IServices.IActividadExtraescolarService
{
    public interface IActividadExtraescolarService
    {
        Task<IEnumerable<ActividadExtraescolarDto>> GetAllAsync();
        Task<ActividadExtraescolarDto> GetByIdAsync(int id);
        Task<ActividadExtraescolarDto> AddAsync(ActividadExtraescolarCreateDto actividadextraescolarCreateDto);
        Task<ActividadExtraescolarDto> UpdateAsync(int id, ActividadExtraescolarCreateDto actividadextraescolarCreateDto);
        Task DeleteAsync(int id);

        //métodos para buscar en buscador del frontend
        //Task<List<ActividadDto>> GetByNombreAsync(string nombreActividad); 
        //Task<List<ActividadDto>> GetActividadesByTipoAsync(string tipoActividad);
    }
}
