using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Models.Enum;

namespace SistemaCreditosComplementarios.Core.Dtos.ActividadesExtraescolares
{
    public class ActividadExtraescolarDto
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
        public EstadoActividad EstadoActividad { get; set; } 

        public string ImagenNombre { get; set; }

        public int DepartamentoId { get; set; }
        public string DepartamentoNombre { get; set; }

    }

    public class ActividadExtraescolarCreateDto
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
    }
}

