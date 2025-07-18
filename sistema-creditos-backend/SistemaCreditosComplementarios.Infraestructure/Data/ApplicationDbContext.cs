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
using SistemaCreditosComplementarios.Core.Models.ActividadesCarreras;

namespace SistemaCreditosComplementarios.Infraestructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<AlumnoActividad> AlumnosActividades { get; set; }
        public DbSet<ActividadCarrera> ActividadesCarreras { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        
        public DbSet<Carrera> Carreras { get; set; }

        public DbSet<Alumno> Alumnos { get; set; }

        public DbSet<Departamento> Departamentos { get; set; }

        public DbSet<Coordinador> Coordinadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<ActividadCarrera>()
                .HasKey(ac => new { ac.IdActividad, ac.IdCarrera }); // Clave compuesta

            modelBuilder.Entity<ActividadCarrera>()
                .HasOne(ac => ac.Carrera)
                .WithMany(ac => ac.ActividadesCarreras)
                .HasForeignKey(ac => ac.IdCarrera);

            modelBuilder.Entity<ActividadCarrera>()
                .HasOne(ac => ac.Actividad)
                .WithMany(a => a.ActividadesCarreras)
                .HasForeignKey(ac => ac.IdActividad)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Carrera>()
                .HasOne(c => c.Departamento) // a career has one department
                .WithMany() //a department can have different careers
                .HasForeignKey(c => c.DepartamentoId)
                .OnDelete(DeleteBehavior.Restrict); // Can only delete department when not associated to a career

            modelBuilder.Entity<Carrera>()
                .HasOne(c => c.Coordinador)// a career has one coordinator
                .WithMany() // a coordinator can have many careers
                .HasForeignKey(c => c.CoordinadorId)
                .OnDelete(DeleteBehavior.Restrict); // Can only delete coordinador when not associated to a career

        }
    }
}
