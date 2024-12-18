using MediatR;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;
using TaskManagementAPI.Application.Queries;

namespace TaskManagementAPI.Application.Handlers;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Project>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectByIdQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Project> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        return await _projectRepository.GetByIdAsync(request.Id);
    }
}