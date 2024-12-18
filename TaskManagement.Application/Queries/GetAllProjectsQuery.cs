using MediatR;
using TaskManagement.Domain.Entities;

namespace TaskManagementAPI.Application.Queries;

public class GetAllProjectsQuery : IRequest<IEnumerable<Project>>
{
}