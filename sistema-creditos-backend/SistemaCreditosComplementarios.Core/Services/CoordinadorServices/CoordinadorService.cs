using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SistemaCreditosComplementarios.Core.Dtos.Coordinador;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.ICoordinadorRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.ICoordinadorService;
using SistemaCreditosComplementarios.Core.Models.Usuario;

namespace SistemaCreditosComplementarios.Core.Services.CoordinadorServices
{
    public class CoordinadorService : ICoordinadorService
    {
        private readonly ICoordinadorRepository _coordinadorRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CoordinadorService(ICoordinadorRepository coordinadorRepository, UserManager<ApplicationUser> userManager)
        {
            _coordinadorRepository = coordinadorRepository;
            _userManager = userManager;
        }


        //Método para obtener un coordinador por su ID
        public async Task<CoordinadorDto> GetByIdAsync(int id)
        {
            var coordinador = await _coordinadorRepository.GetByIdAsync(id);
            if (coordinador == null) { throw new Exception("Coordinador no encontrado"); }
            return new CoordinadorDto
            {
                Id = coordinador.Id,
                Nombre = coordinador.Nombre,
                Apellido = coordinador.Apellido,
                CorreoElectronico = coordinador.Usuario.Email,
                FechaRegistro = coordinador.FechaRegistro,
            };
        }

        public async Task<CoordinadorDto> GetByUserIdAsync(string userId)
        {
            var coordinador = await _coordinadorRepository.GetByUserIdAsync(userId);
            if (coordinador == null) { throw new Exception("Coordinador no encontrado"); }
            return new CoordinadorDto
            {
                Id = coordinador.Id,
                Nombre = coordinador.Nombre,
                Apellido = coordinador.Apellido,
                CorreoElectronico = coordinador.Usuario.Email,
                FechaRegistro = coordinador.FechaRegistro,
            };
        }

        //Método para actualizar los datos del coordinador
        public async Task<CoordinadorDto> UpdateAsync(CoordinadorUpdateDto coordinadorUpdateDto)
        {
            var coordinadorExistente = await _coordinadorRepository.GetByIdAsync(coordinadorUpdateDto.Id);
            if (coordinadorExistente == null) { throw new Exception("Coordinador no encontrado"); }

    
            var user = await _userManager.FindByIdAsync(coordinadorExistente.UsuarioId); 
            if (user == null)
            {
                throw new Exception("User is not linked to any record");
            }
            //updates the name of the coordinator
            if (!string.IsNullOrWhiteSpace(coordinadorUpdateDto.Nombre))
            {
                coordinadorExistente.Nombre = coordinadorUpdateDto.Nombre;
            }
            if (!string.IsNullOrWhiteSpace(coordinadorUpdateDto.Apellido))
            {
                coordinadorExistente.Apellido = coordinadorUpdateDto.Apellido;
            }
            //updates the email of the coordinator


            var newEmail = coordinadorUpdateDto.CorreoElectronico?.Trim();

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
            var currentPassword = coordinadorUpdateDto.CurrentPassword?.Trim();
            var newPassword = coordinadorUpdateDto.NewPassword?.Trim();

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
            var coordinadorActualizado = await _coordinadorRepository.UpdateAsync(coordinadorExistente);
            return new CoordinadorDto
            {
                Id = coordinadorActualizado.Id,
                Nombre = coordinadorActualizado.Nombre,
                CorreoElectronico = user.Email,

            };
        }
    }
}
