using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Models.Coordinadores;
using SistemaCreditosComplementarios.Core.Models.Departamentos;
using SistemaCreditosComplementarios.Core.Models.Alumnos;

namespace SistemaCreditosComplementarios.Core.Models.CarreraModel
{
    public class Carrera
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public int? DepartamentoId { get; set; }
        public Departamento? Departamento { get; set; }
        public int? CoordinadorId { get; set; }
        public Coordinador? Coordinador { get; set; }
    }
}
