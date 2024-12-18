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
        var project = new Project
        {
            Name = request.Name
        };

        return await _projectRepository.AddAsync(project);
    }
}