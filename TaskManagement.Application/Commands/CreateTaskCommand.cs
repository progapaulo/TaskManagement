using MediatR;
using TaskManagement.Domain.Entities;

namespace TaskManagementAPI.Application.Commands;

public class CreateTaskCommand : IRequest<Tasks>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid ProjectId { get; set; }
    public DateTime DueDate { get; set; }
    public string Priority { get; set; }
}
