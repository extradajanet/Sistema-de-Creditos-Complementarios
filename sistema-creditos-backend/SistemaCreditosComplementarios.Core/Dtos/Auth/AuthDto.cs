using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Dtos.Auth
{
    public class RegisterDto
    {
        public string Email { get; set; } 
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string NumeroControl { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Semestre { get; set; }
        public decimal TotalCreditos { get; set; } = 0;
        public int CarreraId { get; set; }

    }

    public class LoginDto
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public int AlumnoId { get; set; }
    }
}
