using Microsoft.AspNetCore.Identity;
using SistemaCreditosComplementarios.Core.Dtos.Auth;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAuthRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Infraestructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityUser> RegisterAsync(RegisterDto registerDto)
        {
            var user = new IdentityUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                throw new Exception("User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            // Si el rol no existe, se crea y se asigna al usuario
            if (!await _roleManager.RoleExistsAsync("Alumno"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Alumno"));
            }

            await _userManager.AddToRoleAsync(user, "Alumno");

            return user;

        }

        public async Task<IdentityUser> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                throw new UnauthorizedAccessException("Invalid login attempt.");
            }
            return user;
        }
    }
}
