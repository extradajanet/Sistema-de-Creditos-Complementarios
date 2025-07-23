using SistemaCreditosComplementarios.Core.Models.ActividadModel;
using SistemaCreditosComplementarios.Core.Models.Alumnos;
using SistemaCreditosComplementarios.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Models.AlumnosActividades
{
    public class AlumnoActividad
    {
        public int IdAlumno { get; set; }
        public Alumno Alumno { get; set; }

        public int IdActividad { get; set; }
        public Actividad Actividad { get; set; }

        public EstadoAlumnoActividad EstadoAlumnoActividad { get; set; }
        public DateTime FechaInscripcion { get; set; }
    }
}
