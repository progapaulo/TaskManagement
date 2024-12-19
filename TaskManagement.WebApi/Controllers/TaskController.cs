using System.Text.Json;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;
using TaskManagement.Domain.Services;
using TaskManagementAPI.Application.Commands;
using TaskManagementAPI.Application.Queries;

namespace TaskManagement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreateTaskCommand> _validator;
    private readonly TaskService _taskService;
    private readonly ITaskRepository _taskRepository;

    public TaskController(IMediator mediator, IValidator<CreateTaskCommand> validator, TaskService taskService,
        ITaskRepository taskRepository)
    {
        _mediator = mediator;
        _validator = validator;
        _taskService = taskService;
        _taskRepository = taskRepository;
    }
    
    [HttpPost("{taskId}/comments")]
    public async Task<IActionResult> AddComment(Guid taskId, [FromBody] string commentText)
    {
        try
        {
            await _taskService.AddCommentToTaskAsync(taskId, commentText);
            return Ok("Comment added successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
    {
        // Validando o comando de criação da tarefa
        var validationResult = await _validator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var createdTask = await _mediator.Send(command);
        
        // Create a task history entry
        var taskHistory = new TaskHistory
        {
            TaskId = createdTask.Id,
            ChangeDetails = "Tarefa criada",
            ChangeDate = DateTime.UtcNow
        };

        // Add the history to the task
        await _taskRepository.AddTaskHistoryAsync(taskHistory);
        
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