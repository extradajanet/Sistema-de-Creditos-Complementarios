using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SistemaCreditosComplementarios.Core.Dtos.Auth;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAuthRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAlumnoService;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAuthService;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IDepartmentService;
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
        private readonly IDepartamentoService _departamentoService;
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthService
            (
            IOptions<JwtOptions> jwtOptions, 
            IAlumnoService alumnoService,
            IDepartamentoService departamentoService,
            IAuthRepository authRepository,
            UserManager<ApplicationUser> userManager
            )
        {
            _jwtOptions = jwtOptions.Value;
            _alumnoService = alumnoService;
            _departamentoService = departamentoService;
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

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _authRepository.LoginAsync(loginDto);

            var alumno = await _alumnoService.GetByUserIdAsync(user.Id);
           

            var token = GenerateToken(user);

           var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Contains("Alumno"))
            {
               return new LoginResponseDto
                {
                    Token = token,
                   Expiration = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryInMinutes),
                    AlumnoId = alumno?.Id
                };
            } 
           else if (userRoles.Contains("Departamento"))
            {
                var departamento = await _departamentoService.GetByUserIdAsync(user.Id);
                return new LoginResponseDto
                {
                    Token = token,
                    Expiration = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryInMinutes),
                    DepartamentoId = departamento?.Id
                };
            }
            //else if (userRoles.Contains("Coordinador"))
            //{
            //    var coordinador = await _alumnoService.GetCoordinadorByUserIdAsync(user.Id);
            //    return new LoginResponseDto
            //    {
            //        Token = token,
            //        Expiration = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryInMinutes),
            //        CoordinadorId = coordinador?.Id
            //    };
            //}

            return new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryInMinutes),
                AlumnoId = alumno?.Id
            };
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
