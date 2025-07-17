using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Models.Enum;

namespace SistemaCreditosComplementarios.Core.Dtos.ActividadExtraescolarAlumno
{
    public class AlumnoActividadExtraescolarDto
    {
        public int AlumnoId { get; set; }
        public string AlumnoNombre { get; set; }
        public int ActividadExtraescolarId { get; set; }
        public string ActividadExtraescolarNombre { get; set; }
        public EstadoAlumnoActividad EstadoAlumnoActividad { get; set; }
        public DateTime FechaRegistro { get; set; }
        public decimal CreditosObtenidos { get; set; }
    }

    public class AlumnoActividadExtraescolarCreateDto
    {
        public int AlumnoId { get; set; }
        public int ActividadExtraescolarId { get; set; }
        public EstadoAlumnoActividad EstadoAlumnoActividad { get; set; }
        public DateTime FechaInscripcion { get; set; } = DateTime.UtcNow;
    }

    public class AlumnoActividadExtraescolarUpdate
    {
        public int AlumnoId { get; set; }
        public int ActividadExtraescolarId { get; set; }
        public EstadoAlumnoActividad EstadoAlumnoActividad { get; set; }
        public DateTime FechaInscripcion { get; set; } = DateTime.UtcNow;
    }


    public class AlumnoInscritoExtraescolarDto
    {
        public int AlumnoId { get; set; }
        public string NombreCompleto { get; set; }
        public string CarreraNombre { get; set; }
        public decimal CreditosObtenidos { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public EstadoAlumnoActividad EstadoAlumnoActividad { get; set; }
    }

    public class ExtraescolarAlumnoDto
    {
        public int ActividadId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImagenNombre { get; set; }
        public decimal Creditos { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public EstadoAlumnoActividad EstadoAlumnoActividad { get; set; }
    }


}
