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
        Task<IEnumerable<AlumnoDto>> GetByCarreraIdsAsync(IEnumerable<int> carreraIds); //Método para obtener los alumnos de una carrera
        Task<AlumnoDto> GetByIdAsync(int id);
        Task<AlumnoDto> AddAsync(AlumnoCreateDto alumnoCreateDto);
        Task<AlumnoDto> GetByUserIdAsync(string userId); // Método para obtener un alumno por su UserId
        Task<AlumnoDto> AddFromRegisterAsync(RegisterDto registerDto, string usuarioId);
        Task<AlumnoDto> UpdateAsync(AlumnoUpdateDto alumnoUpdateDto); 
        Task<AlumnoDto> DeleteAsync(int id); // devuelve el dto del alumno eliminado
        Task<IEnumerable<AlumnoDto>> GetAlumnosFiltradosByCoordinadorIdAsync(int coordinadorId, int? carreraId = null, double? cantCreditos = null);
        Task<double> GetTotalCreditosAsync(int alumnoId); // Método para obtener los créditos totales de un alumno
    }
}
