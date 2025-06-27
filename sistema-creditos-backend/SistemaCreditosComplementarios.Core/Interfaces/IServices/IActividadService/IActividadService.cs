using SistemaCreditosComplementarios.Core.Dtos.Actividad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Interfaces.IServices.IActividadService
{
    public interface IActividadService
    {
        //implementa el dto de actividad y los métodos que se van a utilizar en el controlador
        Task<IEnumerable<ActividadDto>> GetAllAsync();
        Task<ActividadDto> GetByIdAsync(int id);
        Task<ActividadDto> AddAsync(ActividadCreateDto actividadCreateDto);
        Task<ActividadDto> UpdateAsync(int id, ActividadCreateDto actividadUpdateDto); // recibe el id de la actividad a actualizar y el dto con los nuevos datos
        Task DeleteAsync(int id);

        //métodos para buscar en buscador del frontend
        //Task<List<ActividadDto>> GetByNombreAsync(string nombreActividad); 
        //Task<List<ActividadDto>> GetActividadesByTipoAsync(string tipoActividad);
    }
}
