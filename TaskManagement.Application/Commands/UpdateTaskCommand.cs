using MediatR;
using TaskManagement.Domain.Entities;

namespace TaskManagementAPI.Application.Commands;

public class UpdateTaskCommand : IRequest<Tasks>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; }
}