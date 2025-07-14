using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Dtos.Departamento;


namespace SistemaCreditosComplementarios.Core.Interfaces.IServices.IDepartmentService
{
    public interface IDepartamentoService
    {
        Task<DepartamentoDto> GetByIdAsync(int  id);
        Task <DepartamentoDto> GetByUserIdAsync (string userId); //Metodo para obtener el departamento por su UserId
        Task <DepartamentoDto> UpdateAsync (DepartamentoUpdateDto departamentoUpdateDto);
    }
}
