using System.Text.Json.Serialization;
using ClassLibrary1TaskManagement.Common.Validator;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaskManagement.Domain.Repositories;
using TaskManagement.Domain.Services;
using TaskManagement.ORM;
using TaskManagement.ORM.Repositories;
using TaskManagementAPI.Application;
using TaskManagementAPI.Application.Commands;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DefaultContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddFluentValidation(fv => 
    fv.RegisterValidatorsFromAssemblyContaining<CreateProjetoCommandValidator>());
builder.Services.AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssemblyContaining<CreateTarefaCommandValidator>());

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

// Registro do MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(ApplicationLayer).Assembly,
        typeof(Program).Assembly
    );
});

builder.Services.AddMvc()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    );

// Registro dos repositórios
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
// Configuração do Automapper (caso esteja usando para mapear DTOs)
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Registro de FluenteValidation (caso esteja usando validação de DTOs)
//builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Configuração para usar o Controller (API)
builder.Services.AddControllers();

// Registering the TaskService
builder.Services.AddScoped<TaskService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.MapControllers();

app.Run();