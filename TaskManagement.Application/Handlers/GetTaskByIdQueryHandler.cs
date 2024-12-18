using MediatR;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;
using TaskManagementAPI.Application.Queries;

namespace TaskManagementAPI.Application.Handlers;

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, Tasks>
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskByIdQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Tasks> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        return await _taskRepository.GetByIdAsync(request.Id);
    }
}