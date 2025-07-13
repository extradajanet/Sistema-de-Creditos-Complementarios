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
        public int Capacidad { get; set; } 

        public Dias Dias { get; set; } 
        public TimeSpan HoraInicio { get; set; } 
        public TimeSpan HoraFin { get; set; } 

        public TipoActividad TipoActividad { get; set; } 
        public EstadoActividad EstadoActividad { get; set; } // "Activo = 1", "En Progreso = 2", "Finalizado = 3"
        
        public string ImagenNombre { get; set; }

        public int DepartamentoId { get; set; }
        public string DepartamentoNombre { get; set; } 
        public List<string> CarreraNombres { get; set; } // Lista de nombres de carreras asociadas a la actividad

    }

    public class ActividadCreateDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Creditos { get; set; }
        public int Capacidad { get; set; }
        public Dias Dias { get; set; } 
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public TipoActividad TipoActividad { get; set; }
        public EstadoActividad EstadoActividad { get; set; }
        public string ImagenNombre { get; set; }
        public int DepartamentoId { get; set; } 
        public List<int> CarreraIds { get; set; } // Lista de IDs de carreras asociadas a la actividad
    }
}
