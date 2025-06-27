using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Models.Enum;

namespace SistemaCreditosComplementarios.Core.Models.ActividadModel
{
    //modelo de curso/actividad para el sistema de créditos complementarios
    public class ActividadModels
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Creditos { get; set; }
        public string TipoActividad { get; set; } //"Curso", "Taller", "Seminario", "Tutorías" **se puede hacer un enum para definir los tipos de actividad

        //se podría agregar más adelante...
        //public int CarreraId { get; set; }
        //public Carrera Carrera { get; set; }

        public EstadoActividad EstadoActividad { get; set; } // estado de la actividad (Activo = 1, En Progreso = 2, Finalizado = 3)

        // Agregado para imagen
        public string ImagenNombre { get; set; } 

    }
}
