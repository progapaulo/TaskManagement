using MediatR;
using TaskManagement.Domain.Entities;

namespace TaskManagementAPI.Application.Queries;

public class GetTaskByIdQuery : IRequest<Tasks>
{
    public Guid Id { get; set; }

    public GetTaskByIdQuery(Guid id)
    {
        Id = id;
    }
}