using Microsoft.AspNetCore.Identity;
using SistemaCreditosComplementarios.Core.Dtos.Auth;
using SistemaCreditosComplementarios.Core.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAuthRepository
{
    public interface IAuthRepository
    {
        Task<ApplicationUser> RegisterAsync(RegisterDto registerDto);
        Task<ApplicationUser> LoginAsync(LoginDto loginDto);
    }
}
