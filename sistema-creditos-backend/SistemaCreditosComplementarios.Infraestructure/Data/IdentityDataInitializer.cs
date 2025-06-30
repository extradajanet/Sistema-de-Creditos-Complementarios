using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SistemaCreditosComplementarios.Core.Models.Coordinador;
using SistemaCreditosComplementarios.Core.Models.Departamento;
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
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
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
                var user = new IdentityUser
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
                var user = new IdentityUser
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
                var user = new IdentityUser
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
        }
    }
}
