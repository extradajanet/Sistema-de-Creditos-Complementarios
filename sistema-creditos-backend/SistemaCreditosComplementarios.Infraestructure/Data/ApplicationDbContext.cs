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

namespace SistemaCreditosComplementarios.Infraestructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<ActividadModels> Actividades { get; set; }
        
        public DbSet<CarreraModels> Carreras { get; set; }

        public DbSet<Alumno> Alumnos { get; set; }

        public DbSet<Departamento> Departamentos { get; set; }

        public DbSet<Coordinador> Coordinadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //configurar las carreras por defecto 
            modelBuilder.Entity<CarreraModels>().HasData(
                   new CarreraModels { Id = 1, Nombre = "Ingeniería en Sistemas Computacionales" },
                new CarreraModels { Id = 2, Nombre = "Ingeniería en Tecnologías de la Información y Comunicaciones" },
                new CarreraModels { Id = 3, Nombre = "Ingeniería en Administración" },
                new CarreraModels { Id = 4, Nombre = "Licenciatura en Administración" },
                new CarreraModels { Id = 5, Nombre = "Arquitectura" },
                new CarreraModels { Id = 6, Nombre = "Licenciatura en Biología" },
                new CarreraModels { Id = 7, Nombre = "Licenciatura en Turismo" },
                new CarreraModels { Id = 8, Nombre = "Ingeniería Civil" },
                new CarreraModels { Id = 9, Nombre = "Contador Público" },
                new CarreraModels { Id = 10, Nombre = "Ingeniería Eléctrica" },
                new CarreraModels { Id = 11, Nombre = "Ingeniería Electromecánica " },
                new CarreraModels { Id = 12, Nombre = "Ingeniería en Gestión Empresarial " },
                new CarreraModels { Id = 13, Nombre = "Ingeniería en  Desarrollo de Aplicaciones " },
                new CarreraModels { Id = 14, Nombre = "Todas las carreras " }
            );

        }
    }
}
