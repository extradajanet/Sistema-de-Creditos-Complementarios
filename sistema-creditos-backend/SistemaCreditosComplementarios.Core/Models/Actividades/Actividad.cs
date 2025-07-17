using SistemaCreditosComplementarios.Core.Models.ActividadesCarreras;
using SistemaCreditosComplementarios.Core.Models.AlumnosActividades;
using SistemaCreditosComplementarios.Core.Models.CarreraModel;
using SistemaCreditosComplementarios.Core.Models.Departamentos;
using SistemaCreditosComplementarios.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Models.ActividadModel
{
    //modelo de curso/actividad para el sistema de créditos complementarios
    public class Actividad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Creditos { get; set; }
        public int Capacidad { get; set; } 
        public Dias Dias { get; set; } // días de la semana en los que se imparte la actividad (ejemplo: "Lunes, Miércoles, Viernes")
        public TimeSpan HoraInicio { get; set; } // hora de inicio de la actividad (ejemplo: 08:00 AM)
        public TimeSpan HoraFin { get; set; } // hora de fin de la actividad (ejemplo: 10:00 AM)
        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }

        public TipoActividad TipoActividad { get; set; } 
        public EstadoActividad EstadoActividad { get; set; } // estado de la actividad (Activo = 1, En Progreso = 2, Finalizado = 3)
        public string ImagenNombre { get; set; }

        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }

        public ICollection<AlumnoActividad> AlumnosActividades { get; set; }
        public ICollection<ActividadCarrera> ActividadesCarreras { get; set; } 

    }
}
