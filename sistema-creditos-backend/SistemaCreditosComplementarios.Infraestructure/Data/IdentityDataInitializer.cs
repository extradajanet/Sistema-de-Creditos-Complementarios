using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SistemaCreditosComplementarios.Core.Models.CarreraModel;
using SistemaCreditosComplementarios.Core.Models.Coordinadores;
using SistemaCreditosComplementarios.Core.Models.Departamentos;
using SistemaCreditosComplementarios.Core.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Infraestructure.Data
{
    public static class IdentityDataInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>(); 

            string[] roleNames = { "Alumno", "Departamento", "Coordinador", "Administrador" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                    await roleManager.CreateAsync(new IdentityRole(roleName));

            }

            // Crear un usuario administrador si no existe

            var adminEmail = "admin@ejemplo.com";
            if(await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, "Admin@1234");
                await userManager.AddToRoleAsync(user, "Administrador");
            }

            // Crear un usuario coordinador si no existe

            var coordinatorEmail = "coordinador@ejemplo.com";
            if (await userManager.FindByEmailAsync(coordinatorEmail) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "coordinador",
                    Email = coordinatorEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, "Coordinador@1234");
                await userManager.AddToRoleAsync(user, "Coordinador");

                var coordinador = new Coordinador
                {
                    Nombre = "Coordinador",
                    Apellido = "Ejemplo",
                    FechaRegistro = DateTime.UtcNow,
                    UsuarioId = user.Id
                };

                context.Coordinadores.Add(coordinador);
            }

            // Crear un usuario departamento si no existe

            var departmentEmail = "deptosistemas@ejemplo.com";
            if (await userManager.FindByEmailAsync(departmentEmail) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "deptosistemas",
                    Email = departmentEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, "Sistemas@1234");
                await userManager.AddToRoleAsync(user, "Departamento");

                var departamento = new Departamento
                {
                    Nombre = "Sistemas y Computación",
                    FechaRegistro = DateTime.UtcNow,
                    UsuarioId = user.Id
                };

                context.Departamentos.Add(departamento);
            }

            await context.SaveChangesAsync();

            if (!context.Carreras.Any())
            {
                var depto = await context.Departamentos.FirstOrDefaultAsync(d => d.Usuario.Email == "deptosistemas@ejemplo.com");
                var coord = await context.Coordinadores.FirstOrDefaultAsync(c => c.Usuario.Email == "coordinador@ejemplo.com");

                context.Carreras.AddRange(
                    new Carrera { Nombre = "Ingeniería en Sistemas Computacionales", DepartamentoId = depto?.Id, CoordinadorId = coord?.Id },
                    new Carrera { Nombre = "Ingeniería en Tecnologías de la Información", DepartamentoId = depto?.Id, CoordinadorId = coord?.Id },
                    new Carrera { Nombre = "Ingeniería en Administración" },
                    new Carrera { Nombre = "Licenciatura en Administración" },
                    new Carrera { Nombre = "Arquitectura" },
                    new Carrera { Nombre = "Licenciatura en Biología" },
                    new Carrera { Nombre = "Licenciatura en Turismo" },
                    new Carrera { Nombre = "Ingeniería Civil" },
                    new Carrera { Nombre = "Contador Público" },
                    new Carrera { Nombre = "Ingeniería Eléctrica" },
                    new Carrera { Nombre = "Ingeniería Electromecánica " },
                    new Carrera { Nombre = "Ingeniería en Gestión Empresarial " },
                    new Carrera { Nombre = "Ingeniería en  Desarrollo de Aplicaciones ", DepartamentoId = depto?.Id, CoordinadorId = coord?.Id },
                    new Carrera { Nombre = "Todas las carreras " }

                );

                await context.SaveChangesAsync();
            }

        }
    }
}
