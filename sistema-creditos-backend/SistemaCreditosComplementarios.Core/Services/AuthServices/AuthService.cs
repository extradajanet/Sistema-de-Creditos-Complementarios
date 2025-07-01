using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SistemaCreditosComplementarios.Core.Dtos.Auth;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAuthRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAlumnoService;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAuthService;
using SistemaCreditosComplementarios.Core.Models.Usuario;
using SistemaCreditosComplementarios.Core.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaCreditosComplementarios.Core.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IAlumnoService _alumnoService;
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthService
            (
            IOptions<JwtOptions> jwtOptions, 
            IAlumnoService alumnoService,
            IAuthRepository authRepository,
            UserManager<ApplicationUser> userManager
            )
        {
            _jwtOptions = jwtOptions.Value;
            _alumnoService = alumnoService;
            _authRepository = authRepository;
            _userManager = userManager;
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            var user = await _authRepository.RegisterAsync(registerDto);

            // Se crea el alumno asociado al usuario
            await _alumnoService.AddFromRegisterAsync(registerDto, user.Id);

            return "User registered successfully.";
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var user = await _authRepository.LoginAsync(loginDto);

            var token = GenerateToken(user);

            return token;
        }

        // Genera un token JWT para el usuario autenticado

        private string GenerateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = _userManager.GetRolesAsync(user).Result;
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
