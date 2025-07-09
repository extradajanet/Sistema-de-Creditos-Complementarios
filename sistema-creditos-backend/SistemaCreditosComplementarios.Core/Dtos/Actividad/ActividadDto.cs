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
        public string TipoActividad { get; set; }
        public EstadoActividad EstadoActividad { get; set; }
        public int CarreraId { get; set; }
        public string CarreraNombre { get; set; }
        public int CapacidadMaxima { get; set; }
        public string ImagenNombre { get; set; }
    }

    public class ActividadCreateDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Creditos { get; set; }
        public string TipoActividad { get; set; }
        public int CarreraId { get; set; }
        public EstadoActividad EstadoActividad { get; set; }
        public int CapacidadMaxima { get; set; }
        public string ImagenNombre { get; set; }
    }
}
