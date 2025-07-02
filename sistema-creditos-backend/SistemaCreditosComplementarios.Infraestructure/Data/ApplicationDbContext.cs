using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaCreditosComplementarios.Core.Models.ActividadModel;
using SistemaCreditosComplementarios.Core.Models.Alumnos;

using SistemaCreditosComplementarios.Core.Models.Coordinadores;
using SistemaCreditosComplementarios.Core.Models.Departamentos;
using SistemaCreditosComplementarios.Core.Models.CarreraModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditosComplementarios.Core.Models.Usuario;
using SistemaCreditosComplementarios.Core.Models.AlumnosActividades;

namespace SistemaCreditosComplementarios.Infraestructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<AlumnoActividad> AlumnosActividades { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        
        public DbSet<Carrera> Carreras { get; set; }

        public DbSet<Alumno> Alumnos { get; set; }

        public DbSet<Departamento> Departamentos { get; set; }

        public DbSet<Coordinador> Coordinadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //configurar las carreras por defecto 
            modelBuilder.Entity<Carrera>().HasData(
                   new Carrera { Id = 1, Nombre = "Ingeniería en Sistemas Computacionales" },
                new Carrera { Id = 2, Nombre = "Ingeniería en Tecnologías de la Información y Comunicaciones" },
                new Carrera { Id = 3, Nombre = "Ingeniería en Administración" },
                new Carrera { Id = 4, Nombre = "Licenciatura en Administración" },
                new Carrera { Id = 5, Nombre = "Arquitectura" },
                new Carrera { Id = 6, Nombre = "Licenciatura en Biología" },
                new Carrera { Id = 7, Nombre = "Licenciatura en Turismo" },
                new Carrera { Id = 8, Nombre = "Ingeniería Civil" },
                new Carrera { Id = 9, Nombre = "Contador Público" },
                new Carrera { Id = 10, Nombre = "Ingeniería Eléctrica" },
                new Carrera { Id = 11, Nombre = "Ingeniería Electromecánica " },
                new Carrera { Id = 12, Nombre = "Ingeniería en Gestión Empresarial " },
                new Carrera { Id = 13, Nombre = "Ingeniería en  Desarrollo de Aplicaciones " },
                new Carrera { Id = 14, Nombre = "Todas las carreras " }
            );

            modelBuilder.Entity<AlumnoActividad>()
                .HasKey(aa => new { aa.IdAlumno, aa.IdActividad }); // Clave compuesta

            modelBuilder.Entity<AlumnoActividad>()
                .HasOne(aa => aa.Alumno)
                .WithMany(a => a.AlumnosActividades)
                .HasForeignKey(aa => aa.IdAlumno);

            modelBuilder.Entity<AlumnoActividad>()
                .HasOne(aa => aa.Actividad)
                .WithMany(a => a.AlumnosActividades)
                .HasForeignKey(aa => aa.IdActividad);

        }
    }
}
