using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Application.Commands;
using TaskManagementAPI.Application.Queries;

namespace TaskManagement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
    {
        if (command == null || string.IsNullOrEmpty(command.Title))
        {
            return BadRequest("O título da tarefa é obrigatório.");
        }

        var createdTask = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTasks([FromQuery] Guid projectId)
    {
        var tasks = await _mediator.Send(new GetAllTasksQuery(projectId));
        return Ok(tasks);
    }

    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(Guid id)
    {
        var task = await _mediator.Send(new GetTaskByIdQuery(id));
        if (task == null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    // Endpoint para atualizar uma tarefa
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID da tarefa não corresponde ao ID informado.");
        }

        var updatedTask = await _mediator.Send(command);
        return Ok(updatedTask);
    }
}