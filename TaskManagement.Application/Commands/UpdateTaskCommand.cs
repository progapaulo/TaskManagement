using MediatR;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enum;

namespace TaskManagementAPI.Application.Commands;

public class UpdateTaskCommand : IRequest<Tasks>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public StatusTask Status { get; set; }
}