using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SistemaCreditosComplementarios.Core.Dtos.Coordinador;
using SistemaCreditosComplementarios.Core.Dtos.Departamento;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IDepartamentoRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IDepartmentService;
using SistemaCreditosComplementarios.Core.Models.Usuario;

namespace SistemaCreditosComplementarios.Core.Services.DepartamentoServices
{
    public class DepartmentService : IDepartamentoService
    {
        private readonly IDepartmentRepository _departamentoRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public DepartmentService(IDepartmentRepository departamentoRepository, UserManager<ApplicationUser> userManager) 
        { 
            _departamentoRepository = departamentoRepository;
            _userManager = userManager;
        }

        //Método para obtener un departamento por su ID
        public async Task<DepartamentoDto> GetByIdAsync(int id)
        {
            var departamento = await _departamentoRepository.GetByIdAsync(id);
            if (departamento == null)
            {
                throw new Exception("Departamento no encontrado");
            }
            return new DepartamentoDto
            {
                Id = departamento.Id,
                Nombre = departamento.Nombre,
                CorreoElectronico = departamento.Usuario.Email,
                FechaRegistro = departamento.FechaRegistro,
            };
        }

        public async Task<DepartamentoDto> GetByUserIdAsync(string userId)
        {
            var departamento = await _departamentoRepository.GetByUserIdAsync(userId);
            if (departamento == null)
            {
                throw new Exception("Departamento no encontrado");
            }
            return new DepartamentoDto
            {
                Id = departamento.Id,
                Nombre = departamento.Nombre,
                CorreoElectronico = departamento.Usuario.Email,
                FechaRegistro = departamento.FechaRegistro,
            };
        }

        //Método para actualizar los datos del departamento
        public async Task<DepartamentoDto> UpdateAsync(DepartamentoUpdateDto departamentoUpdateDto)
        {
            var departamentoExistente = await _departamentoRepository.GetByIdAsync(departamentoUpdateDto.Id);
            if (departamentoExistente == null) { throw new Exception("Departamento no encontrado. "); }
            var user = await _userManager.FindByIdAsync(departamentoExistente.UsuarioId);
            if (user == null)
            {
                throw new Exception("User is not linked to any record");
            }
            //Updates the name of the department
            if (!string.IsNullOrWhiteSpace(departamentoUpdateDto.Nombre))
            {
                departamentoExistente.Nombre = departamentoUpdateDto.Nombre;

            }

            //Updates the email of the department and validates that the email isn't assigned to another user
            var newEmail = departamentoUpdateDto.CorreoElectronico?.Trim();

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
            var currentPassword = departamentoUpdateDto.CurrentPassword?.Trim();
            var newPassword = departamentoUpdateDto.NewPassword?.Trim();

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
            var departamentoActualizado = await _departamentoRepository.UpdateAsync(departamentoExistente);

            return new DepartamentoDto
            {
                Id = departamentoActualizado.Id,
                Nombre = departamentoActualizado.Nombre,
                CorreoElectronico= user.Email,
            };
        }
    }
}
