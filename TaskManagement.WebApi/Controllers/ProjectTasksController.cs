using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.Services;

namespace TaskManagement.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectTasksController : ControllerBase
{
    private readonly IProjectService _projectService;
    //private readonly ITaskService _taskService;

    public ProjectTasksController(IProjectService projectService)
    {
        _projectService = projectService;
        //_taskService = taskService;
    }

    // Endpoint para criar um novo projeto
    [HttpPost("projects")]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto dto)
    {
        var project = await _projectService.CreateProjectAsync(dto);
        return CreatedAtAction(nameof(CreateProject), new { id = project.Id }, project);
    }

    // // Endpoint para criar uma nova tarefa
    // [HttpPost("tasks")]
    // public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto dto)
    // {
    //     var task = await _taskService.CreateTaskAsync(dto);
    //     return CreatedAtAction(nameof(CreateTask), new { id = task.Id }, task);
    // }
    
}