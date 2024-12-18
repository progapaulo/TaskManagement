using MediatR;
using TaskManagement.Domain.Entities;

namespace TaskManagementAPI.Application.Commands;

public class CreateProjectCommand : IRequest<Project>
{
    public string Name { get; set; }
}