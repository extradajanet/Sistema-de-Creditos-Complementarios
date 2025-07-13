using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Models.Enum
{
    public enum EstadoActividad
    {
        Activo = 1,
        EnProgreso = 2,
        Finalizado = 3
    }

    public enum EstadoAlumnoActividad
    {
        Inscrito = 1, // El alumno se ha inscrito a la actividad
        EnCurso = 2, // El alumno está participando en la actividad
        Completado = 3, // El alumno ha completado la actividad
        Acreditado = 4, // El alumno ha acreditado la actividad
        NoAcreditado = 5 // El alumno no ha acreditado la actividad
    }

    public enum Dias
    {
        Lunes = 1,
        Martes = 2,
        Miercoles = 3,
        Jueves = 4,
        Viernes = 5,
        Sabado = 6,
        Domingo = 7
    }

    public enum TipoActividad
    {
        Deportiva = 1, // Actividades deportivas
        Cultural = 2, // Actividades culturales
        Tutorias = 3, // Actividades tutorías
        MOOC = 4, // Cursos en línea (MOOC)
    }
}
