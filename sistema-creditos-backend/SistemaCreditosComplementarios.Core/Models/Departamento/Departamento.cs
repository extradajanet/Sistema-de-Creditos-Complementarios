using Microsoft.AspNetCore.Identity;
using SistemaCreditosComplementarios.Core.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Models.Departamento
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public string UsuarioId { get; set; }
        public ApplicationUser Usuario { get; set; }
    }
}
