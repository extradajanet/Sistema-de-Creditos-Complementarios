using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaCreditosComplementarios.Core.Models.ActividadModel;
using SistemaCreditosComplementarios.Core.Models.Alumno;
using SistemaCreditosComplementarios.Core.Models.Coordinador;
using SistemaCreditosComplementarios.Core.Models.Departamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Infraestructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<ActividadModels> Actividades { get; set; }

        public DbSet<Alumno> Alumnos { get; set; }

        public DbSet<Departamento> Departamentos { get; set; }

        public DbSet<Coordinador> Coordinadores { get; set; }

    }
}
