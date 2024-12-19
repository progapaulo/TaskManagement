using FluentValidation;
using MediatR;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enum;
using TaskManagement.Domain.Repositories;
using TaskManagementAPI.Application.Commands;

namespace TaskManagementAPI.Application.Handlers;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Tasks>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IValidator<CreateTaskCommand> _validator;

    public CreateTaskCommandHandler(ITaskRepository taskRepository, IProjectRepository projectRepository,
        IValidator<CreateTaskCommand> validator)
    {
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
        _validator = validator;
    }

    public async Task<Tasks> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        // Validando o comando de criação da tarefa
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            // Se não for válido, lança uma exceção ou retorna erros para a API
            throw new ValidationException(validationResult.Errors);
        }
        
        var project = await _projectRepository.GetByIdAsync(request.ProjectId);
        if (project == null)
        {
            throw new ArgumentException("Projeto não encontrado.");
        }
        var task = new Tasks
        {
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            Priority = request.Priority,
            Status = StatusTask.PENDENTE,
            ProjectId = request.ProjectId
        };

        return await _taskRepository.AddAsync(task);
    }
}