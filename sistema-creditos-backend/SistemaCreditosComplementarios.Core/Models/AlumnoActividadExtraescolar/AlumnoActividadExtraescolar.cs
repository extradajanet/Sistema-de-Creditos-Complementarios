using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Models.ActividadExtraescolarModel;
using SistemaCreditosComplementarios.Core.Models.Alumnos;
using SistemaCreditosComplementarios.Core.Models.Enum;

namespace SistemaCreditosComplementarios.Core.Models.AlumnoActividadExtraescolares
{
    public class AlumnoActividadExtraescolar
    {
        public int IdAlumno { get; set; }
        public Alumno Alumno { get; set; }

        public int IdActividadExtraescolar { get; set; }
        public ActividadExtraescolar ActividadExtraescolar { get; set; }

        public EstadoAlumnoActividad EstadoAlumnoActividad { get; set; }
        public DateTime FechaInscripcion { get; set; }
    }
}
