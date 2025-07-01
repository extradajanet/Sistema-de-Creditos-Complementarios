using Microsoft.AspNetCore.Identity;
using SistemaCreditosComplementarios.Core.Models.CarreraModel;
using SistemaCreditosComplementarios.Core.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Models.Alumno
{
    public class Alumno
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NumeroControl { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public int CarreraId { get; set; }
        public CarreraModels Carrera { get; set; }
        public string UsuarioId { get; set; }
        public ApplicationUser Usuario { get; set; }
    }
}
