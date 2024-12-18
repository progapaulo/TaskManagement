using MediatR;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;
using TaskManagementAPI.Application.Commands;

namespace TaskManagementAPI.Application.Handlers;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Tasks>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IProjectRepository _projectRepository;

    public CreateTaskCommandHandler(ITaskRepository taskRepository, IProjectRepository projectRepository)
    {
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
    }

    public async Task<Tasks> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId);
        if (project == null)
        {
            throw new ArgumentException("Projeto não encontrado.");
        }

        if (project.Tasks.Count >= 20)
        {
            throw new InvalidOperationException("Limite de 20 tarefas por projeto atingido.");
        }

        var task = new Tasks
        {
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            Priority = request.Priority,
            Status = "Pendente",
            ProjectId = request.ProjectId
        };

        return await _taskRepository.AddAsync(task);
    }
}