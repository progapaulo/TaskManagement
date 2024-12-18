using MediatR;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;
using TaskManagementAPI.Application.Queries;

namespace TaskManagementAPI.Application.Handlers;

public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<Tasks>>
{
    private readonly ITaskRepository _taskRepository;

    public GetAllTasksQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<Tasks>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        return await _taskRepository.GetAllByProjectIdAsync(request.ProjectId);
    }
}