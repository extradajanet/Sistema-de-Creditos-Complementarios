using SistemaCreditosComplementarios.Core.Dtos.Alumno;
using SistemaCreditosComplementarios.Core.Dtos.Auth;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAlumnoRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAlumnoService;
using SistemaCreditosComplementarios.Core.Models.Alumnos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Services.AlumnoServices
{
    public class AlumnoService : IAlumnoService
    {
        private readonly IAlumnoRepository _alumnoRepository;

        // Constructor que recibe el repositorio de alumnos
        public AlumnoService(IAlumnoRepository alumnoRepository)
        {
            _alumnoRepository = alumnoRepository;
        }

        // Método para obtener todos los alumnos
        public async Task<IEnumerable<AlumnoDto>> GetAllAsync()
        {
            var alumnos = await _alumnoRepository.GetAllAsync(); // Llamada al repositorio para obtener todos los alumnos
            return alumnos.Select(a => new AlumnoDto
            {
                Id = a.Id,
                NumeroControl = a.Usuario.NumeroControl,
                Nombre = a.Nombre,
                Apellido = a.Apellido,
                CorreoElectronico = a.Usuario.Email,
                FechaRegistro = a.FechaRegistro,
                Semestre = a.Semestre,
                TotalCreditos = a.TotalCreditos,
                CarreraId = a.CarreraId,
            });
        }

        // Método para obtener un alumno por su ID
        public async Task<AlumnoDto> GetByIdAsync(int id)
        {
            var alumno = await _alumnoRepository.GetByIdAsync(id); // Llamada al repositorio para obtener el alumno por ID
            if (alumno == null)
            {
                throw new Exception("Alumno no encontrado.");
            }
            return new AlumnoDto
            {
                Id = alumno.Id,
                NumeroControl = alumno.Usuario.NumeroControl,
                Nombre = alumno.Nombre,
                Apellido = alumno.Apellido,
                CorreoElectronico = alumno.Usuario.Email,
                FechaRegistro = alumno.FechaRegistro,
                Semestre = alumno.Semestre,
                TotalCreditos = alumno.TotalCreditos,
                CarreraId = alumno.CarreraId,
            };
        }

        // Método para agregar un nuevo alumno
        public async Task<AlumnoDto> AddAsync(AlumnoCreateDto alumnoCreateDto)
        {
            var nuevoAlumno = new Alumno
            {
                Nombre = alumnoCreateDto.Nombre,
                Apellido = alumnoCreateDto.Apellido,
                Semestre = alumnoCreateDto.Semestre,
                TotalCreditos = alumnoCreateDto.TotalCreditos,
                CarreraId = alumnoCreateDto.CarreraId, 
                FechaRegistro = DateTime.UtcNow
            };
            await _alumnoRepository.AddAsync(nuevoAlumno); // Llamada al repositorio para agregar el nuevo alumno
            return new AlumnoDto
            {
                Id = nuevoAlumno.Id,
                NumeroControl = nuevoAlumno.Usuario.NumeroControl, 
                Nombre = nuevoAlumno.Nombre,
                Apellido = nuevoAlumno.Apellido,
                FechaRegistro = nuevoAlumno.FechaRegistro,
                Semestre = nuevoAlumno.Semestre,
                TotalCreditos = nuevoAlumno.TotalCreditos,
                CarreraId = nuevoAlumno.CarreraId,
            };
        }

        // Método para agregar un alumno a partir de un registro de usuario
        public async Task<AlumnoDto> AddFromRegisterAsync(RegisterDto registerDto, string usuarioId)
        {
            var nuevoAlumno = new Alumno
            {
                Nombre = registerDto.Nombre,
                Apellido = registerDto.Apellido,
                Semestre = registerDto.Semestre,
                TotalCreditos = 0, 
                CarreraId = registerDto.CarreraId, 
                FechaRegistro = DateTime.UtcNow,
                UsuarioId = usuarioId // Asignar el ID del usuario que se registra
            };
            await _alumnoRepository.AddAsync(nuevoAlumno); // Llamada al repositorio para agregar el nuevo alumno
            return new AlumnoDto
            {
                Id = nuevoAlumno.Id,
                NumeroControl = nuevoAlumno.Usuario.NumeroControl,
                Nombre = nuevoAlumno.Nombre,
                Apellido = nuevoAlumno.Apellido,
                CorreoElectronico = nuevoAlumno.Usuario.Email,
                Semestre = registerDto.Semestre,
                TotalCreditos = nuevoAlumno.TotalCreditos,
                FechaRegistro = nuevoAlumno.FechaRegistro,
                CarreraId = nuevoAlumno.CarreraId,
            };
        }

        public async Task<AlumnoDto> UpdateAsync(AlumnoUpdateDto alumnoUpdateDto)
        {
            var alumnoExistente = await _alumnoRepository.GetByIdAsync(alumnoUpdateDto.Id); // Llamada al repositorio para obtener el alumno por ID
            if (alumnoExistente == null)
            {
                throw new Exception("Alumno no encontrado.");
            }
            // Actualiza los datos del alumno existente
            alumnoExistente.Nombre = alumnoUpdateDto.Nombre;
            alumnoExistente.Apellido = alumnoUpdateDto.Apellido;
            alumnoExistente.Semestre = alumnoUpdateDto.Semestre;
            alumnoExistente.TotalCreditos = alumnoUpdateDto.TotalCreditos;
            alumnoExistente.CarreraId = alumnoUpdateDto.CarreraId;


            var alumnoActualizado = await _alumnoRepository.UpdateAsync(alumnoExistente); // Llamada al repositorio para actualizar el alumno
            
            return new AlumnoDto
            {
                Id = alumnoActualizado.Id,
                NumeroControl = alumnoActualizado.Usuario.NumeroControl,
                Nombre = alumnoActualizado.Nombre,
                Apellido = alumnoActualizado.Apellido,
                CorreoElectronico = alumnoActualizado.Usuario.Email,
                FechaRegistro = alumnoActualizado.FechaRegistro,
                Semestre = alumnoActualizado.Semestre,
                TotalCreditos = alumnoActualizado.TotalCreditos,
                CarreraId = alumnoActualizado.CarreraId,
            };
        }

        public async Task<AlumnoDto> DeleteAsync(int id)
        {
            var alumno = await _alumnoRepository.GetByIdAsync(id); // Llamada al repositorio para obtener el alumno por ID
            if (alumno == null)
            {
                throw new Exception("Alumno no encontrado.");
            }
            await _alumnoRepository.DeleteAsync(id); // Llamada al repositorio para eliminar el alumno
            return new AlumnoDto
            {
                Id = alumno.Id,
                NumeroControl = alumno.Usuario.NumeroControl,
                Nombre = alumno.Nombre,
                Apellido = alumno.Apellido,
                FechaRegistro = alumno.FechaRegistro,
                CarreraId = alumno.CarreraId,
            };
        }
    }
}
