using Microsoft.EntityFrameworkCore;
using SistemaCreditosComplementarios.Core.Models.ActividadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Infraestructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<ActividadModels> Actividades { get; set; }
        

    }
}
