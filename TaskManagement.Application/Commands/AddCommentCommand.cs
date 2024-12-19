using MediatR;
using TaskManagement.Domain.Entities;

namespace TaskManagementAPI.Application.Commands;

public class AddCommentCommand : IRequest<Comments>
{
    public int TaskId { get; set; }
    public string Comment { get; set; }
}