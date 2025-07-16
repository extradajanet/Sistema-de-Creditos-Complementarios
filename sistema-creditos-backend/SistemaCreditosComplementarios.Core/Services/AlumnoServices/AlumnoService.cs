using Microsoft.AspNetCore.Identity;
using SistemaCreditosComplementarios.Core.Dtos.Alumno;
using SistemaCreditosComplementarios.Core.Dtos.Auth;
using SistemaCreditosComplementarios.Core.Dtos.Coordinador;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAlumnoRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.ICarreraRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAlumnoService;
using SistemaCreditosComplementarios.Core.Models.Alumnos;
using SistemaCreditosComplementarios.Core.Models.Usuario;
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
        private readonly ICarreraRepository _carreraRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        // Constructor que recibe el repositorio de alumnos
        public AlumnoService(IAlumnoRepository alumnoRepository, UserManager<ApplicationUser> userManager, ICarreraRepository carreraRepository)
        {
            _alumnoRepository = alumnoRepository;
            _userManager = userManager;
            _carreraRepository = carreraRepository;
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
                CarreraNombre = a.Carrera?.Nombre,
            });
        }
        //Método para obtener la lista de alumnos de una carrera
        public async Task<IEnumerable<AlumnoDto>> GetByCarreraIdsAsync(IEnumerable<int> carreraIds)
        {
            var alumnosCA = await _alumnoRepository.GetByCarreraIdsAsync(carreraIds);
            return alumnosCA.Select( a => new AlumnoDto
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
                CarreraNombre = a.Carrera?.Nombre,
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
                CarreraNombre = alumno.Carrera?.Nombre,
            };
        }

        public async Task<AlumnoDto> GetByUserIdAsync(string userId)
        {
            var alumno = await _alumnoRepository.GetByUserIdAsync(userId); 
            if (alumno == null)
            {
                return null; // Retorna null si no se encuentra el alumno
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
                CarreraNombre= alumno.Carrera?.Nombre,
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
            var user = await _userManager.FindByIdAsync(alumnoExistente.UsuarioId);
            if (user == null)
            {
                throw new Exception("User is not linked to any record");
            }
            // Actualiza los datos del alumno existente si los campos tienen algun valor
            if (!string.IsNullOrWhiteSpace(alumnoUpdateDto.Nombre))
                alumnoExistente.Nombre = alumnoUpdateDto.Nombre;

            if (!string.IsNullOrWhiteSpace(alumnoUpdateDto.Apellido))
                alumnoExistente.Apellido = alumnoUpdateDto.Apellido;

            if (alumnoUpdateDto.Semestre.HasValue && alumnoUpdateDto.Semestre.Value>0)
                alumnoExistente.Semestre = alumnoUpdateDto.Semestre.Value;

            // Este campo tiene valor por defecto, así que solo lo asignas si cambia
            if (alumnoUpdateDto.TotalCreditos != alumnoExistente.TotalCreditos)
                alumnoExistente.TotalCreditos = alumnoUpdateDto.TotalCreditos;

            if (alumnoUpdateDto.CarreraId.HasValue && alumnoUpdateDto.CarreraId.Value > 0)
            {
                alumnoExistente.CarreraId = alumnoUpdateDto.CarreraId.Value;
            }


            //updates the email of the student

            var newEmail = alumnoUpdateDto.CorreoElectronico?.Trim();

            if (!string.IsNullOrEmpty(newEmail) && user.Email != newEmail)
            {
                // Prevents duplicate emails
                var existingUserWithEmail = await _userManager.FindByEmailAsync(newEmail);
                if (existingUserWithEmail != null && existingUserWithEmail.Id != user.Id)
                {
                    throw new Exception("El correo electrónico ya está en uso por otro usuario.");
                }

                user.Email = newEmail;
                user.UserName = newEmail;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Error al actualizar el correo electrónico: {errors}");
                }
            }
            //Adds old password confirmation when updating
            var currentPassword = alumnoUpdateDto.CurrentPassword?.Trim();
            var newPassword = alumnoUpdateDto.NewPassword?.Trim();

            if (!string.IsNullOrEmpty(currentPassword) && !string.IsNullOrEmpty(newPassword))
            {
                var passwordChangeResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                if (!passwordChangeResult.Succeeded)
                {
                    var errors = string.Join("; ", passwordChangeResult.Errors.Select(e => e.Description));
                    throw new Exception($"Error al cambiar la contraseña: {errors}");
                }
            }



            //Sends/Saves the new values 

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



        public async Task<double> GetTotalCreditosAsync(int alumnoId)
        {
            var alumno = await _alumnoRepository.GetByIdAsync(alumnoId) ?? throw new Exception("Alumno no encontrado."); // Llamada al repositorio para obtener el alumno por ID
            return (int)alumno.TotalCreditos; // Retorna los créditos totales del alumno
        }

        public async Task<IEnumerable<AlumnoDto>> GetAlumnosCon5CreditosByCoordinadorIdAsync(int coordinadorId)
        {
            var carreras = await _carreraRepository.GetByCoordinadorId(coordinadorId);
            var carreraIds = carreras.Select(c => c.Id);

            var alumnos = await _alumnoRepository.GetByCarreraIdsAsync(carreraIds);

            var result = new List<AlumnoDto>();

            foreach (var alumno in alumnos)
            {
                double totalCreditos = (double)alumno.TotalCreditos; 

                if (totalCreditos >= 5)
                {
                    result.Add(new AlumnoDto
                    {
                        Id = alumno.Id,
                        Nombre = alumno.Nombre,
                        Apellido = alumno.Apellido,
                        NumeroControl = alumno.Usuario.NumeroControl,
                        CorreoElectronico = alumno.Usuario.Email,
                        Semestre= alumno.Semestre,
                        CarreraId = alumno.CarreraId,
                        CarreraNombre = alumno.Carrera?.Nombre,
                        TotalCreditos = (decimal)totalCreditos
                    });
                }
            }

            return result;
        }


    }
}
