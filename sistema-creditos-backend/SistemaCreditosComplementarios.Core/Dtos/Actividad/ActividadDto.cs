using SistemaCreditosComplementarios.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Dtos.Actividad
{
    public class ActividadDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Creditos { get; set; }
        public string TipoActividad { get; set; } // "Curso", "Taller", "Seminario", "Tutorías"
        public  EstadoActividad EstadoActividad { get; set; } // "Activo", "En Progreso", "Finalizado"
        public string ImagenNombre { get; set; }
    }

    public class ActividadCreateDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Creditos { get; set; }
        public string TipoActividad { get; set; } // "Curso", "Taller", "Seminario", "Tutorías"
        public EstadoActividad EstadoActividad { get; set; } // "Activo", "En Progreso", "Finalizado"
        public string ImagenNombre { get; set; }
    }
}
