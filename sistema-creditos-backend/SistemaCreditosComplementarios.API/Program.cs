using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.ActividadRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAlumnoRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAuthRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.ICarreraRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IActividadService;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAlumnoService;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IAuthService;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.ICarreraService;
using SistemaCreditosComplementarios.Core.Services.ActividadService;
using SistemaCreditosComplementarios.Core.Services.AlumnoServices;
using SistemaCreditosComplementarios.Core.Services.AuthServices;
using SistemaCreditosComplementarios.Core.Services.CarreraServices;

using SistemaCreditosComplementarios.Core.Settings;
using SistemaCreditosComplementarios.Infraestructure.Data;
using SistemaCreditosComplementarios.Infraestructure.Repositories;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configurar base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyecci�n de dependencias para el repositorio
builder.Services.AddScoped<IActividadRepository, ActividadRepository>(); //se añadió el repositorio para las actividades
builder.Services.AddScoped<ICarreraRepository, CarreraRepository>(); //se añade el repositorio para las carreras
builder.Services.AddScoped<IAlumnoRepository, AlumnoRepository>(); 
builder.Services.AddScoped<IAuthRepository, AuthRepository>();


//Inyecci�n de dependencias para el servicio 
builder.Services.AddScoped<IActividadService, ActividadService>(); //se añade el servicio de actividades
builder.Services.AddScoped<ICarreraService, CarreraService>(); //se añade el servicio de carreras
builder.Services.AddScoped<IAlumnoService, AlumnoService>();
builder.Services.AddScoped<IAuthService, AuthService>(); //se añade el servicio de autenticaci�n

// JWT Authentication 
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
var key = Encoding.UTF8.GetBytes(jwtOptions.SecretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(options => 
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
        };
    });

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configuraci�n de CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Inicializar roles y usuarios de identidad
try
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        await IdentityDataInitializer.InitializeAsync(services);
    }
}
catch (Exception ex)
{
    // Manejo de excepciones al inicializar los datos de identidad
    Console.WriteLine($"Error al inicializar los datos de identidad: {ex.Message}");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
