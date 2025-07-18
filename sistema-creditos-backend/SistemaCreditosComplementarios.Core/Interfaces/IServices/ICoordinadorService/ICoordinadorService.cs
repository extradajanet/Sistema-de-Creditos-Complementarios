using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Dtos.Coordinador;

namespace SistemaCreditosComplementarios.Core.Interfaces.IServices.ICoordinadorService
{
    public interface ICoordinadorService
    {
        Task<CoordinadorDto> GetByIdAsync(int id);
        Task<CoordinadorDto > GetByUserIdAsync(string userId);
        Task<CoordinadorDto> UpdateAsync(CoordinadorUpdateDto coordinadorUpdateDto);
    }
}
