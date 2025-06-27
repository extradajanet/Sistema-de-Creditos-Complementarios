using SistemaCreditosComplementarios.Core.Dtos.Carrera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Interfaces.IServices.ICarreraService
{
    public interface ICarreraService
    {
        //obtiene todas las carreras
        Task<IEnumerable<CarreraDto>> GetAll();
    }
}
