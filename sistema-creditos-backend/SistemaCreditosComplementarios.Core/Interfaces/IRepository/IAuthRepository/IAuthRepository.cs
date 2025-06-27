using Microsoft.AspNetCore.Identity;
using SistemaCreditosComplementarios.Core.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAuthRepository
{
    public interface IAuthRepository
    {
        Task<IdentityUser> RegisterAsync(RegisterDto registerDto);
        Task<IdentityUser> LoginAsync(LoginDto loginDto);
    }
}
