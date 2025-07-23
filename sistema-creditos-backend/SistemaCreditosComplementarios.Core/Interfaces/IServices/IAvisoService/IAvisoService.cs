using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Dtos.Aviso;

namespace SistemaCreditosComplementarios.Core.Interfaces.IServices.IAvisoService
{
    public interface IAvisoService
    {
        Task<AvisoDto> CreateAvisoAsync(AvisoCreateDto createDto);

        Task<IEnumerable<AvisoDto>> GetAllAvisoAsync();
        Task<AvisoDto> GetByIdAsync(int id);

        Task DeleteAvisoAsync(int id, int? coordinadorId, int? departamentoId);
    }
}
