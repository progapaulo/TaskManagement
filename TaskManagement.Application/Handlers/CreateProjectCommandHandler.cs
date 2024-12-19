using FluentValidation;
using MediatR;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;
using TaskManagementAPI.Application.Commands;

namespace TaskManagementAPI.Application.Handlers;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Project>
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Project> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateProjetoCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            // Lançar uma exceção ou retornar erros para a API
            throw new ValidationException(validationResult.Errors);
        }
        
        var project = new Project
        {
            Name = request.Name,
            UsuarioId = request.UserID
        };

        return await _projectRepository.AddAsync(project);
    }
}