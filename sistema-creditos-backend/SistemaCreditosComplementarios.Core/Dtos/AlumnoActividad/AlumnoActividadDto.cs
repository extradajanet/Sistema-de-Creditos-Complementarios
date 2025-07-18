using SistemaCreditosComplementarios.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Dtos.AlumnoActividad
{
    public class AlumnoActividadDto
    {
        public int AlumnoId { get; set; }
        public string AlumnoNombre { get; set; }
        public int ActividadId { get; set; }
        public string ActividadNombre { get; set; }
        public EstadoAlumnoActividad EstadoAlumnoActividad { get; set; }
        public DateTime FechaRegistro { get; set; }
        public decimal CreditosObtenidos { get; set; }
        public Genero Genero { get; set; }
    }

    public class AlumnoActividadCreateDto
    {
        public int AlumnoId { get; set; }
        public int ActividadId { get; set; }
        public EstadoAlumnoActividad EstadoAlumnoActividad { get; set; }
        public DateTime FechaInscripcion { get; set; } = DateTime.UtcNow;
        public Genero Genero { get; set; }
    }

    public class AlumnoActividadUpdate
    {
        public int AlumnoId { get; set; }
        public int ActividadId { get; set; }
        public EstadoAlumnoActividad EstadoAlumnoActividad { get; set; }
        public DateTime FechaInscripcion { get; set; } = DateTime.UtcNow;
        public Genero Genero { get; set; }
    }


    public class AlumnoInscritoDto
    {
        public int AlumnoId { get; set; }
        public string NombreCompleto { get; set; }
        public string CarreraNombre { get; set; }
        public decimal CreditosObtenidos { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public EstadoAlumnoActividad EstadoAlumnoActividad { get; set; }
        public Genero Genero { get; set; }
    }

    public class CursoAlumnoDto
    {
        public int ActividadId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImagenNombre { get; set; }
        public decimal Creditos { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public EstadoAlumnoActividad EstadoAlumnoActividad { get; set; }
        public Genero Genero { get; set; }
    }

}