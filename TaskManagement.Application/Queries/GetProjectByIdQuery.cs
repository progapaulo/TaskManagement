using MediatR;
using TaskManagement.Domain.Entities;

namespace TaskManagementAPI.Application.Queries;

public class GetProjectByIdQuery : IRequest<Project>
{
    public Guid Id { get; set; }

    public GetProjectByIdQuery(Guid id)
    {
        Id = id;
    }
}