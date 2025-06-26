using Microsoft.EntityFrameworkCore;
using SistemaCreditosComplementarios.Core.Interfaces.IRepository.ActividadRepository;
using SistemaCreditosComplementarios.Core.Interfaces.IServices.IActividadService;
using SistemaCreditosComplementarios.Core.Services.ActividadService;
using SistemaCreditosComplementarios.Infraestructure.Data;
using SistemaCreditosComplementarios.Infraestructure.Repositories;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configurar base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyección de dependencias para el repositorio
builder.Services.AddScoped<IActividadRepository, ActividadRepository>(); //se añadió el repositorio para las actividades

//Inyección de dependencias para el servicio 
builder.Services.AddScoped<IActividadService, ActividadService>(); //se añade el servicio de actividades

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
