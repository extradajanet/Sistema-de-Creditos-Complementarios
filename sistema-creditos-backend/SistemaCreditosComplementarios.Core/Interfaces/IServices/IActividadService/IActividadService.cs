using SistemaCreditosComplementarios.Core.Dtos.Actividad;
using SistemaCreditosComplementarios.Core.Models.ActividadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Interfaces.IServices.IActividadService
{
    public interface IActividadService
    {
        Task<IEnumerable<ActividadDto>> GetAllAsync();
        Task<ActividadDto> GetByIdAsync(int id);

        Task<IEnumerable<ActividadDto>> GetByCoordinadorIdAsync(int coordinadorId);

        Task<ActividadDto> AddAsync(ActividadCreateDto actividadCreateDto);
        Task<ActividadDto> UpdateAsync(int id, ActividadUpdateDto actividadUpdateDto);
        Task DeleteAsync(int id);

        //métodos para buscar en buscador del frontend
        //Task<List<ActividadDto>> GetByNombreAsync(string nombreActividad); 
        //Task<List<ActividadDto>> GetActividadesByTipoAsync(string tipoActividad);
    }
}
