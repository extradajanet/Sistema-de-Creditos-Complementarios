using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Dtos.Alumno
{
    public class AlumnoDto
    {
        public int Id { get; set; }
        public string NumeroControl { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public DateTime FechaRegistro { get; set; } 
        public int CarreraId { get; set; } 
    }

    public class AlumnoCreateDto
    {
        public string NumeroControl { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int CarreraId { get; set; }
    }

    public class AlumnoUpdateDto
    {
        public int Id { get; set; }
        public string NumeroControl { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int CarreraId { get; set; }
    }
}
