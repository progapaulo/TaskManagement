using MediatR;
using TaskManagement.Domain.Entities;

namespace TaskManagementAPI.Application.Queries;

public class GetAllTasksQuery : IRequest<IEnumerable<Tasks>>
{
    public Guid ProjectId { get; set; }

    public GetAllTasksQuery(Guid projectId)
    {
        ProjectId = projectId;
    }
}