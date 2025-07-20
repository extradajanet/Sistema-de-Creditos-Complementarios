using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Models.Coordinadores;
using SistemaCreditosComplementarios.Core.Models.Departamentos;

namespace SistemaCreditosComplementarios.Core.Models.Avisos
{
    public class Aviso
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Mensaje { get; set; }

        public int? DepartamentoId { get; set; }
        public Departamento? Departamento { get; set; }
        public int? CoordinadorId { get; set; }
        public Coordinador? Coordinador { get; set; }
    }
}
