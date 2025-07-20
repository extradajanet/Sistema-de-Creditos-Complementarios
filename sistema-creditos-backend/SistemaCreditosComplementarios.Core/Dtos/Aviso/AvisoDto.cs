using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Dtos.Aviso
{
    public class AvisoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public int? DepartamentoId { get; set; }
        public string? DepartamentoNombre { get; set; }
        public int? CoordinadorId { get; set; }
        public string? CoordinadorNombre { get; set; }
        public string? CoordinadorApellido { get; set; }
    }

    public class AvisoCreateDto
    {
        public string Titulo { get; set; }
        public string Mensaje { get; set; }

        public int? DepartamentoId { get; set; } // nullable: only one sender required
        public int? CoordinadorId { get; set; }
    }

}

