using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditosComplementarios.Core.Dtos.Auth;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAuthRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAuthService;

namespace SistemaCreditosComplementarios.API.Controllers.AuthController
{
    /// <summary>
    /// Controlador para autenticación y registro de usuarios.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Constructor del controlador de autenticación.
        /// </summary>
        /// <param name="authService">Servicio de autenticación.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="registerDto">Datos del nuevo usuario.</param>
        /// <returns>Mensaje de éxito o error de validación.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (registerDto == null)
            {
                return BadRequest("Datos de registro inválidos.");
            }

            var result = await _authService.RegisterAsync(registerDto);

            if (result != null)
            {
                return Ok(new { message = "Registro exitoso" });
            }

            return BadRequest("El registro falló. Verifica los datos e intenta nuevamente.");
        }

        /// <summary>
        /// Inicia sesión en el sistema.
        /// </summary>
        /// <param name="loginDto">Credenciales del usuario.</param>
        /// <returns>Token de autenticación o mensaje de error.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("Datos de inicio de sesión inválidos.");
            }

            try
            {
                var response = await _authService.LoginAsync(loginDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al iniciar sesión: {ex.Message}");
            }
        }
    }
}
