using System.Text.Json.Serialization;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Repositories;
using TaskManagement.ORM;
using TaskManagement.ORM.Repositories;
using TaskManagementAPI.Application;
using TaskManagementAPI.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DefaultContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro do MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(ApplicationLayer).Assembly,
        typeof(Program).Assembly
    );
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(
    options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Registro dos repositórios
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Registro dos serviços
builder.Services.AddScoped<IProjectService, ProjectService>();
//builder.Services.AddScoped<ITaskService, TaskService>();

// Configuração do Automapper (caso esteja usando para mapear DTOs)
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Registro de FluenteValidation (caso esteja usando validação de DTOs)
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Configuração de CORS (caso esteja implementando CORS em sua API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Configuração para usar o Controller (API)
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Usar middleware de CORS
app.UseCors("AllowAll");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Usar middleware para o roteamento de controllers da API
app.UseRouting();

app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}