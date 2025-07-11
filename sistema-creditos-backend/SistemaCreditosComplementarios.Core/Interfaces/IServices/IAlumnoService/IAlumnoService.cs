using SistemaCreditosComplementarios.Core.Dtos.Alumno;
using SistemaCreditosComplementarios.Core.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Interfaces.IServices.IAlumnoService
{
    public interface IAlumnoService
    {
        Task<IEnumerable<AlumnoDto>> GetAllAsync();
        Task<AlumnoDto> GetByIdAsync(int id);
        Task<AlumnoDto> AddAsync(AlumnoCreateDto alumnoCreateDto);
        Task<AlumnoDto> AddFromRegisterAsync(RegisterDto registerDto, string usuarioId);
        Task<AlumnoDto> UpdateAsync(AlumnoUpdateDto alumnoUpdateDto); 
        Task<AlumnoDto> DeleteAsync(int id); // devuelve el dto del alumno eliminado
        Task<double> GetTotalCreditosAsync(int alumnoId); // Método para obtener los créditos totales de un alumno
    }
}
