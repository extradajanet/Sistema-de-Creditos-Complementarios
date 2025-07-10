using SistemaCreditosComplementarios.Core.Models.ActividadModel;
using SistemaCreditosComplementarios.Core.Models.CarreraModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Models.ActividadesCarreras
{
    public class ActividadCarrera
    {
        public int IdActividad { get; set; }
        public Actividad Actividad { get; set; } // Relación con la entidad Actividad

        public int IdCarrera { get; set; }
        public Carrera Carrera { get; set; } // Relación con la entidad Carrera
    }
}
