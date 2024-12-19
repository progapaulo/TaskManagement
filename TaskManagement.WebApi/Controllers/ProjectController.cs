using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Application.Commands;
using TaskManagementAPI.Application.Queries;

namespace TaskManagement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Endpoint para criar um projeto
    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand command)
    {
        var validator = new CreateProjetoCommandValidator();
        var validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        
        var createdProject = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, createdProject);
    }

    // Endpoint para listar todos os projetos
    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        var projects = await _mediator.Send(new GetAllProjectsQuery());
        return Ok(projects);
    }

    // Endpoint para visualizar um projeto específico (opcional)
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(Guid id)
    {
        var project = await _mediator.Send(new GetProjectByIdQuery(id));
        if (project == null)
        {
            return NotFound();
        }

        return Ok(project);
    }
}